using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklyLanguage
{
    internal class Tokenizer
    {
        public void Tokenize()
        {
            string codeString = ReadInput();
        }

        private string ReadInput()
        {
            String line;
            StringBuilder sb = new StringBuilder();

            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("InputCode.txt");
            //Read the first line of text
            line = sr.ReadLine();
            sb.AppendLine(line);
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                //Read the next line
                line = sr.ReadLine();
                sb.AppendLine(line);
            }
            //close the file
            sr.Close();
            Console.ReadLine();

            return sb.ToString();
        }

        private List<TokenData> TransformCodeStringToTokens(string codeString)
        {
            var split = codeString.Split(" ");

            foreach (var splitPart in split)
            {
                var a = splitPart.Split(".");
                var b = splitPart.Split(".");
            }

            return null;
        }
    }
}
