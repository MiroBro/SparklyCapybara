using SparklyLanguage;

class Program
{
    static void Main(string[] args)
    {
        // Display the number of command line arguments.
        Console.WriteLine("Program Running");

        var tokenizer = new Tokenizer();
        tokenizer.Tokenize();

        ProgramQuit();
    }

    private static void ProgramQuit()
    {
        Console.WriteLine("Program Quitting");
    }
}