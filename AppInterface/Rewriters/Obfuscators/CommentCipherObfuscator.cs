using AppInterface.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AppInterface.Algorithms
{
    class CommentCipherObfuscator : CSharpSyntaxRewriter
    {
        private CipherUtils cipherUtils;
        public CommentCipherObfuscator()
        {
            cipherUtils = new CipherUtils();
        }
        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            
            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                return  SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, cipherUtils.Encipher(trivia.ToFullString().ToString(), RandomUtils.generateIntInAlphabetRange()));
            }

            return base.VisitTrivia(trivia);
        } 
    }
}
