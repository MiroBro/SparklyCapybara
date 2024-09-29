using SparklyCapybara.AbstarctSyntaxTree;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Example token list
        var tokens = new List<TokenData.Token>
        {
            new TokenData.Token(TokenData.TokenType.Bool, "bool"),
            new TokenData.Token(TokenData.TokenType.Variable, "myFunction"),
            new TokenData.Token(TokenData.TokenType.RoundBracketLeft, "("),
            new TokenData.Token(TokenData.TokenType.Variable, "param1"),
            new TokenData.Token(TokenData.TokenType.RoundBracketRight, ")"),
            new TokenData.Token(TokenData.TokenType.CurlyBracketLeft, "{"),
            new TokenData.Token(TokenData.TokenType.Variable, "x"),
            new TokenData.Token(TokenData.TokenType.CurlyBracketRight, "}"),
            new TokenData.Token(TokenData.TokenType.EOF, "")
        };

        // Parse tokens
        var parser = new Parser(tokens);
        var programNode = parser.Parse();

        // Create the evaluator
        var evaluator = new ASTEvaluator();

        // Evaluate the program
        programNode.Accept(evaluator);
    }
}
