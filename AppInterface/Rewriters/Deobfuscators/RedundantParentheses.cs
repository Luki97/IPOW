using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Rewriters
{
    class RedundantParentheses : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            if (node.Expression.Kind() == SyntaxKind.ParenthesizedExpression)
            {
                return VisitParenthesizedExpression(node.DescendantNodes().OfType<ParenthesizedExpressionSyntax>().First());
            }

            if (node.Expression.Kind() == SyntaxKind.NumericLiteralExpression)
            {
                return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression)
                    .WithToken(node.Expression.GetFirstToken());
            }

            return base.VisitParenthesizedExpression(node);

        }
    }
}
