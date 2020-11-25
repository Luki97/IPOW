using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
        }

        public String GetSourceCode()
        {
            return root.NormalizeWhitespace().ToFullString();
        }

    }
}
