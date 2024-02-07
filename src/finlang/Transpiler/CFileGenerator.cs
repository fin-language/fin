using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class CFileGenerator : CSharpSyntaxWalker
{
    C99ClsEnum cls;
    public SemanticModel model;
    StringBuilder sb;

    /// <summary>
    /// Used to add arguments to a function call. Used for passing an object to its own method call.
    /// This eventually needs to be made into a stack of string builders, so that nested calls can be handled.
    /// </summary>
    StringBuilder firstArgsSb = new();
    public bool skipNextLeadingTrivia = false; // probably needs to be a stack as well
    public bool renderingPrototypes = false;

    Namer namer;
    TranspilerHelper transpilerHelper;

    public CFileGenerator(C99ClsEnum cls) : base(SyntaxWalkerDepth.StructuredTrivia)
    {
        this.cls = cls;
        this.model = cls.model;
        sb = cls.cFile.mainCode;
        namer = new Namer(model);
        transpilerHelper = new(this, model);
    }

    public void SetSb(StringBuilder sb)
    {
        this.sb = sb;
    }

    public void UseHFile()
    {
        sb = cls.hFile.mainCode;
    }

    public void Generate()
    {
        // TODO public constants

        foreach (var member in cls.GetMethods())
        {
            if (member.DeclaringSyntaxReferences.Any())
                Visit(member.DeclaringSyntaxReferences[0].GetSyntax());
        }
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
        var symbol = model.GetDeclaredSymbol(node).ThrowIfNull();

        VisitLeadingTrivia(node);
        sb.Append($"void {Namer.GetCName(symbol)}");

        VisitParameterList(node.ParameterList);

        if (renderingPrototypes)
            return;

        var body = node.Body.ThrowIfNull();
        VisitToken(body.OpenBraceToken);
        sb.Append("        memset(self, 0, sizeof(*self));\n");
        cls.cFile.includes.Add("<string.h>"); // for memset
        body.VisitChildrenNodesWithWalker(this);
        VisitToken(body.CloseBraceToken);
    }

    public override void VisitBlock(BlockSyntax node)
    {
        if (renderingPrototypes)
            return;
        base.VisitBlock(node);
    }

    public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        // a variable declaration can be a field or a local variable. It doesn't contain the semicolon.
        ITypeSymbol typeSymbol = model.GetTypeInfo(node.Type).Type.ThrowIfNull();

        Visit(node.Type);
        // ex: `c_array<u8> a, b;` --> `u8 * a, * b;`

        // render the first one
        {
            if (typeSymbol.IsReferenceType && typeSymbol.Name != "c_array")
                sb.Append($"* ");

            VariableDeclaratorSyntax? variable = node.Variables.First();
            Visit(variable);
        }

        // render the rest
        for (int i = 1; i < node.Variables.Count; i++)
        {
            sb.Append(", ");

            // handle the case 
            if (typeSymbol.IsReferenceType || typeSymbol.Name == "c_array")
            {
                // This is a bit weird I know. Blame the programmers that declare multiple variables on the same line :)
                // The problem is that the type of `c_array<uint8_t>` renders as `uint8_t *` with the pointer.
                sb.Append($"* ");
            }

            VariableDeclaratorSyntax? variable = node.Variables[i];
            Visit(variable);
        }
        //base.VisitVariableDeclaration(node);
    }

    //public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    //{
    //    ITypeSymbol? typeSymbol = model.GetTypeInfo(node.Declaration.Type).Type.ThrowIfNull();

    //    Visit(node.Declaration.Type);

    //    bool needsStar = typeSymbol.IsReferenceType;
    //    string joiner = "";

    //    foreach (var variable in node.Declaration.Variables)
    //    {
    //        sb.Append(joiner); joiner = ", ";
    //        if (needsStar)
    //            sb.Append($"* ");

    //        visitor.Visit(variable);
    //    }
    //    sb.Append(";");
    //    visitor.VisitTrailingTrivia(fds);

    //    //base.VisitFieldDeclaration(node);
    //}


    // parameters are declared for methods and constructors
    public override void VisitParameterList(ParameterListSyntax node)
    {
        ISymbol? symbol = null;

        if (node.Parent is MethodDeclarationSyntax mds)
        {
            symbol = model.GetDeclaredSymbol(mds).ThrowIfNull();
        }
        else if (node.Parent is ConstructorDeclarationSyntax cds)
        {
            symbol = model.GetDeclaredSymbol(cds).ThrowIfNull();
        }

        var list = new WalkableChildSyntaxList(this, node.ChildNodesAndTokens());

        if (symbol?.IsStatic == false)
        {
            list.VisitUpTo(node.OpenParenToken, including: true);

            sb.Append(Namer.GetCName(symbol.ContainingType) + " * self");
            if (node.Parameters.Count > 0)
            {
                sb.Append(", ");
            }
        }

        list.VisitRest();
    }

    public override void VisitParameter(ParameterSyntax node)
    {
        var parameterSymbol = model.GetDeclaredSymbol(node);

        // if the parameter is a reference type, add a star to the type
        if (parameterSymbol != null && parameterSymbol.Type.IsReferenceType)
        {
            bool alreadyHasStar = parameterSymbol.Type.Name == "c_array";

            Visit(node.Type);
            if (!alreadyHasStar)
                sb.Append("* ");
            VisitToken(node.Identifier);
        }
        else
        {
            base.VisitParameter(node);
        }
    }


    // <Expression> <OperatorToken> <Name>
    // `this.stuff` this == Expression. stuff == Name.
    public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        bool done = false;

        if (transpilerHelper.ExpressionIsEnumMember(node.Expression))
        {
            // used for enum access: `MyEnumClass.EnumName`
            Visit(node.Name);
            done = true;
        }
        else if (transpilerHelper.HandleThisMethodAccess(node))
        {
            done = true;
        }
        else
        {
            if (node.IsSimpleMemberAccess())
            {
                if (node.Parent is InvocationExpressionSyntax ies)
                {
                    done = HandleSimpleMemberInvocation(node, ies);
                }
                else
                {
                    done = HandleSimpleMemberAccess(node);
                }
            }
        }

        if (!done)
            base.VisitMemberAccessExpression(node);
    }

    public bool HandleSimpleMemberInvocation(MemberAccessExpressionSyntax node, InvocationExpressionSyntax ies)
    {
        // for non-virtual instance methods: led.toggle() to led_toggle(led)
        // for static methods: Led.toggle() to Led_toggle()

        IMethodSymbol ims = (IMethodSymbol)model.GetSymbolInfo(node.Name).Symbol.ThrowIfNull();
        if (ims.IsStatic)
        {
            // no need to provide the object as an argument
        }
        else
        {
            StringBuilder renderedExpression = new();
            var oldSb = sb;
            sb = renderedExpression;
            skipNextLeadingTrivia = true;
            Visit(node.Expression);
            firstArgsSb = renderedExpression;
            sb = oldSb;
        }

        // add dependency
        cls.cFile.AddFqnDependency(ims.ContainingType);

        VisitLeadingTrivia(node);
        Visit(node.Name);
        VisitTrailingTrivia(node);
        return true;
    }

    public override void VisitArgumentList(ArgumentListSyntax node)
    {
        var list = new WalkableChildSyntaxList(this, node.ChildNodesAndTokens());
        list.VisitUpTo(node.OpenParenToken, including: true);

        if (firstArgsSb.Length > 0)
        {
            sb.Append(firstArgsSb);
            firstArgsSb.Clear();

            if (node.Arguments.Count > 0)
            {
                sb.Append(", ");
            }
        }

        list.VisitRest();
    }

    public bool HandleSimpleMemberAccess(MemberAccessExpressionSyntax node)
    {
        ISymbol memberNameSymbol = model.GetSymbolInfo(node.Name).Symbol.ThrowIfNull();
        //string nameFqn = nameSymbol.GetFqn();

        if (node.Expression is ThisExpressionSyntax tes)
        {
            VisitLeadingTrivia(tes.Token);
            Visit(node.Name);
            //VisitTrailingTrivia(tes.Token);
            return true;
        }

        return TryHandleFinSpecials(node, memberNameSymbol);
    }

    public override void VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        // handle `finlang.unsafe_mode()`
        if (node.Expression is InvocationExpressionSyntax ies)
        {
            if (ies.Expression is MemberAccessExpressionSyntax maes)
            {
                if (maes.Expression is IdentifierNameSyntax ins && ins.Identifier.Text == "math")
                {
                    if (maes.Name.Identifier.Text == "unsafe_mode")
                    {
                        // do nothing
                        VisitLeadingTrivia(node);
                        sb.Append("/* fin: math.unsafe_mode() */");
                        VisitTrailingTrivia(node);
                        return;
                    }
                }
            }
        }

        base.VisitExpressionStatement(node);
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax ies)
    {
        bool done = false;

        if (ies.Expression is MemberAccessExpressionSyntax maes)
        {
            IMethodSymbol methodNameSymbol = (IMethodSymbol)model.GetSymbolInfo(maes.Name).Symbol.ThrowIfNull();
            if (methodNameSymbol.ContainingNamespace.Name == "finlang")
            {
                done = TryFinInvocations(ies, maes, methodNameSymbol);
            }
        }

        if (!done)
            base.VisitInvocationExpression(ies);
    }

    public bool TryFinInvocations(InvocationExpressionSyntax ies, MemberAccessExpressionSyntax maes, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (methodNameSymbol.Name == "unsafe_get")
        {
            int x = 222;
        }

        if (TryFinNumericInvocations(ies, maes, methodNameSymbol))
            return true;

        if (TryFinCArrayInvocations(ies, maes, methodNameSymbol))
            return true;

        return done;
    }

    private bool TryFinCArrayInvocations(InvocationExpressionSyntax ies, MemberAccessExpressionSyntax maes, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (methodNameSymbol.ContainingType.Name != "c_array")
        {
            return done;
        }

        // handle `my_c_array.unsafe_get(index)` --> `my_c_array[index]`
        if (methodNameSymbol.Name == "unsafe_get")
        {
            Visit(maes.Expression); // `my_c_array`
            sb.Append("[");
            Visit(ies.ArgumentList.Arguments.Single());
            sb.Append("]");
            done = true;
        }
        // handle `my_c_array.unsafe_set(index, value)` --> `my_c_array[index] = value`
        else if (methodNameSymbol.Name == "unsafe_set")
        {
            Visit(maes.Expression); // `my_c_array`
            sb.Append("[");
            Visit(ies.ArgumentList.Arguments[0]);
            sb.Append("] = ");
            Visit(ies.ArgumentList.Arguments[1]);
            done = true;
        }

        return done;
    }

    private bool TryFinNumericInvocations(InvocationExpressionSyntax ies, MemberAccessExpressionSyntax maes, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (!methodNameSymbol.BelongsToFinlangInteger())
        {
            return done;
        }

        // handle `u8.from(42)` --> `42`
        if (methodNameSymbol.Name == "from")
        {
            VisitLeadingTrivia(ies);
            // get the argument
            var arg = ies.ArgumentList.Arguments[0].Expression;
            Visit(arg);
            done = true;
        }
        // handle `my_u32.narrow_to_u8()` --> `(uint8_t)my_u32`
        else if (methodNameSymbol.Name.StartsWith("narrow_to_"))
        {
            var finType = methodNameSymbol.Name.Substring("narrow_to_".Length);
            string? ctype = FinNumberTypeToCType(finType);
            if (ctype != null)
            {
                VisitLeadingTrivia(ies);
                sb.Append($"({ctype})");
                Visit(maes.Expression);
                done = true;
            }
        }
        // handle `u8.narrow_from(my_i32)` --> `(uint8_t)my_i32`
        else if (methodNameSymbol.Name == "narrow_from")
        {
            var finType = methodNameSymbol.ContainingType.Name;
            string? ctype = FinNumberTypeToCType(finType);
            if (ctype != null)
            {
                VisitLeadingTrivia(ies);
                sb.Append($"({ctype})");
                Visit(ies.ArgumentList.Arguments.Single());
                done = true;
            }
        }

        return done;
    }

    public override void VisitCastExpression(CastExpressionSyntax node)
    {
        var symbol = model.GetSymbolInfo(node.Type).Symbol.ThrowIfNull();

        if (symbol.ContainingNamespace.Name == "finlang")
        {
            string? ctype = FinNumberTypeToCType(symbol.Name);

            if (ctype != null)
            {
                sb.Append($"({ctype})");
                Visit(node.Expression);
                return;
            }
        }
        
        base.VisitCastExpression(node);
    }

    public bool TryHandleFinSpecials(MemberAccessExpressionSyntax node, ISymbol memberNameSymbol)
    {
        //if (memberNameSymbol.ContainingNamespace.Name != "finlang")
        //    return false;

        if (memberNameSymbol.BelongsToFinlangInteger())
        {
            if (TryFinWideningOrWrapping(node, memberNameSymbol))
                return true;

            if (TryFinSelfDeclaration(node, memberNameSymbol))
                return true;
        }

        return false;
    }

    public bool TryFinSelfDeclaration(MemberAccessExpressionSyntax node, ISymbol memberNameSymbol)
    {
        bool found = false;
        switch (memberNameSymbol.Name)
        {
            case "u8_":
            case "u16_":
            case "u32_":
            case "u64_":
            case "i8_":
            case "i16_":
            case "i32_":
            case "i64_":
            case "f32_":
            case "f64_":
                found = true;
                break;
        }

        if (found)
        {
            // in this case, we just visit the expression
            Visit(node.Expression);
            VisitTrailingTrivia(node.Name);
        }

        return found;
    }

    public string? FinNumberTypeToCType(string finType)
    {
        return finType switch
        {
            "u8" => "uint8_t",
            "u16" => "uint16_t",
            "u32" => "uint32_t",
            "u64" => "uint64_t",
            "i8" => "int8_t",
            "i16" => "int16_t",
            "i32" => "int32_t",
            "i64" => "int64_t",
            "f32" => "float",
            "f64" => "double",
            _ => null
        };
    }

    public bool TryFinWideningOrWrapping(MemberAccessExpressionSyntax node, ISymbol memberNameSymbol)
    {
        string? castType = null;
        bool handled = false;

        switch (memberNameSymbol.Name)
        {
            case "u8": castType = "uint8_t"; break;
            case "u16": castType = "uint16_t"; break;
            case "u32": castType = "uint32_t"; break;
            case "u64": castType = "uint64_t"; break;
            case "i8": castType = "int8_t"; break;
            case "i16": castType = "int16_t"; break;
            case "i32": castType = "int32_t"; break;
            case "i64": castType = "int64_t"; break;
            case "f32": castType = "float"; break;
            case "f64": castType = "double"; break;

            case "wrap_u8": castType = "uint8_t"; break;
            case "wrap_u16": castType = "uint16_t"; break;
            case "wrap_u32": castType = "uint32_t"; break;
            case "wrap_u64": castType = "uint64_t"; break;
            case "wrap_i8": castType = "int8_t"; break;
            case "wrap_i16": castType = "int16_t"; break;
            case "wrap_i32": castType = "int32_t"; break;
            case "wrap_i64": castType = "int64_t"; break;
        }

        if (castType != null)
        {
            sb.Append($"({castType})");
            sb.Append("(");
            Visit(node.Expression);
            sb.Append(")");
            VisitTrailingTrivia(node.Name);
            handled = true;
        }

        return handled;
    }

    ///////////////////////////////////////////////////////////////////////////////

    public override void VisitPredefinedType(PredefinedTypeSyntax node)
    {
        string result = node.Keyword.Text switch
        {
            "void" => "void",
            "bool" => "bool",
            "sbyte" => "int8_t",
            "byte" => "uint8_t",
            "short" => "int16_t",
            "ushort" => "uint16_t",
            "int" => "int32_t",
            "uint" => "uint32_t",
            "long" => "int64_t",
            "ulong" => "uint64_t",
            "float" => "float",
            "double" => "double",
            "string" => "char const *",
            _ => throw new NotImplementedException(node + ""),
        };

        VisitLeadingTrivia(node);
        sb.Append(result);
        VisitTrailingTrivia(node);
    }

    public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        base.VisitAssignmentExpression(node);
    }

    public override void VisitGenericName(GenericNameSyntax node)
    {
        if (node.Identifier.Text == "c_array")
        {
            VisitLeadingTrivia(node);
            Visit(node.TypeArgumentList.Arguments[0]);
            sb.Append(" * ");
            return;
        }
        base.VisitGenericName(node);
    }

    public override void VisitIdentifierName(IdentifierNameSyntax node)
    {
        var result = node.Identifier.Text;

        switch (result)
        {
            case "Boolean": result = "bool"; break;
            case "SByte": result = "int8_t"; break;
            case "Byte": result = "uint8_t"; break;
            case "Int16": result = "int16_t"; break;
            case "UInt16": result = "uint16_t"; break;
            case "Int32": result = "int32_t"; break;
            case "UInt32": result = "uint32_t"; break;
            case "Int64": result = "int64_t"; break;
            case "UInt64": result = "uint64_t"; break;
            case "Double": result = "float"; break;
            case "Single": result = "double"; break;

            default:
                {
                    SymbolInfo symbol = model.GetSymbolInfo(node);
                    
                    // support field accesses
                    if (symbol.Symbol is IFieldSymbol fs)
                    {
                        result = "self->" + Namer.GetCName(fs);
                        break;
                    }

                    result = Namer.GetCName(symbol.Symbol.ThrowIfNull());
                    break;
                }
        }

        VisitLeadingTrivia(node);
        sb.Append(result);
        VisitTrailingTrivia(node);
    }

    public override void VisitTrivia(SyntaxTrivia trivia)
    {
        sb.Append(trivia.ToFullString()); // to full string required for XML doc comments
    }

    public override void VisitLeadingTrivia(SyntaxToken token)
    {
        if (skipNextLeadingTrivia)
        {
            skipNextLeadingTrivia = false;
            return;
        }

        if (!token.HasLeadingTrivia)
            return;

        VisitTriviaList(token.LeadingTrivia);
    }

    public override void VisitTrailingTrivia(SyntaxToken token)
    {
        if (!token.HasTrailingTrivia)
            return;

        VisitTriviaList(token.TrailingTrivia);
    }

    public void VisitTriviaList(SyntaxTriviaList syntaxTrivias)
    {
        VisitTriviaList((IReadOnlyList<SyntaxTrivia>)syntaxTrivias);
    }

    public void VisitTriviaList(IReadOnlyList<SyntaxTrivia> syntaxTrivias)
    {
        foreach (var trivia in syntaxTrivias)
        {
            VisitTrivia(trivia);
        }
    }

    public void VisitLeadingTrivia(SyntaxNode node)
    {
        VisitLeadingTrivia(node.GetFirstToken());
    }

    public void VisitTrailingTrivia(SyntaxNode node)
    {
        VisitTrailingTrivia(node.GetLastToken());
    }

    public void OutputAttachedCommentTrivia(SyntaxNode node)
    {
        List<SyntaxTrivia> toOutput = TranspilerHelper.GetAttachedCommentTrivia(node);
        VisitTriviaList(toOutput);
    }

    public override void VisitToken(SyntaxToken token)
    {
        VisitLeadingTrivia(token);

        switch ((SyntaxKind)token.RawKind)
        {
            case SyntaxKind.PublicKeyword:
            case SyntaxKind.EnumKeyword:
            case SyntaxKind.StaticKeyword:
            case SyntaxKind.ReadOnlyKeyword:
            case SyntaxKind.PrivateKeyword:
                return;
        }

        if (token.IsKind(SyntaxKind.ExclamationToken) && token.Parent.IsKind(SyntaxKind.SuppressNullableWarningExpression))
        {
            // ignore exclamations like: `this.current_state_exit_handler!();`
        }
        else if (token.IsKind(SyntaxKind.IdentifierToken) && token.Parent is MethodDeclarationSyntax mds)
        {
            sb.Append(Namer.GetCName(model.GetDeclaredSymbol(mds).ThrowIfNull()));
        }
        else if (token.IsKind(SyntaxKind.IdentifierToken) && token.Parent is EnumMemberDeclarationSyntax emds)
        {
            sb.Append(Namer.GetCName(model.GetDeclaredSymbol(emds).ThrowIfNull()));
        }
        //else if (token.IsKind(SyntaxKind.ThisKeyword))
        //{
        //    sb.Append("self");
        //}
        else
        {
            sb.Append(token);
        }

        VisitTrailingTrivia(token);
    }
}
