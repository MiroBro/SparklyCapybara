using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklyLanguage
{
    internal class TokenData
    {
        public enum Token 
        {
            Bool,
            String,
            Int,
            Char,

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
        }
    }
}
