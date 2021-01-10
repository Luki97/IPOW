using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AppInterface.Algorithms
{
    class ClassNamesRewriter : CSharpSyntaxRewriter
    {
        private List<string> classNames;
        private List<string> fieldNames;

        public ClassNamesRewriter()
        {
            this.classNames = new List<string>();
            this.fieldNames = new List<string>();
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            classNames.Add(node.Identifier.ValueText);

            foreach (MemberDeclarationSyntax member in node.Members)
            {
                if (member.Kind().Equals(SyntaxKind.FieldDeclaration))
                {
                    var field = (FieldDeclarationSyntax)member;
                    foreach (VariableDeclaratorSyntax variable in field.Declaration.Variables)
                    {
                        fieldNames.Add(variable.Identifier.ValueText);
                    }
                }
            }

            return base.VisitClassDeclaration(node);
        }

        public string changeClassNames(string sourceCode)
        {
            string obfuscatedSourceCode = sourceCode;
            foreach (string className in classNames)
            {
                string generatedName = RandomUtils.generateRandomString(RandomUtils.GenerateLittleInt());
                obfuscatedSourceCode = Regex.Replace(obfuscatedSourceCode, @"(\W)(" + className + @")(\W)", "$1" + generatedName + "$3");
            }
            return obfuscatedSourceCode;
        }

        public string changeFieldNames(string sourceCode)
        {
            string obfuscatedSourceCode = sourceCode;
            foreach (string fieldName in fieldNames)
            {
                string generatedName = RandomUtils.generateRandomString(RandomUtils.GenerateLittleInt());
                obfuscatedSourceCode = Regex.Replace(obfuscatedSourceCode, @"(\W)(" + fieldName + @")(\W)", "$1" + generatedName + "$3");
            }
            return obfuscatedSourceCode;
        }
    }
}
