

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppInterface.Rewriters.Deobfuscators
{
    class SemicolnsIntoMethodsDeobfuscator : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
        {
            
            return node.WithSemicolonToken(
                SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken)
                    .WithLeadingTrivia(node.SemicolonToken.LeadingTrivia)
                    .WithTrailingTrivia(node.SemicolonToken.TrailingTrivia));
        }

      
    }
    }