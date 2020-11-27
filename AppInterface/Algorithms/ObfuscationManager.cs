using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Diagnostics;

namespace AppInterface.Algorithms
{
    class ObfuscationManager
    {

        private CompilationUnitSyntax root;

        public ObfuscationManager(String sourceCode)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            this.root = syntaxTree.GetCompilationUnitRoot();
        }

        public void InsertDeadCodeIntoMethods()
        {
            InsertDeadCodeIntoMethods rewriter = new InsertDeadCodeIntoMethods();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Dead code insertion");
        }

        public void ChangeNumericTypes()
        {
            NumericTypesRewrite rewriter = new NumericTypesRewrite();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            Trace.WriteLine("Numeric types change");
        }

        public void ChangeMethodNames()
        {
            MethodNamesChanger rewriter = new MethodNamesChanger();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            root = SyntaxFactory.ParseCompilationUnit(rewriter.changeMethodNamesInClass(root.ToFullString()));
            Trace.WriteLine("Method names change");
        }
        public void ChangeClassNames()
        {
            ClassNamesChanger rewriter = new ClassNamesChanger();
            root = (CompilationUnitSyntax)rewriter.Visit(root);
            root = SyntaxFactory.ParseCompilationUnit(rewriter.changeClassNames(root.ToFullString()));
            root = SyntaxFactory.ParseCompilationUnit(rewriter.changeFieldNames(root.ToFullString()));
            Trace.WriteLine("Class names change");
        }

        public String GetSourceCode()
        {
            return root.NormalizeWhitespace().ToFullString();
        }

    }
}
