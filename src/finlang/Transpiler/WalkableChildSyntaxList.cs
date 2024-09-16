﻿#nullable enable

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

// spell-checker: ignore customizer

namespace finlang.Transpiler;

// TODO use a single string for `sm` as replacement for `this`. currently scattered.
//FIXME - use "self" 

public class WalkableChildSyntaxList
{
    private readonly CSharpSyntaxWalker walker;
    private readonly List<SyntaxNodeOrToken> nodeOrTokenList;
    private int index = 0;

    public WalkableChildSyntaxList(CSharpSyntaxWalker walker, ChildSyntaxList childSyntaxList)
    {
        this.walker = walker;
        this.nodeOrTokenList = childSyntaxList.ToList();
    }

    public WalkableChildSyntaxList(CSharpSyntaxWalker walker, SyntaxNode syntaxNode) : this(walker, syntaxNode.ChildNodesAndTokens())
    {

    }

    public SyntaxNodeOrToken Peek()
    {
        return nodeOrTokenList[index];
    }

    public void VisitUpTo(Predicate<SyntaxNodeOrToken> test, bool including = false)
    {
        while (index < nodeOrTokenList.Count)
        {
            SyntaxNodeOrToken syntaxNodeOrToken = nodeOrTokenList[index];
            if (test(syntaxNodeOrToken))
            {
                if (including)
                {
                    VisitNext(syntaxNodeOrToken);
                }
                return;
            }

            VisitNext(syntaxNodeOrToken);
        }
    }

    public void SkipUpTo(SyntaxNodeOrToken toSkip, bool including = false)
    {
        SkipUpTo((snot) => snot == toSkip, including);
    }

    public void SkipUpTo(SyntaxToken syntaxToken, bool including = false)
    {
        SkipUpTo((snot) => snot == syntaxToken, including);
    }

    public void SkipUpTo(Predicate<SyntaxNodeOrToken> test, bool including = false)
    {
        while (index < nodeOrTokenList.Count)
        {
            SyntaxNodeOrToken syntaxNodeOrToken = nodeOrTokenList[index];
            if (test(syntaxNodeOrToken))
            {
                if (including)
                {
                    index++;
                }
                return;
            }

            index++;
        }
    }

    public void VisitNext(SyntaxNodeOrToken? syntaxNodeOrToken = null)
    {
        syntaxNodeOrToken ??= nodeOrTokenList[index];
        syntaxNodeOrToken.Value.VisitWith(walker);
        index++;
    }

    public void SkipNext()
    {
        index++;
    }

    public void VisitUpTo(SyntaxKind syntaxKind, bool including = false)
    {
        VisitUpTo((snot) => snot.IsKind(syntaxKind), including);
    }

    public void VisitUpTo(SyntaxToken syntaxToken, bool including = false)
    {
        VisitUpTo((snot) => snot == syntaxToken, including);
    }

    public void VisitUpTo(SyntaxNode syntaxNode, bool including = false)
    {
        VisitUpTo((snot) => snot == syntaxNode, including);
    }

    public bool TryRemove(SyntaxToken syntaxToken)
    {
        return nodeOrTokenList.Remove(syntaxToken);
    }

    public void Remove(SyntaxToken syntaxToken)
    {
        if (TryRemove(syntaxToken) == false)
            throw new ArgumentException("Failed to find syntaxToken to remove " + syntaxToken);
    }

    public void Replace(SyntaxToken toFind, SyntaxToken replacement)
    {
        int index = nodeOrTokenList.IndexOf(toFind);
        if (index == -1)
            throw new ArgumentException("Failed to find syntaxToken to replace " + replacement);

        nodeOrTokenList.Remove(toFind);
        nodeOrTokenList.Insert(index, replacement);
    }

    public void VisitRest()
    {
        while (index < nodeOrTokenList.Count)
        {
            SyntaxNodeOrToken syntaxNodeOrToken = nodeOrTokenList[index];
            VisitNext(syntaxNodeOrToken);
        }
    }

    public bool HasNext()
    {
        return index < nodeOrTokenList.Count;
    }
}
