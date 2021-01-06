using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Algorithms
{
    class InsertSemicolonsIntoMethodsRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            SyntaxList<StatementSyntax> blockWithNewStatements = node.Body.Statements;

            for (int i = blockWithNewStatements.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < RandomUtils.GenerateLittleInt(); j++)
                {
                    blockWithNewStatements = blockWithNewStatements.Insert(i, StatementGenerator.GenerateSemicolons());
                }
            }

            BlockSyntax newBlock = SyntaxFactory.Block(blockWithNewStatements);

            MethodDeclarationSyntax newMethod2 = node.WithBody(newBlock);

            var newNode = node.ReplaceNode(node, newMethod2);

            return base.VisitMethodDeclaration(newNode);
        }
    }
}
