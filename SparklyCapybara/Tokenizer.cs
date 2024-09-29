using SparklyCapybara;
using static TokenData;
using System.Text;

internal class Tokenizer
{
    private string codeText;

    public Tokenizer(string inputCode)
    {
        codeText = inputCode;
    }

    public List<Token> Tokenize()
    {
        // Use the codeText to extract tokens
        List<string> words = ExtractWordsAndSpecialCharacters(codeText);
        return TransformToTokens(words);
    }

    static List<string> ExtractWordsAndSpecialCharacters(string input)
    {
        List<string> items = new List<string>();
        StringBuilder word = new StringBuilder();
        HashSet<string> sequences = new HashSet<string> { "=>", "<=", "==", "!=", "&&", "||", "++", "--", "->", "???", "%=", "/=", "+=", "-=" };
        HashSet<char> specialChars = new HashSet<char> { '#', '-', '+', '_', '=', '<', '>', '/', '%', '!', '?' };

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            // Check for sequences
            if (i < input.Length - 1)
            {
                string nextTwoChars = input.Substring(i, 2);
                if (sequences.Contains(nextTwoChars))
                {
                    word.Append(nextTwoChars);
                    i++; // Skip the next character
                    items.Add(word.ToString());
                    word.Clear();
                    continue;
                }
            }

            if (i < input.Length - 2)
            {
                string nextThreeChars = input.Substring(i, 3);
                if (sequences.Contains(nextThreeChars))
                {
                    word.Append(nextThreeChars);
                    i += 2; // Skip the next two characters
                    items.Add(word.ToString());
                    word.Clear();
                    continue;
                }
            }

            if (char.IsLetterOrDigit(c) || (WordStartsWithLetterOrDigit(word) && specialChars.Contains(c)))
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

    private static bool WordStartsWithLetterOrDigit(StringBuilder word)
    {
        if (word.Length > 0)
            return char.IsLetterOrDigit(word[0]);
        return false;
    }

    public static List<Token> TransformToTokens(List<string> items)
    {
        List<Token> tokens = new List<Token>();

        for (int i = 0; i < items.Count; i++)
        {
            string currentItem = items[i];
            TokenType tokenType = MapStringToTokenType(currentItem, tokens);

            tokens.Add(new Token(tokenType, currentItem));
        }

        return tokens;
    }

    private static TokenType MapStringToTokenType(string item, List<Token> tokens)
    {
        switch (item)
        {
            case "true": return TokenType.True;
            case "false": return TokenType.False;
            case "bool": return TokenType.Bool;
            case "string": return TokenType.String;
            case "int": return TokenType.Int;
            case "char": return TokenType.Char;
            case "fix": return TokenType.Fix;
            case "free": return TokenType.Free;
            case "while": return TokenType.While;
            case "new": return TokenType.New;

            // Handle operators and symbols
            case "+": return TokenType.Plus;
            case "-": return TokenType.Subtract;
            case "*": return TokenType.Multiply;
            case "/": return TokenType.Divide;
            case "%": return TokenType.Modulus;
            case "++": return TokenType.Increment;
            case "--": return TokenType.Decrement;
            case "=>": return TokenType.ArrowRight;
            case "->": return TokenType.ArrowLeft;
            case "<=": return TokenType.LessThanOrEqual;
            case ">=": return TokenType.GreaterThanOrEqual;
            case "==": return TokenType.DoubleEquals;
            case "!=": return TokenType.NotEquals;
            case "=": return TokenType.Assign;
            case "%=": return TokenType.ModAssign;
            case "/=": return TokenType.DivideAssign;
            case "+=": return TokenType.PlusAssign;
            case "-=": return TokenType.MinusAssign;
            case "???": return TokenType.QuestionMark;
            case ";": return TokenType.Semicolon;
            case ".": return TokenType.Dot;
            case ",": return TokenType.Comma;
            case "#": return TokenType.Hash;
            case "(": return TokenType.RoundBracketLeft;
            case ")": return TokenType.RoundBracketRight;
            case "[": return TokenType.BoxBracketLeft;
            case "]": return TokenType.BoxBracketRight;
            case "{": return TokenType.CurlyBracketLeft;
            case "}": return TokenType.CurlyBracketRight;

            default:
                if (int.TryParse(item, out _))
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
