using finlang.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System.Text;

namespace finlang.Transpiler;

public class CFileGenerator : CSharpSyntaxWalker
{
    C99ClsEnumInterface cls;
    public SemanticModel model;
    StringBuilder sb;
    OutputFile outputFile;
    StyleSettings styleSettings;
    string Indent => styleSettings.indent;
    protected string NL => styleSettings.newLine;

    /// <summary>
    /// Used to add arguments to a function call. Used for passing an object to its own method call.
    /// This eventually needs to be made into a stack of string builders, so that nested calls can be handled.
    /// </summary>
    StringBuilder firstArgsSb = new();
    public bool skipNextLeadingTrivia = false; // probably needs to be a stack as well
    public bool renderingPrototypes = false;

    Namer namer;
    TranspilerHelper transpilerHelper;
    private bool nextTypeIsNotPointer;
    private bool skipEqualsValueClauseForFieldDeclaration;
    private string? nextTypeOverride;


    public CFileGenerator(C99ClsEnumInterface cls, StyleSettings styleSettings) : base(SyntaxWalkerDepth.StructuredTrivia)
    {
        this.cls = cls;
        this.model = cls.model;
        outputFile = cls.cFile;
        sb = outputFile.mainCodeSb;
        namer = new Namer(model);
        transpilerHelper = new(this, model);
        this.styleSettings = styleSettings;
    }

    public void SetSb(StringBuilder sb)
    {
        this.sb = sb;
    }

    public void SetSbFromOutputFile()
    {
        sb = outputFile.mainCodeSb;
    }

    public void UseHFile()
    {
        outputFile = cls.hFile;
        SetSbFromOutputFile();
    }

    public void UseCFile()
    {
        outputFile = cls.cFile;
        SetSbFromOutputFile();
    }

    public void Generate()
    {
        if (cls.IsFFIClass)
        {
            // FFI classes don't have method implementations
        }
        else
        {
            foreach (var member in cls.GetMethods())
            {
                if (member.IsFFI())
                {
                    cls.SetHasFFIMethod();
                    continue;
                }

                if (member.DeclaringSyntaxReferences.Any())
                    Visit(member.DeclaringSyntaxReferences[0].GetSyntax());
            }
        }
    }

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
        VisitLeadingTrivia(node);
        string name = new Namer(model).GetCName(node);

        sb.AppendTokenAndTrivia(node.Identifier, overrideTokenText: $"typedef enum {name}");
        sb.AppendTokenAndTrivia(node.OpenBraceToken);

        foreach (var kid in node.ChildNodesAndTokens().SkipWhile(n => n.IsToken))
        {
            if (kid.IsNode)
            {
                Visit(kid.AsNode());
            }
            else
            {
                if (kid == node.CloseBraceToken)
                    break;
                VisitToken(kid.AsToken());
            }
        }

        VisitLeadingTrivia(node.CloseBraceToken);
        sb.Append($"}} {name};");
        VisitTrailingTrivia(node.CloseBraceToken);
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
        var symbol = model.GetDeclaredSymbol(node).ThrowIfNull();

        VisitLeadingTrivia(node);
        sb.Append($"void {Namer.GetCName(symbol)}");

        VisitParameterList(node.ParameterList);

        if (renderingPrototypes)
            return;

        cls.cFile.includesSet.Add("<string.h>"); // for memset

        var body = node.Body.ThrowIfNull();
        VisitToken(body.OpenBraceToken);
        sb.Append($"{Indent}{Indent}memset(self, 0, sizeof(*self));{NL}");

        // loop over fields with initializers and set them
        foreach (var field in cls.GetInstanceFields())
        {
            // we don't support initialization of c_array_sized<u8> fields yet.
            if (field.Type.Name == nameof(c_array_sized<u8>))
                continue;

            // determine if the field has an initializer
            var variableDeclartor = (VariableDeclaratorSyntax)field.DeclaringSyntaxReferences.Single().GetSyntax();
            if (variableDeclartor.Initializer == null)
                continue;

            // hanlde field mem assignment/creation
            if (field.HasMemAttr() && field.Type.IsReferenceType)
            {
                // `IsReferenceType` check is there just to allow users to use mem on primitive types if they want.
                // the initializer must be a call to mem.init()
                ExpressionSyntax value = variableDeclartor.Initializer.Value;
                HandleMemInit(field, variableDeclartor, value);
                sb.Append($";{NL}");
            }
            else
            {
                sb.Append($"{Indent}{Indent}self->");
                Visit(variableDeclartor);
                sb.Append($";{NL}");
            }
        }

