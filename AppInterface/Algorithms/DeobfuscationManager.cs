using AppInterface.Rewriters;
using AppInterface.Rewriters.Deobfuscators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AppInterface.Algorithms
{
    class DeobfuscationManager
    {
        private readonly SyntaxTree SyntaxTree;
        private CompilationUnitSyntax root;

        public DeobfuscationManager(string sourceCode)
        {
            SyntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            this.root = SyntaxTree.GetCompilationUnitRoot();
        }

        public void NumberWrapper()
        {
            NumberWrapperDeobfuscator rewriter = new NumberWrapperDeobfuscator();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Number wrapper");
        }

        public void SimplifyingExpressions()
        {
            BitExpressionDeobfuscator rewriter = new BitExpressionDeobfuscator();
            RedundantParentheses rewriter2 = new RedundantParentheses();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            root = (CompilationUnitSyntax)rewriter2.Visit(root);
            Trace.WriteLine("Simplyfing expression");
        }

        public void DeadCodeAnnihilation()
        {
            // first build semantic model
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "CodeCompiled",
                syntaxTrees: new [] { SyntaxTree }
            );
            SemanticModel model = compilation.GetSemanticModel(SyntaxTree);

            // rewrite syntax
            DeadCodeDeobfuscator rewriter = new DeadCodeDeobfuscator(SyntaxTree, model);
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Dead Code Annihilation");
        }

        public string GetSourceCode()
        {
            return root.NormalizeWhitespace().ToFullString();
        }
        public void DecypherComments()
        {
            CommentCipherDeobfuscator rewriter = new CommentCipherDeobfuscator();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Decyphering comments");
        }

        public void RemoveSemicolns()
        {
            SemicolnsIntoMethodsDeobfuscator rewritter = new SemicolnsIntoMethodsDeobfuscator();
            root = (CompilationUnitSyntax)rewritter.Visit(root);

        }

        public void NumericTypesDeobfuscate()
        {
            NumericTypesDeobfuscator rewriter = new NumericTypesDeobfuscator();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Numeric types deobfuscation");
        }

        public void Deobfuscate(Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.EmptyInstructions:
                    RemoveSemicolns();
                    break;
                case Algorithm.ChangeClassAndMethodNames:
                    break;
                case Algorithm.ExtendExpresions:
                    NumberWrapper();
                    break;
                case Algorithm.DeadCodeInjection:
                    DeadCodeAnnihilation();
                    break;
                case Algorithm.ChangeNumberBase:
                    NumericTypesDeobfuscate();
                    break;
                case Algorithm.ReplaceOperators:
                    SimplifyingExpressions();
                    break;
                case Algorithm.CypherComments:
                    DecypherComments();
                    break;
            }
        }
    }
}
