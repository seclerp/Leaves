using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using leafs_lang.Testing;

namespace leafs_lang
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("~ Leafs 0.0.1 live interpreter ~");
            Console.WriteLine("~ Leafs 0.0.1 live interpreter ~");
            ConsoleColor current = Console.ForegroundColor;
            //UnitTests.TestExpressions();
            Console.ForegroundColor = current;

            // TODO DODO DODODODOOD TOTOTOOTDODOODOD
            do {
                current = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("-> ");
                string input = Console.ReadLine();
                Console.ForegroundColor = current;
                try {
                    Lexer lexer = new Lexer();
                    lexer.InitializeTokenDefinitions();

                    Parser parser = new Parser();
                    var tokens = lexer.Tokenize(input);
                    var result = parser.Parse(tokens.ToList());

                    Console.Write(">> ");
                    foreach (var expression in result) {
                        Console.WriteLine(expression.Evaluate());
                    }

                    //string tokens = string.Join("\n\t", lexer.Tokenize(input).ToList());
                } catch (Exception e) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(e.GetType().Name + ": " + e.Message);
                    Console.ForegroundColor = current;
                }
            } while (true);
        }
    }
}