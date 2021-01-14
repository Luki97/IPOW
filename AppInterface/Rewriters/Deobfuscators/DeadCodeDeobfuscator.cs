using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace AppInterface.Rewriters
{
    class DeadCodeDeobfuscator : CSharpSyntaxRewriter
    {
        private readonly List<ISymbol> UnusedVars = new List<ISymbol>();

        public DeadCodeDeobfuscator(SyntaxTree root, SemanticModel model) : base()
        {
            var methods = root.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();

            foreach (MethodDeclarationSyntax method in methods)
            {
                // identify unused variables
                DataFlowAnalysis        result      = model.AnalyzeDataFlow(method.Body);
                ImmutableArray<ISymbol> varDeclared = result.VariablesDeclared;
                IEnumerable<ISymbol>    varRead     = result.ReadInside.Union(result.ReadOutside);
                IEnumerable<ISymbol>    varUnused   = varDeclared.Except(varRead);

                // add unused varaibles symbols
                UnusedVars.AddRange(varUnused);
            }
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            var varIdentifer = node.Declaration.Variables.First().Identifier.Value;

            if (UnusedVars.Exists(symbol => symbol.Name.Equals(varIdentifer))) {
                return null;
            }

            return base.VisitLocalDeclarationStatement(node);
        }
    }
}
 