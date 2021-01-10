using AppInterface.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace AppInterface.Algorithms
{
    class DeobfuscationManager
    {
        private CompilationUnitSyntax root;

        public DeobfuscationManager(string sourceCode)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            this.root = syntaxTree.GetCompilationUnitRoot();
        }

        public void NumberWrapper()
        {
            NumberWrapperDeobfuscator rewriter = new NumberWrapperDeobfuscator();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Number wrapper");
        }

        public string GetSourceCode()
        {
            return root.NormalizeWhitespace().ToFullString();
        }

        public void Deobfuscate(Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.EmptyInstructions:
                    break;
                case Algorithm.ChangeClassAndMethodNames:
                    break;
                case Algorithm.ExtendExpresions:
                    NumberWrapper();
                    break;
                case Algorithm.DeadCodeInjection:
                    break;
                case Algorithm.ChangeNumberBase:
                    break;
                case Algorithm.ReplaceOperators:
                    break;
                case Algorithm.CypherComments:
                    break;
            }
        }
    }
}
