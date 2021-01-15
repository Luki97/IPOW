using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Utils
{
    class BitExpressionToInt
    {
        public static bool IsApplicable(BinaryExpressionSyntax node)
        {
            SyntaxKind kind = node.Kind();
            return (kind == SyntaxKind.BitwiseAndExpression
                    || kind == SyntaxKind.BitwiseOrExpression
                    || kind == SyntaxKind.ExclusiveOrExpression)
                && LeftRightNumericLiteralExpression(node);
        }

        private static bool LeftRightNumericLiteralExpression(BinaryExpressionSyntax node)
        {
            return node.Left.Kind() == SyntaxKind.NumericLiteralExpression
                && node.Right.Kind() == SyntaxKind.NumericLiteralExpression;
        }

        public static int ParseToInt(BinaryExpressionSyntax node)
        {
            return node.Kind() switch
            {
                SyntaxKind.BitwiseAndExpression  => int.Parse(node.Left.ToString()) & int.Parse(node.Right.ToString()),
                SyntaxKind.BitwiseOrExpression   => int.Parse(node.Left.ToString()) | int.Parse(node.Right.ToString()),
                SyntaxKind.ExclusiveOrExpression => int.Parse(node.Left.ToString()) ^ int.Parse(node.Right.ToString()),
                _ => 0
            };
        }
    }
}
