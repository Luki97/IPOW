using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AppInterface.Algorithms
{
    class SingleLineCommentsRewriter : CSharpSyntaxRewriter
    {

        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                return SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, Encipher(trivia.ToFullString().ToString(), RandomUtils.generateIntInAlphabetRange()));
            }

            return base.VisitTrivia(trivia);
        }

        public static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }


        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += Cipher(ch, key);

            return output;
        }

    }
}
