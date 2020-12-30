using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace AppInterface.Algorithms
{
    class NumberExtensionRewrite : CSharpSyntaxRewriter
    {

        public override SyntaxNode VisitArgument(ArgumentSyntax node)
        {
            if (node.Expression.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                NumberOperations ne = new NumberOperations();
                LiteralExpressionSyntax les = (LiteralExpressionSyntax) node.Expression;
                ExpressionSyntax es = SyntaxFactory.ParseExpression(ne.UnwrapNumber(les.Token.ValueText));
                ArgumentSyntax newNode = node.WithExpression(es);
                return base.VisitArgument(node.ReplaceNode(node, newNode));
            }
            return base.VisitArgument(node);
        }

        public override SyntaxNode VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            if (node.Value.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                NumberOperations ne = new NumberOperations();
                LiteralExpressionSyntax les = (LiteralExpressionSyntax) node.Value;
                ExpressionSyntax es = SyntaxFactory.ParseExpression(ne.UnwrapNumber(les.Token.ValueText));
                EqualsValueClauseSyntax newNode = node.WithValue(es);
                return base.VisitEqualsValueClause(node.ReplaceNode(node, newNode));
            }
            return base.VisitEqualsValueClause(node);
        }

        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (node.Right.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                NumberOperations ne = new NumberOperations();
                LiteralExpressionSyntax les = (LiteralExpressionSyntax) node.Right;
                ExpressionSyntax es = SyntaxFactory.ParseExpression(ne.UnwrapNumber(les.Token.ValueText));
                AssignmentExpressionSyntax newNode = node.WithRight(es);
                return base.VisitAssignmentExpression(node.ReplaceNode(node, newNode));
            }
            return base.VisitAssignmentExpression(node);
        }

    }
}
