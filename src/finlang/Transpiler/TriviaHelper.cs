using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace finlang.Transpiler;

public class TriviaHelper
{
    /// <summary>
    /// This doesn't work with DocumentationCommentTrivia.
    /// </summary>
    /// <param name="triviaList"></param>
    /// <returns></returns>
    [Obsolete]
    public static bool EndsWithNewLineOptSpace(SyntaxTriviaList triviaList)
    {
        // loop backwards to find the last non-whitespace trivia
        for (int i = triviaList.Count - 1; i >= 0; i--)
        {
            var trivia = triviaList[i];
            if (trivia.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                return true;
            }
            if (!trivia.IsKind(SyntaxKind.WhitespaceTrivia))
            {
                return false;
            }
        }

        return false;
    }
}
