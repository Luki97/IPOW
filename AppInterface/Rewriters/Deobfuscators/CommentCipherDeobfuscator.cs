using AppInterface.Algorithms;
using AppInterface.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;

namespace AppInterface.Rewriters.Deobfuscators
{
    class CommentCipherDeobfuscator : CSharpSyntaxRewriter
    {
        private CipherUtils cipherUtils;
        public CommentCipherDeobfuscator()
        {
            cipherUtils = new CipherUtils();
        }

        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                string line = trivia.ToFullString().ToString();
                return SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, cipherUtils.Decipher(line.Substring(0,line.Length-1), Int32.Parse(line.Substring(line.Length-1))));
            }

            return base.VisitTrivia(trivia);
        }
    }
}

