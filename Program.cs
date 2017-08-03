using System;
using System.Linq;

namespace leafs_lang
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("~ Leafs 0.0.1 live interpreter ~");
            var current = Console.ForegroundColor;
            //UnitTests.TestExpressions();
            Console.ForegroundColor = current;

            // TODO DODO DODODODOOD TOTOTOOTDODOODOD
            do
            {
                current = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("-> ");
                var input = Console.ReadLine();
                Console.ForegroundColor = current;
                //try {
                var lexer = new Lexer();
                lexer.TokenDebug = true;
                lexer.InitializeTokenDefinitions();

                var parser = new Parser();
                var tokens = lexer.Tokenize(input);
                var result = parser.Parse(tokens.ToList());

                if (result.Count != 0) foreach (var expression in result) expression?.Execute();

                //string tokens = string.Join("\n\t", lexer.Tokenize(input).ToList());
                //} catch (Exception e) {
                //    throw e;
                //    Console.ForegroundColor = ConsoleColor.Yellow;
                //    Console.WriteLine(e);
                //    Console.ForegroundColor = current;
                //}
            } while (true);
        }
    }
}