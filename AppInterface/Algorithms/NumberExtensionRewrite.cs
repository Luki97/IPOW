using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace AppInterface.Algorithms
{
    class NumberExtensionRewrite : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                //System.Diagnostics.Trace.WriteLine(node.Parent.Kind() + " -> " + node.Parent.ToFullString());
            }
            return base.VisitLiteralExpression(node);
        }

        public override SyntaxNode VisitArgument(ArgumentSyntax node)
        {
            if (node.Expression.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                NumbersExtension ne = new NumbersExtension();
                ArgumentSyntax newNode = node.WithExpression(ne.ExtendNumber((LiteralExpressionSyntax)node.Expression));
                return base.VisitArgument(node.ReplaceNode(node, newNode));

            }
            return base.VisitArgument(node);
        }

        public override SyntaxNode VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            if (node.Value.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                NumbersExtension ne = new NumbersExtension();
                EqualsValueClauseSyntax newNode = node.WithValue(ne.ExtendNumber((LiteralExpressionSyntax)node.Value));
                return base.VisitEqualsValueClause(node.ReplaceNode(node, newNode));
            }
            return base.VisitEqualsValueClause(node);
        }

        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (node.Right.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                NumbersExtension ne = new NumbersExtension();
                AssignmentExpressionSyntax newNode = node.WithRight(ne.ExtendNumber((LiteralExpressionSyntax)node.Right));
                return base.VisitAssignmentExpression(node.ReplaceNode(node, newNode));
            }
            return base.VisitAssignmentExpression(node);
        }

    }
}
