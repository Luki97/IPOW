using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Algorithms
{
    class InsertDeadCodeIntoMethodsRewriter : CSharpSyntaxRewriter
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
