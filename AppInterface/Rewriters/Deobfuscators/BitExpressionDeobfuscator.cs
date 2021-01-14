using AppInterface.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Rewriters.Deobfuscators
{
    class BitExpressionDeobfuscator : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            if (!BitExpressionToInt.IsApplicable(node)) { return base.VisitBinaryExpression(node); }

            return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression)
                .WithToken(SyntaxFactory.Literal(BitExpressionToInt.ParseToInt(node)));
        }
    }
}
