using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AppInterface.Generators;

namespace AppInterface.Algorithms
{
    class NumberToBinaryExpressionObfuscator : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (!node.Kind().Equals(SyntaxKind.NumericLiteralExpression)
                || !int.TryParse(node.Token.ValueText, out int result)) { return base.VisitLiteralExpression(node); }

            return SyntaxFactory.ParseExpression(IntToBitExpression.Replace(result));
        }
    }
}