        body.VisitChildrenNodesWithWalker(this);
        VisitToken(body.CloseBraceToken);
    }

    private void HandleMemInit(IFieldSymbol field, SyntaxNode nodeForError, ExpressionSyntax value)
    {
        var arg = value.GetMemInitCallArgument();
        if (arg is null)
            throw new TranspilerException("mem.init() must be used to initialize a field with the mem attribute", nodeForError);

        // the value must be a new expression
        if (arg.Expression is not ObjectCreationExpressionSyntax oces)
            throw new TranspilerException("mem.init() must be used with a new expression", nodeForError);

        // pass field to constructor
        firstArgsSb.Append($"&self->{field.Name}");
        sb.Append($"{Indent}{Indent}");
        sb.Append($"{namer.GetCName(oces.Type)}_ctor");
        Visit(oces.ArgumentList);
    }

    // From: c_array_sized<u8> data = mem.init(new c_array_sized<u8>(5));
    // To: u8 data[5];
    private bool TryCArraySizedFieldDecl(FieldDeclarationSyntax fds)
    {
        if (fds.Declaration.Type is not GenericNameSyntax gns)
            return false;
        
        if (gns.Identifier.Text != nameof(c_array_sized<u8>))
            return false;

        // find size first from the initializer
        var foundCreations = fds.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();

        if (foundCreations.Count() != 1)
            throw new TranspilerException($"c_array_sized field declaration must have exactly one initializer.", fds);

        ObjectCreationExpressionSyntax creation = foundCreations.First();
        ArgumentListSyntax? argList = creation.ArgumentList;

        if (argList == null || argList.Arguments.Count != 1)
            throw new TranspilerException($"c_array_sized field declaration must have size argument", fds);

        var sizeArg = argList.Arguments[0].Expression;

        if (sizeArg is not LiteralExpressionSyntax les)
            throw new TranspilerException($"c_array_sized field declaration size argument must be a literal", fds);

        if (les.Token.Value is not int size)
            throw new TranspilerException($"c_array_sized field declaration size argument must be an integer", fds);

        VisitLeadingTrivia(fds);
        // get type inside the c_array_sized<T>
        var type = gns.TypeArgumentList.Arguments.Single();
        Visit(type);
        VisitTrailingTrivia(gns.TypeArgumentList);

        sb.Append($"{fds.Declaration.Variables[0].Identifier.Text}[{size}]");
        VisitToken(fds.SemicolonToken);

        return true;
    }

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        if (TryCArraySizedFieldDecl(node))
            return;

        // we need to skip the equals value clause for fields that have initializers
        // Example: `[mem]XyPoint start = mem.init(new XyPoint() { x = 1, y = 2});`
        skipEqualsValueClauseForFieldDeclaration = true;
        base.VisitFieldDeclaration(node);
        skipEqualsValueClauseForFieldDeclaration = false;
        
        // We can also render manually, but it's a bit more work. The below doesn't yet handle multiple variables declared on the same line.
        //foreach (var attrList in node.AttributeLists)
        //    VisitAttributeList(attrList);

        //VisitLeadingTrivia(node.Declaration);
        //Visit(node.Declaration.Type);

        //foreach (var variable in node.Declaration.Variables)
        //{
        //    VisitLeadingTrivia(variable);
        //    VisitToken(variable.Identifier);
        //    VisitTrailingTrivia(variable);
        //}
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        // Render prototype if this is private
        if (node.IsFinNonPublic())
        {
            var oldSb = sb;
            sb = outputFile.prototypesSb;
            renderingPrototypes = true;
            VisitMethod(node);
            StringUtils.EraseTrailingWhitespace(sb); // this is needed because the closing parenthesis often has a newline/whitespace after it.
            sb.Append($";{NL}");
            renderingPrototypes = false;
            sb = oldSb;
        }

        VisitMethod(node);

        void VisitMethod(MethodDeclarationSyntax node)
        {
            VisitLeadingTrivia(node);
            skipNextLeadingTrivia = true;

            if (node.IsFinNonPublic())
                sb.Append("static ");

            base.VisitMethodDeclaration(node);
        }
    }

    public override void VisitBlock(BlockSyntax node)
    {
        if (renderingPrototypes)
            return;
        base.VisitBlock(node);
    }

    // TODOLOW handle multiple variables declared on the same line with array/reference types
    //public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
    //{
    //    // a variable declaration can be a field or a local variable. It doesn't contain the semicolon.
    //    ITypeSymbol typeSymbol = model.GetTypeInfo(node.Type).Type.ThrowIfNull();

    //    // c_array type already provides one star
    //    Visit(node.Type);
    //    // ex: `c_array<u8> a, b;` --> `u8 * a, * b;`
    //    // ex: `c_array<SomeRefType> a, b;` --> `SomeRefType ** a, ** b;`

    //    // render the first one
    //    {
    //        VariableDeclaratorSyntax? variable = node.Variables.First();
    //        Visit(variable);
    //    }

    //    // render the rest
    //    for (int i = 0; i < node.Variables.Count; i++)
    //    {
    //        sb.Append(", ");

    //        // handle the case 
    //        if (typeSymbol.IsReferenceType || typeSymbol.Name == "c_array")
    //        {
    //            // This is a bit weird I know. Blame the programmers that declare multiple variables on the same line :)
    //            // The problem is that the type of `c_array<uint8_t>` renders as `uint8_t *` with the pointer.
    //            sb.Append($"* ");
    //        }

    //        VariableDeclaratorSyntax? variable = node.Variables[i];
    //        Visit(variable);
    //    }
    //    //base.VisitVariableDeclaration(node);
    //}

    public override void VisitAttributeList(AttributeListSyntax node)
    {
        VisitLeadingTrivia(node);

        if (node.HasFFI())
            sb.Append("// FFI function. User code must provide the implementation");

        // override_type support
        {
            AttributeSyntax? overrideType = node.GetTypeOverride();
            if (overrideType != null)
            {
                LiteralExpressionSyntax? les = overrideType.ArgumentList.ThrowIfNull().Arguments.Single().Expression as LiteralExpressionSyntax;

                if (les == null || !les.IsKind(SyntaxKind.StringLiteralExpression))
                    throw new TranspilerException("Override attribute must have a string literal argument", overrideType);

                nextTypeOverride = les.Token.ValueText;

                if (StringUtils.EndsWithNewLineOptSpace(sb))
                    StringUtils.EraseTrailingWhitespace(sb);
            }
        }

        if (node.HasMemAttr())
            nextTypeIsNotPointer = true;

        var trailingTrivia = node.GetTrailingTrivia().ToFullString();
        if (trailingTrivia.Contains('\n') || trailingTrivia.Contains('\r'))
            sb.Append(trailingTrivia);
    }


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

        VisitParameterListCustom(node, symbol);
    }

    public void VisitParameterListCustom(ParameterListSyntax node, ISymbol? symbol, string? selfTypeName = null)
    {
        var list = new WalkableChildSyntaxList(this, node.ChildNodesAndTokens());

        if (symbol?.IsStatic == false)
        {
            list.VisitUpTo(node.OpenParenToken, including: true);
            selfTypeName ??= Namer.GetCName(symbol.ContainingType);
            sb.Append(selfTypeName + " * self");
            if (node.Parameters.Count > 0)
            {
                sb.Append(", ");
            }
        }

        list.VisitRest();
    }

    public override void VisitQualifiedName(QualifiedNameSyntax node)
    {
        // used for nested classes/enums
        // MyOuterClass.MyInnerClass.DoSomething();
        VisitLeadingTrivia(node);
        Visit(node.Right);
    }

    // <Expression> <OperatorToken> <Name>
    // ex: `this.stuff`. `this` is Expression. `stuff` is Name.
    public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        bool done = false;

        // used for enum access: `MyEnumClass.EnumName`
        if (transpilerHelper.ExpressionIsEnumMember(node.Expression))
        {
            Visit(node.Name);
            done = true;
        }
        else if (HandleThisMethodAccess(node))
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

    /// <summary>
    /// `this.SomeMethod(args)`
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool HandleThisMethodAccess(MemberAccessExpressionSyntax node)
    {
        if (!transpilerHelper.IsThisMethodAccess(node))
            return false;

        VisitLeadingTrivia(node);
        firstArgsSb.Append("self");
        Visit(node.Name);
        return true;
    }

    public bool HandleSimpleMemberInvocation(MemberAccessExpressionSyntax memberAccessNode, InvocationExpressionSyntax ies)
    {
        // for non-virtual instance methods: led.toggle() to led_toggle(led)
        // for static methods: Led.toggle() to Led_toggle()

        IMethodSymbol ims = (IMethodSymbol)model.GetSymbolInfo(memberAccessNode.Name).Symbol.ThrowIfNull();
        // add dependency
        cls.cFile.AddFqnDependency(ims.ContainingType);

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

            // if expression is a field and that field is memory (and not a pointer), we need to pass the address of the field
            // ex: `d1.get_start_x()` where `d1` is mem object, we need to transpile to `D1Class_get_start_x(&self->d1);`
            if (model.GetSymbolInfo(memberAccessNode.Expression).Symbol is IFieldSymbol ifs && ifs.HasMemAttr())
            {
                sb.Append('&');
            }

            Visit(memberAccessNode.Expression);
            firstArgsSb = renderedExpression;
            sb = oldSb;
        }

        VisitLeadingTrivia(memberAccessNode);

        // if the method is an interface method, we need to handle it specially
        if (ims.ContainingType.TypeKind == TypeKind.Interface)
        {
            // we need to call the method on the interface type being accessed and not the interface that contains the method (incase of interface inheritance)
            ExpressionSyntax expression = memberAccessNode.Expression;
            var objInterfaceType = model.GetTypeInfo(expression).ConvertedType.ThrowIfNull();
            string functionName = Namer.GetCName(objInterfaceType) + "_" + ims.Name;
            sb.Append(functionName);
        }
        else
        {
            Visit(memberAccessNode.Name);
        }

        VisitTrailingTrivia(memberAccessNode);
        return true;
    }

    public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        // track type of variable as a dependency
        var typeSymbol = model.GetTypeInfo(node.Type).Type.ThrowIfNull();
        outputFile.AddFqnDependency(typeSymbol);

        base.VisitVariableDeclaration(node);
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

    public override void VisitArgument(ArgumentSyntax node)
    {
        var done = HandleArgumentInterfaceConversion(node);

        if (!done)
        {
            VisitLeadingTrivia(node);

            // if the argument is a ref or out argument, we need to pass the address of the argument
            // ex: `my_obj.do_stuff(ref x)` to `MyObj_do_stuff(&x)`
            if (!node.RefKindKeyword.IsKind(SyntaxKind.None))
            {
                sb.Append('&');
            }

            Visit(node.Expression);
        }
    }

    public override void VisitParameter(ParameterSyntax node)
    {
        VisitLeadingTrivia(node);

        foreach (var attrList in node.AttributeLists)
        {
            // note: if there is an attribute present, it doesn't have any leading whitespace.
            // The whitespace belongs to the opening "(" or to the "," that separates the parameters.
            VisitAttributeList(attrList);
        }

        SyntaxTokenList modifiers = node.Modifiers;

        bool addStar = (modifiers.HasModifier(SyntaxKind.OutKeyword) || modifiers.HasModifier(SyntaxKind.RefKeyword));
        
        // don't visit modifiers

        if (modifiers.HasModifier(SyntaxKind.InKeyword))
        {
            sb.Append("const ");
        }

        Visit(node.Type);

        if (addStar)
        {
            sb.Append("* ");
        }

        VisitToken(node.Identifier);
    }

    private bool HandleArgumentInterfaceConversion(ArgumentSyntax node)
    {
        bool done = false;
        var op = model.GetOperation(node);
        if (op is IArgumentOperation iao)
        {
            var type = iao.Parameter.ThrowIfNull().Type;
            // check if type is an interface
            if (type.TypeKind == TypeKind.Interface)
            {
                var toName = Namer.GetCName(type);
                var fromType = model.GetTypeInfo(node.Expression).Type.ThrowIfNull();
                var fromName = Namer.GetCName(fromType);

                if (toName != fromName)
                {
                    var funcName = InterfaceGenerator.GetMclConversionFunctionName(fromName, toName);
                    sb.Append($"&{funcName}(");
                    base.VisitArgument(node);
                    sb.Append(')');
                    done = true;
                }
            }
        }

        return done;
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

        // need to try Fin special cases first
        if (TryHandleFinFieldLikeSpecials(node, memberNameSymbol))
            return true;

        if (TryInstanceFieldAccess(node))
            return true;

        return false;
    }

    /// <summary>
    /// support stuff like `redLed.my_public_var`, `some_expression.name`
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool TryInstanceFieldAccess(MemberAccessExpressionSyntax node)
    {
        bool done = false;

        // support stuff like `redLed.my_public_var = 33;`
        var exp = node.Expression; // `redLed`
        var name = node.Name;      // `my_public_var`

        // get symbol of name
        ISymbol? symbol = model.GetSymbolInfo(name).Symbol;
        if (symbol != null)
        {
            // if it's a field, we need to handle it
            if (symbol is IFieldSymbol ifs)
            {
                // if it's a field, we need to handle it
                if (ifs.IsStatic)
                {
                    // it's a static field
                    //sb.Append(Namer.GetCName(ifs.ContainingType) + "_");
                    Visit(name);
                    done = true;
                }
                else
                {
                    // if it's an instance field, we need to handle it
                    Visit(exp);

                    ISymbol? expressionSymbol = model.GetSymbolInfo(exp).Symbol;
                    if (expressionSymbol?.HasMemAttr() ?? false)
                    {
                        // ex: `redLed.my_public_var = 33;` --> `redLed.my_public_var = 33;`
                        sb.Append('.');
                    }
                    else
                    {
                        // ex: `redLed.my_public_var = 33;` --> `redLed->my_public_var = 33;`
                        sb.Append("->");
                    }

                    Visit(name);
                    done = true;
                }
            }
        }

        return done;
    }

    public override void VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        // Some invocations need to be handled up here so that we don't output the trailing semicolon
        // ex: `finlang.unsafe_mode();`. The statement includes the semicolon, so we need to handle it here.

        // handle `finlang.unsafe_mode();`
        if (node.Expression is InvocationExpressionSyntax ies)
        {
            IMethodSymbol methodNameSymbol = (IMethodSymbol)model.GetSymbolInfo(node.Expression).Symbol.ThrowIfNull();

            if (methodNameSymbol.IsInFinlangNamespace())
            {
                if (methodNameSymbol.ContainingType.Name == nameof(SimOnly))
                {
                    // do nothing
                    return;
                }

                if (methodNameSymbol.IsClassMethod(nameof(math), nameof(math.unsafe_mode)))
                {
                    // do nothing
                    VisitLeadingTrivia(node);
                    sb.Append("/* fin: math.unsafe_mode() */");
                    VisitTrailingTrivia(node);
                    return;
                }
            }
        }

        base.VisitExpressionStatement(node);
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax ies)
    {
        if (ies.Expression is MemberAccessExpressionSyntax maes)
        {
            // stuff like: `my_u8.wrap_lshift(1 + 1)`, `FinC.ignore_unused(some_var)`
            IMethodSymbol methodNameSymbol = (IMethodSymbol)model.GetSymbolInfo(maes.Name).Symbol.ThrowIfNull();
            if (methodNameSymbol.IsInFinlangNamespace())
            {
                if (TryFinMemberAccessInvocations(ies, maes, methodNameSymbol))
                    return;

                if (TryFinGlobalInvocations(ies, methodNameSymbol))
                    return;
            }
        }

        // Handle an object calling its own method without `this` like `toggle()`
        // We have to check some Fin invocations here as well because of `using static ...`.
        // Allows `ignore_unused()` isntead of `FinC.ignore_unused()`
        if (ies.Expression is IdentifierNameSyntax ins)
        {
            var methodNameSymbol = (IMethodSymbol)model.GetSymbolInfo(ins).Symbol.ThrowIfNull();

            if (methodNameSymbol.IsInFinlangNamespace())
            {
                if (TryFinGlobalInvocations(ies, methodNameSymbol))
                    return;
            }

            if (methodNameSymbol.IsStatic == false && methodNameSymbol.ContainingType.Name == cls.symbol.Name)
            {
                firstArgsSb.Append("self");
            }
        }

        base.VisitInvocationExpression(ies);
    }

    private bool TryFinGlobalInvocations(InvocationExpressionSyntax ies, IMethodSymbol methodNameSymbol)
    {
        if (TryFinMemInvocations(ies, methodNameSymbol))
            return true;

        if (TryFinCInvocations(ies, methodNameSymbol))
            return true;

        if (TrySimOnlyInvocations(ies, methodNameSymbol))
            return true;

        return false;
    }

    public bool TryFinMemberAccessInvocations(InvocationExpressionSyntax ies, MemberAccessExpressionSyntax maes, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (TryFinNumericInvocations(ies, maes, methodNameSymbol))
            return true;

        if (TryFinCArrayInvocations(ies, maes, methodNameSymbol))
            return true;

        return done;
    }

    private bool TryFinCArrayInvocations(InvocationExpressionSyntax ies, MemberAccessExpressionSyntax maes, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (methodNameSymbol.ContainingType.IsCArray() == false)
        {
            return done;
        }

        // handle `my_c_array.unsafe_get(index)` --> `my_c_array[index]`
        if (methodNameSymbol.Name == nameof(c_array<u8>.unsafe_get))
        {
            Visit(maes.Expression); // `my_c_array`
            sb.Append("[");
            Visit(ies.ArgumentList.Arguments.Single());
            sb.Append("]");
            done = true;
        }
        // handle `my_c_array.unsafe_set(index, value)` --> `my_c_array[index] = value`
        else if (methodNameSymbol.Name == nameof(c_array<u8>.unsafe_set))
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
        if (methodNameSymbol.Name == nameof(u8.from))
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
        else if (methodNameSymbol.Name == nameof(u8.narrow_from))
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
        // handle `my_u8.wrap_lshift(1+1)` --> `(uint8_t)(my_u8 << (1+1))`
        // handle `(my_u8 + 10).wrap_lshift(1)` --> `(uint8_t)((my_u8 + 10) << (1))`
        else if (methodNameSymbol.Name == nameof(u8.wrap_lshift))
        {
            var finType = methodNameSymbol.ContainingType.Name;
            string? ctype = FinNumberTypeToCType(finType);
            if (ctype != null)
            {
                VisitLeadingTrivia(ies);
                sb.Append($"({ctype})(");
                Visit(maes.Expression);
                sb.Append(" << (");
                Visit(ies.ArgumentList.Arguments.Single());
                sb.Append("))");
                done = true;
            }
        }

        return done;
    }

    private bool TryFinMemInvocations(InvocationExpressionSyntax ies, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (methodNameSymbol.ContainingType.Name != nameof(mem))
            return done;

        // Handle `mem.size_of(some_var)` --> `sizeof(uint8_t)`
        // We purposely don't translate to `sizeof(some_var)` because we want to avoid
        // the case where `some_var` is a pointer and we end up with `sizeof(int*)` instead of `sizeof(int)`
        if (methodNameSymbol.Name == nameof(mem.size_of))
        {
            ArgumentSyntax arg = ies.ArgumentList.Arguments.Single();
            // get the type of the argument
            var type = model.GetTypeInfo(arg.Expression).ThrowIfNull().Type.ThrowIfNull();
            var cType = FinNumberTypeToCType(type.Name);

            VisitLeadingTrivia(ies);
            sb.Append($"sizeof({cType} /*{arg}*/)");
            VisitTrailingTrivia(ies);
            done = true;
        }

        return done;
    }

    private bool TryFinCInvocations(InvocationExpressionSyntax ies, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        if (methodNameSymbol.ContainingType.Name != nameof(FinC))
            return done;

        // handle `my_c_array.unsafe_get(index)` --> `my_c_array[index]`
        if (methodNameSymbol.Name == nameof(FinC.ignore_unused))
        {
            VisitLeadingTrivia(ies);
            sb.Append("(void)");
            Visit(ies.ArgumentList);
            done = true;
        }
        else if (methodNameSymbol.Name == nameof(FinC.EchoToC))
        {
            // get string literal
            var arg = (LiteralExpressionSyntax)ies.ArgumentList.Arguments[0].Expression;
            sb.Append(arg.Token.ValueText);
            done = true;
        }

        return done;
    }


    private bool TrySimOnlyInvocations(InvocationExpressionSyntax ies, IMethodSymbol methodNameSymbol)
    {
        bool done = false;

        // ignore `SimOnly.run(() => {..stuff..})`
        if (methodNameSymbol.ContainingType.Name == nameof(SimOnly))
        {
            done = true;
            return done;
        }

        return done;
    }

    public override void VisitCastExpression(CastExpressionSyntax node)
    {
        var symbol = model.GetSymbolInfo(node.Type).Symbol.ThrowIfNull();

        if (symbol.IsInFinlangNamespace())
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

    public bool TryHandleFinFieldLikeSpecials(MemberAccessExpressionSyntax node, ISymbol memberNameSymbol)
    {
        if (memberNameSymbol.ContainingType.Name == nameof(c_array_sized<u8>))  // note! generic type doesn't affect the name
        {
            if (memberNameSymbol.Name == nameof(c_array_sized<u8>.length))
            {
                sb.Append("(sizeof(");
                Visit(node.Expression);
                sb.Append(")/sizeof(");
                Visit(node.Expression);
                sb.Append("[0]))");

                return true;
            }
        }

        if (memberNameSymbol.BelongsToFinlangInteger())
        {
            if (TryFinNumberSizeConstant(node, memberNameSymbol))
                return true;

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

    /// <summary>
    /// We already know that memberNameSymbol belongs to a Finlang integer type
    /// </summary>
    public bool TryFinNumberSizeConstant(MemberAccessExpressionSyntax node, ISymbol memberNameSymbol)
    {
        if (memberNameSymbol.Name != nameof(u8.SIZE))
            return false;

        VisitLeadingTrivia(node);

        string cType = FinNumberTypeToCType(memberNameSymbol.ContainingType.Name).ThrowIfNull();
        sb.Append($"sizeof({cType})");

        VisitTrailingTrivia(node);

        return true;
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
        if (HandleNextTypeOverride(node))
            return;

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

    private bool HandleNextTypeOverride(SyntaxNode node)
    {
        if (nextTypeOverride == null)
            return false;

        VisitLeadingTrivia(node);
        sb.Append(nextTypeOverride);
        nextTypeOverride = null;
        VisitTrailingTrivia(node);

        return true;
    }

    public override void VisitThisExpression(ThisExpressionSyntax node)
    {
        VisitLeadingTrivia(node);
        sb.Append("self");
        VisitTrailingTrivia(node);
    }

    public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        bool done = false;

        var leftSymbol = model.GetSymbolInfo(node.Left).Symbol;
        if (leftSymbol is IFieldSymbol ifs && leftSymbol.HasMemAttr())
        {
            HandleMemInit(ifs, node, node.Right);
            done = true;
        }

        if (!done)
            base.VisitAssignmentExpression(node);
    }

    public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        if (skipEqualsValueClauseForFieldDeclaration)
            return;

        bool done = HandleAssignmentInterfaceConversion(node);

        if (!done)
            base.VisitEqualsValueClause(node);
    }

    private bool HandleAssignmentInterfaceConversion(EqualsValueClauseSyntax node)
    {
        bool done = false;
        
        var op = model.GetOperation(node);
        if (op is IVariableInitializerOperation vio)
        {
            var type = vio.Value.Type.ThrowIfNull();
            // check if type is an interface
            if (type.TypeKind == TypeKind.Interface)
            {
                var toName = Namer.GetCName(type);
                var fromType = model.GetTypeInfo(node.Value).Type.ThrowIfNull();
                var fromName = Namer.GetCName(fromType);

                if (fromName != toName)
                {
                    var funcName = InterfaceGenerator.GetMclConversionFunctionName(fromName, toName);
                    VisitToken(node.EqualsToken);

                    sb.Append($"&{funcName}(");
                    Visit(node.Value);
                    sb.Append(')');
                    done = true;
                }
            }
        }

        return done;
    }

    public override void VisitGenericName(GenericNameSyntax node)
    {
        if (node.Identifier.Text == "c_array")
        {
            VisitLeadingTrivia(node);
            TypeSyntax arrayTypeSyntax = node.TypeArgumentList.Arguments.Single();
            base.Visit(arrayTypeSyntax);
            sb.Append(" * ");
            return;
        }
        base.VisitGenericName(node);
    }

    public override void VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        bool done = false;

        // convert `null` to `NULL`
        if (node.IsKind(SyntaxKind.NullLiteralExpression))
        {
            sb.Append("NULL");
            done = true;
        }
        else if (node.IsKind(SyntaxKind.NumericLiteralExpression))
        {
            // support C# binary/hex literals that have underscore separators
            // `0b1010_1110` --> `0b10101110`
            // `0xFF_AA` --> `0xFFAA`
            var text = node.Token.Text;
            if (text.StartsWith("0b") || text.StartsWith("0x"))
            {
                VisitLeadingTrivia(node);
                text = text.Replace("_", "");
                sb.Append(text);
                done = true;
            }
        }
        
        if (!done)
        {
            base.VisitLiteralExpression(node);
        }
    }

    public override void VisitNullableType(NullableTypeSyntax node)
    {
        // converts `Led?` to `Led`
        Visit(node.ElementType);
        VisitLeadingTrivia(node.QuestionToken);
        VisitTrailingTrivia(node.QuestionToken);
    }

    public override void VisitIdentifierName(IdentifierNameSyntax node)
    {
        if (HandleNextTypeOverride(node))
            return;

        var result = node.Identifier.Text;
        string pre = string.Empty;
        string post = string.Empty;

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
                        // note that enum fields count as IFieldSymbol
                        if (!fs.IsStatic && !fs.IsConst)
                        {
                            if (fs.ContainingSymbol.Name == cls.symbol.Name)
                            {
                                // if the field is in the same class, it's a member access.
                                result = "self->" + Namer.GetCName(fs);
                                break;
                            }
                        }
                    }

                    if (symbol.Symbol is IParameterSymbol ps)
                    {
                        if (ps.RefKind == RefKind.Ref || ps.RefKind == RefKind.Out)
                        {
                            pre = "(*";
                            post = ")";
                        }
                    }

                    result = GetCTypeDeclaration(symbol.Symbol.ThrowIfNull());
                    break;
                }
        }

        nextTypeIsNotPointer = false;

        VisitLeadingTrivia(node);
        sb.Append(pre);
        sb.Append(result);
        sb.Append(post);
        VisitTrailingTrivia(node);
    }

    /// <summary>
    /// Returns the C type declaration for a given symbol. Includes pointer if the symbol is a reference type.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public string GetCTypeDeclaration(ISymbol symbol)
    {
        var temp = Namer.GetCName(symbol.ThrowIfNull());

        if (symbol is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.IsReferenceType && namedTypeSymbol.TypeKind != TypeKind.Delegate && !nextTypeIsNotPointer)
            temp += " *";

        return temp;
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
            case SyntaxKind.RequiredKeyword:
            case SyntaxKind.VirtualKeyword:
            case SyntaxKind.OverrideKeyword:
            case SyntaxKind.SealedKeyword:
            case SyntaxKind.InKeyword:
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
