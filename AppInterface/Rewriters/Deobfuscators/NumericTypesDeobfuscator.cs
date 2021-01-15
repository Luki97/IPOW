using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppInterface.Algorithms
{
    class NumericTypesDeobfuscator : CSharpSyntaxRewriter
    {

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {

                return base.VisitLiteralExpression(node.ReplaceNode(node, node.WithToken(SyntaxFactory.ParseToken(node.Token.ValueText))));
            }

            return base.VisitLiteralExpression(node);
        }

    }
}
