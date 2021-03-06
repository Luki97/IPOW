﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Algorithms
{
    class NumericTypesRewriter : CSharpSyntaxRewriter
    {

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.Kind().Equals(SyntaxKind.NumericLiteralExpression))
            {
                if (int.TryParse(node.Token.ValueText, out int result))
                {
                    LiteralExpressionSyntax newExp;
                    if (result < 10)
                    {
                        newExp = node.WithToken(SyntaxFactory.ParseToken(NumberOperations.DecToBin(node.Token.ValueText)));
                    }
                    else
                    {
                        newExp = node.WithToken(SyntaxFactory.ParseToken(NumberOperations.DecToHex(node.Token.ValueText)));
                    }

                    var newNode = node.ReplaceNode(node, newExp);
                    return base.VisitLiteralExpression(newNode);
                }
            }

            return base.VisitLiteralExpression(node);
        }

    }
}
