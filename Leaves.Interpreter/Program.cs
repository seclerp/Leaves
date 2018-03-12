using System;
using System.Drawing;
using Leaves.Lexer;
using Leaves.Lexer.Exception;

namespace Leaves.Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.WriteColored("Leaves live interpreter", ConsoleColor.Green);
            Console.WriteLine( "\n\nEnter '#exit' to exit progran.\n");
            
            string input = "";
            do
            {
                ConsoleUtils.WriteColored("=> ", ConsoleColor.Yellow);
                input = ConsoleUtils.ReadLineColored(ConsoleColor.White);
                var analyzer = new LexerAnalyzer(new InputReader(input));

                try
                {
                    analyzer.GetLexems().ForEach(l => Console.WriteLine(l));
                }
                catch (LexerException e)
                {
                    ConsoleUtils.WriteColored($"Error: {e.Message}", ConsoleColor.Red);
                    Console.WriteLine();
                }
                
                Console.WriteLine();
            } 
            while (input != "#exit");
        }
    }
}