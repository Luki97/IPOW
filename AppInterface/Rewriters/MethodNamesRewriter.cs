using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AppInterface.Algorithms
{
    class MethodNamesRewriter : CSharpSyntaxRewriter
    {
        private List<string> methodNames;

        public MethodNamesRewriter()
        {
            this.methodNames = new List<string>();
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if(!node.Identifier.ValueText.Equals("Main"))
            {
                methodNames.Add(node.Identifier.ValueText);

                string source = node.ToFullString();

                foreach(ParameterSyntax parameter in node.ParameterList.Parameters)
                {
                    string generatedName = RandomUtils.generateRandomString(RandomUtils.GenerateLittleInt());
                    source = Regex.Replace(source, @"(\W)(" + parameter.Identifier.ValueText + @")(\W)", "$1" + generatedName + "$3");
                }
                    
                foreach(StatementSyntax statement in node.Body.Statements)
                {
                    if(statement.Kind().Equals(SyntaxKind.LocalDeclarationStatement))
                    {
                        foreach(VariableDeclaratorSyntax variable in ((LocalDeclarationStatementSyntax)statement).Declaration.Variables)
                        {
                            string generatedName = RandomUtils.generateRandomString(RandomUtils.GenerateLittleInt());
                            source = Regex.Replace(source, @"(\W)(" + variable.Identifier.ValueText + @")(\W)", "$1" + generatedName + "$3");
                        }   
                    }
                }

                var method = (MethodDeclarationSyntax)SyntaxFactory.ParseMemberDeclaration(source);
                return base.VisitMethodDeclaration(method);
            }
            else
            {
                string source = node.ToFullString();

                foreach (StatementSyntax statement in node.Body.Statements)
                {
                    if (statement.Kind().Equals(SyntaxKind.LocalDeclarationStatement))
                    {
                        foreach (VariableDeclaratorSyntax variable in ((LocalDeclarationStatementSyntax)statement).Declaration.Variables)
                        {
                            string generatedName = RandomUtils.generateRandomString(RandomUtils.GenerateLittleInt());
                            source = Regex.Replace(source, @"(\W)(" + variable.Identifier.ValueText + @")(\W)", "$1" + generatedName + "$3");
                        }
                    }
                }
                var method = (MethodDeclarationSyntax)SyntaxFactory.ParseMemberDeclaration(source);
                return base.VisitMethodDeclaration(method);
            }
        }

        public string changeMethodNamesInClass(string sourceCode)
        {
            string obfuscatedSourceCode = sourceCode;
            foreach(string methodName in methodNames)
            {
                string generatedName = RandomUtils.generateRandomString(RandomUtils.GenerateLittleInt());
                obfuscatedSourceCode = Regex.Replace(obfuscatedSourceCode, @"(\W)(" + methodName + @")(\W)", "$1" + generatedName + "$3");
            }
            return obfuscatedSourceCode;
        }

    }
}
