public class TokenData
{
    public enum TokenType
    {
        // Keywords
        Bool,
        String,
        Int,
        Char,
        Fix,
        Free,
        While,
        New,
        True,
        False,

        // Operators
        Plus,
        Subtract,
        Multiply,
        Divide,
        Modulus,
        Increment,   // ++
        Decrement,   // --
        ArrowRight,  // =>
        ArrowLeft,   // <-
        QuestionMark, // ???
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        DoubleEquals, // ==
        NotEquals,    // !=
        Assign,       // =
        ModAssign,    // %=
        DivideAssign, // /=
        PlusAssign,   // +=
        MinusAssign,  // -=

        // Symbols
        Comma,
        Semicolon,
        Dot,
        Colon,
        Hash,          // #
        ForwardSlash,
        RoundBracketLeft,
        RoundBracketRight,
        BoxBracketLeft,
        BoxBracketRight,
        CurlyBracketLeft,
        CurlyBracketRight,
        QuotationMarks,

        // Identifiers
        Number,
        Variable,

        // Function return types
        FunctionReturnType,
        FunctionName,

        // End of file
        EOF,

        // Additional operators
        And,          // '&&'
        Or,           // '||'
        Assignment,   // '='
        LogicalNot,   // '!'
    }

    public class Token
    {
        public TokenType Type;
        public string Lexeme; // This should be used instead of Lexeme

        public Token(TokenType type, string value)
        {
            Type = type;
            Lexeme = value;
        }
    }
}
