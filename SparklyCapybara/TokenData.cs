using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklyCapybara
{
    internal class TokenData
    {
        public enum TokenType
        {
            Bool,
            True,
            False,
            String,
            Int,
            Char,
            While,
            New,

            Fix,
            Free,

            QuotationMarks,

            Number,
            Subtract,
            Negate,
            Plus,
            SemiColon,
            Percentage,

            Dot,
            Comma,

            ForwardSlash,
            QuestionMark,

            RoundBracketLeft,
            RoundBracketRight,
            BoxBracketLeft,
            BoxBracketRight,
            CurlyBracketLeft,
            CurlyBracketRight,

            LessThan,
            GreaterThan,

            Equals,
            Variable,
            DoubleEquals,
            ArrowLeft,
            NotEquals,
            And,
            Or,
            Increment,
            Decrement,
            ArrowRight,
        }

        public class Token
        {
            public TokenType type;  
            public string value;

            public Token(TokenType type, string value)
            {
                this.type = type;
                this.value = value;
            }
        }
    }
}
