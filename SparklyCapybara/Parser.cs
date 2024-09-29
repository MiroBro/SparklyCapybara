using SparklyCapybara;
using SparklyCapybara.AbstarctSyntaxTree;
using static SparklyCapybara.AbstarctSyntaxTree.AST;
using static TokenData;

internal class Parser
{
    private List<Token> tokens;
    private int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public ProgramNode Parse()
    {
        var programNode = new ProgramNode();
        while (!IsAtEnd())
        {
            programNode.AddStatement(ParseStatement());
        }
        return programNode;
    }

    private ASTNode ParseStatement()
    {
        if (Match(TokenType.Bool, TokenType.String, TokenType.Int, TokenType.Char))
        {
            return ParseFunctionDeclaration();
        }
        else if (Match(TokenType.While))
        {
            return ParseWhileLoop();
        }
        else if (Match(TokenType.Variable))
        {
            return ParseVariableDeclaration();
        }
        else
        {
            Advance(); // Handle other statements like fix, free, etc.
            return null; // Adjust this to return an appropriate AST node if needed
        }
    }

    private AST.FunctionDeclaration ParseFunctionDeclaration()
    {
        // (bool success->#fullydonewoo, string intToText) IntToAscii(int number = 42)
        var returnTypes = ParseReturnTypes();
        Consume(TokenType.FunctionName, "Expected function name.");
        var functionName = Previous().Lexeme;

        Consume(TokenType.RoundBracketLeft, "Expected '(' after function name.");
        var parameters = ParseParameters();
        Consume(TokenType.RoundBracketRight, "Expected ')' after parameters.");
        Consume(TokenType.CurlyBracketLeft, "Expected '{' to begin function body.");

        var body = new List<ASTNode>();
        while (!Check(TokenType.CurlyBracketRight))
        {
            body.Add(ParseStatement());
        }
        Consume(TokenType.CurlyBracketRight, "Expected '}' to end function body.");

        return new AST.FunctionDeclaration(returnTypes, functionName, parameters, body);
    }

    private List<AST.Parameter> ParseReturnTypes()
    {
        var returnTypes = new List<AST.Parameter>();
        while (!Check(TokenType.RoundBracketRight))
        {
            var type = Consume(TokenType.Bool, "Expected return type.")
                            .Lexeme; // or adjust to handle different types
            var name = Consume(TokenType.Variable, "Expected variable name.")
                            .Lexeme;
            returnTypes.Add(new AST.Parameter(type, name));

            if (!Check(TokenType.RoundBracketRight))
            {
                Consume(TokenType.Comma, "Expected ',' between return types.");
            }
        }
        return returnTypes;
    }

    private List<AST.Parameter> ParseParameters()
    {
        var parameters = new List<AST.Parameter>();
        while (!Check(TokenType.RoundBracketRight))
        {
            var type = Consume(TokenType.Bool, "Expected parameter type.")
                            .Lexeme; // Adjust to handle other types
            var name = Consume(TokenType.Variable, "Expected parameter name.")
                            .Lexeme;
            parameters.Add(new AST.Parameter(type, name));

            if (!Check(TokenType.RoundBracketRight))
            {
                Consume(TokenType.Comma, "Expected ',' between parameters.");
            }
        }
        return parameters;
    }

    private AST.WhileLoop ParseWhileLoop()
    {
        Consume(TokenType.RoundBracketLeft, "Expected '(' after 'while'.");
        var condition = ParseExpression();
        Consume(TokenType.RoundBracketRight, "Expected ')' after condition.");

        Consume(TokenType.CurlyBracketLeft, "Expected '{' to begin while-loop body.");
        var body = new List<ASTNode>();
        while (!Check(TokenType.CurlyBracketRight))
        {
            body.Add(ParseStatement());
        }
        Consume(TokenType.CurlyBracketRight, "Expected '}' to end while-loop body.");

        return new AST.WhileLoop(condition, body);
    }

    private AST.VariableDeclaration ParseVariableDeclaration()
    {
        var variableType = Previous().Lexeme; // Assuming it's a valid type
        var variableName = Consume(TokenType.Variable, "Expected variable name.").Lexeme;
        Consume(TokenType.Assign, "Expected '=' for variable assignment.");
        var initialValue = ParseExpression();
        Consume(TokenType.Semicolon, "Expected ';' after variable declaration.");

        return new AST.VariableDeclaration(variableType, variableName, initialValue);
    }

    private ASTNode ParseExpression()
    {
        // Parse custom expressions like arithmetic, conditionals, etc.
        // For simplicity, assume we return a literal value
        var literalValue = Consume(TokenType.Number, "Expected a number.").Lexeme; // Adjust this for different expressions
        return new AST.LiteralExpression(literalValue);
    }

    private bool Match(params TokenType[] types)
    {
        foreach (TokenType type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }
        return false;
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) current++;
        return Previous();
    }

    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.EOF;
    }

    private Token Peek()
    {
        return tokens[current];
    }

    private Token Previous()
    {
        return tokens[current - 1];
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();
        throw new Exception(message);
    }
}
