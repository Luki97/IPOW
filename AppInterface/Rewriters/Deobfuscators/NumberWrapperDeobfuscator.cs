using AppInterface.Algorithms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Rewriters
{
    class NumberWrapperDeobfuscator : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitArgument(ArgumentSyntax node)
        {
            if (node.Expression.Kind().Equals(SyntaxKind.ParenthesizedExpression))
            {
                NumberOperations ne = new NumberOperations();
                ParenthesizedExpressionSyntax pe = (ParenthesizedExpressionSyntax) node.Expression;
                ExpressionSyntax es = SyntaxFactory.ParseExpression(ne.ExpressionParser(pe.Expression.ToString()).ToString());
                ArgumentSyntax newNode = node.WithExpression(es);
                return base.VisitArgument(node.ReplaceNode(node, newNode));
            }
            return base.VisitArgument(node);
        }

        public override SyntaxNode VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            if (node.Value.Kind().Equals(SyntaxKind.ParenthesizedExpression))
            {
                NumberOperations ne = new NumberOperations();
                ParenthesizedExpressionSyntax pe = (ParenthesizedExpressionSyntax)node.Value;
                ExpressionSyntax es = SyntaxFactory.ParseExpression(ne.ExpressionParser(pe.Expression.ToString()).ToString());
                EqualsValueClauseSyntax newNode = node.WithValue(es);
                return base.VisitEqualsValueClause(node.ReplaceNode(node, newNode));
            }
            return base.VisitEqualsValueClause(node);
        }

        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (node.Right.Kind().Equals(SyntaxKind.ParenthesizedExpression))
            {
                NumberOperations ne = new NumberOperations();
                ParenthesizedExpressionSyntax pe = (ParenthesizedExpressionSyntax)node.Right;
                ExpressionSyntax es = SyntaxFactory.ParseExpression(ne.ExpressionParser(pe.Expression.ToString()).ToString());
                AssignmentExpressionSyntax newNode = node.WithRight(es);
                return base.VisitAssignmentExpression(node.ReplaceNode(node, newNode));
            }
            return base.VisitAssignmentExpression(node);
        }
    }
}
