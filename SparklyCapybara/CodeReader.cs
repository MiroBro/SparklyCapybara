using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklyCapybara
{
    public static class CodeReader
    {
        public static string GetCodeText()
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

            return sb.ToString();
        }
    }
}
