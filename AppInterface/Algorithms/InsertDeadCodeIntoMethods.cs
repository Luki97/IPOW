using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppInterface.Algorithms
{
    class InsertDeadCodeIntoMethods : CSharpSyntaxRewriter
    {

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            SyntaxList<StatementSyntax> blockWithNewStatements = node.Body.Statements;

            for (int i = 0; i < RandomUtils.GenerateLittleInt(); i++)
            {
                blockWithNewStatements = blockWithNewStatements.Insert(0, StatementGenerator.GenerateSingleStatement());
            }

            BlockSyntax newBlock = SyntaxFactory.Block(blockWithNewStatements);

            MethodDeclarationSyntax newMethod2 = node.WithBody(newBlock);

            var newNode = node.ReplaceNode(node, newMethod2);

            return base.VisitMethodDeclaration(newNode);
        }

    }
}
