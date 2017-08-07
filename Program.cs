using System;
using System.Diagnostics;
using System.Linq;

namespace LeafS
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

            //TODO DODO DODODODOOD TOTOTOOTDODOODOD
            do
            {
                current = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("-> ");
                var input = Console.ReadLine();
                Console.ForegroundColor = current;
                try
                {
                    var lexer = new Lexer.Lexer();
                    lexer.TokenDebug = true;
                    lexer.InitializeTokenDefinitions();

                    var parser = new Parser.Parser();
                    var tokens = lexer.Tokenize(input);
                    var result = parser.Parse(tokens.ToArray());

                    //string tokens = string.Join("\n\t", lexer.Tokenize(input).ToList());
                }
                catch (Exception e)
                {
                    throw e;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(e);
                    Console.ForegroundColor = current;
                }
            } while (true);

            Stopwatch watch = new Stopwatch();

            var compiler = new Complier.Compiler();
            watch.Start();

            compiler.Compile("test.exe", "code.lfs");

            watch.Stop();
            Console.WriteLine("Compiled.\n{0} ms\n{1} ticks", watch.ElapsedMilliseconds, watch.ElapsedTicks);
        }
    }
}