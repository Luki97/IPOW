using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace AppInterface.Algorithms
{
    class ObfuscationManager
    {

        private CompilationUnitSyntax root;

        public ObfuscationManager(string sourceCode)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            this.root = syntaxTree.GetCompilationUnitRoot();
        }
        public void InsertDeadCodeIntoMethods()
        {
            InsertDeadCodeIntoMethodsRewriter rewriter = new InsertDeadCodeIntoMethodsRewriter();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Dead code insertion");
        }

        public void ChangeNumericTypes()
        {
            NumericTypesRewriter rewriter = new NumericTypesRewriter();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Numeric types change");
        }

        public void ChangeMethodNames()
        {
            MethodNamesRewriter rewriter = new MethodNamesRewriter();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            root = SyntaxFactory.ParseCompilationUnit(rewriter.changeMethodNamesInClass(root.ToFullString()));
            Trace.WriteLine("Method names change");
        }
        public void ChangeClassNames()
        {
            ClassNamesRewriter rewriter = new ClassNamesRewriter();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            root = SyntaxFactory.ParseCompilationUnit(rewriter.changeClassNames(root.ToFullString()));
            root = SyntaxFactory.ParseCompilationUnit(rewriter.changeFieldNames(root.ToFullString()));
            Trace.WriteLine("Class names change");
        }

        public void InsertUnusedSemicolonsIntoMethods()
        {
            InsertSemicolonsIntoMethodsRewriter rewriter = new InsertSemicolonsIntoMethodsRewriter();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Unused semicolons insertion");
        }
        public void NumberUnwrapper()
        {
            NumberUnwrapperObfuscator rewriter = new NumberUnwrapperObfuscator();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Number unwrapper");
        }

         public void ChangeIntToBinaryExpression()
        {
            NumberToBinaryExpressionRewriter rewriter = new NumberToBinaryExpressionRewriter();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Numeric types change");
        }

        public void CypherComments()
        {
            CommentCipherObfuscator rewriter = new CommentCipherObfuscator();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Cyphering comments");
        }

        public string GetSourceCode()
        {
            return root.NormalizeWhitespace().ToFullString();
        }

        public void Obfuscate(Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.EmptyInstructions:
                    InsertUnusedSemicolonsIntoMethods();
                    break;
                case Algorithm.ChangeClassAndMethodNames:
                    ChangeMethodNames();
                    ChangeClassNames();
                    break;
                case Algorithm.ExtendExpresions:
                    NumberUnwrapper();
                    break;
                case Algorithm.DeadCodeInjection:
                    InsertDeadCodeIntoMethods();
                    break;
                case Algorithm.ChangeNumberBase:
                    ChangeNumericTypes();
                    break;
                case Algorithm.ReplaceOperators:
                    ChangeIntToBinaryExpression();
                    break;
                case Algorithm.CypherComments:
                    CypherComments();
                    break;
            }
        }
    }
}
