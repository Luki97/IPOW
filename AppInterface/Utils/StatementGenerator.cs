using AppInterface.Algorithms;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

public class StatementGenerator
{

    public static StatementSyntax GenerateSingleStatement()
    {
        Random random = new Random();
        switch(random.Next(0, 2))
        {
            case 0:
                return GenerateStringVariable();
            default:
                return GenerateIntegerVariable();
        }
    }

    private static StatementSyntax GenerateStringVariable()
    {
        return SyntaxFactory.ParseStatement(RandomUtils.generateUnusedStringDeclaration());
    }

    private static StatementSyntax GenerateIntegerVariable()
    {
        return SyntaxFactory.ParseStatement(RandomUtils.generateUnusedintegerDeclaration());
    }

    public static StatementSyntax GenerateSemicolons()
    {
        return SyntaxFactory.ParseStatement(RandomUtils.generateRandomUnusedSeparators());
    }
}
