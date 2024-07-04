using SparklyCapybara;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SparklyCapybara.TokenData;

namespace SparklyCapybara
{
    internal class Tokenizer
    {
        public void Tokenize()
        {
            List<string> words = ExtractWordsAndSpecialCharacters2(CodeReader.GetCodeText());
            List<Token> tokens = TransformToTokens(words);
        }

        public static List<Token> TransformToTokens(List<string> items)
        {
            List<Token> tokens = new List<Token>();

            foreach (string item in items)
            {
                TokenType type = MapStringToTokenType(item);
                tokens.Add(new Token(type, item));
            }

            return tokens;
        }

        static List<string> ExtractWordsAndSpecialCharacters2(string input)
        {
            List<string> items = new List<string>();
            StringBuilder word = new StringBuilder();
            HashSet<string> sequences = new HashSet<string> { "=>", "<=", "==", "!=", "&&", "||", "++", "--" };

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                // Check for sequences
                if (i < input.Length - 1)
                {
                    string nextTwoChars = input.Substring(i, 2);
                    if (sequences.Contains(nextTwoChars))
                    {
                        if (word.Length > 0)
                        {
                            items.Add(word.ToString());
                            word.Clear();
                        }
                        items.Add(nextTwoChars);
                        i++; // Skip the next character
                        continue;
                    }
                }

                if (char.IsLetterOrDigit(c))
                {
                    word.Append(c);
                }
                else
                {
                    if (word.Length > 0)
                    {
                        items.Add(word.ToString());
                        word.Clear();
                    }
                    if (!char.IsWhiteSpace(c) && !char.IsControl(c))
                    {
                        items.Add(c.ToString());
                    }
                }
            }

            // Add the last word if there is one
            if (word.Length > 0)
            {
                items.Add(word.ToString());
            }

            return items;
        }

        private static TokenType MapStringToTokenType(string word)
        {
            switch (word)
            {
                case "true":
                case "false":
                    return TokenType.Bool;

                case "\"":
                    return TokenType.QuotationMarks;

                case "-":
                    return TokenType.Subtract;

                case "+":
                    return TokenType.Plus;

                case ";":
                    return TokenType.SemiColon;

                case "%":
                    return TokenType.Percentage;

                case ".":
                    return TokenType.Dot;

                case ",":
                    return TokenType.Comma;

                case "/":
                    return TokenType.ForwardSlash;

                case "?":
                    return TokenType.QuestionMark;

                case "(":
                    return TokenType.RoundBracketLeft;

                case ")":
                    return TokenType.RoundBracketRight;

                case "[":
                    return TokenType.BoxBracketLeft;

                case "]":
                    return TokenType.BoxBracketRight;

                case "{":
                    return TokenType.CurlyBracketLeft;

                case "}":
                    return TokenType.CurlyBracketRight;

                case "<":
                    return TokenType.LessThan;

                case ">":
                    return TokenType.GreaterThan;

                case "=":
                    return TokenType.Equals;

                case "==":
                    return TokenType.DoubleEquals;

                case "=>":
                    return TokenType.ArrowLeft;

                case "<=":
                    return TokenType.ArrowLeft;

                case "!=":
                    return TokenType.NotEquals;

                case "&&":
                    return TokenType.And;

                case "||":
                    return TokenType.Or;

                case "++":
                    return TokenType.Increment;

                case "--":
                    return TokenType.Decrement;

                default:
                    if (int.TryParse(word, out _))
                    {
                        return TokenType.Number;
                    }
                    else
                    {
                        return TokenType.Variable;
                    }
            }
        }
    }
}
