using System;
using System.Collections.Generic;
using System.Linq;

namespace leafs_lang.Testing {
    public class UnitTests {
        public static void TestExpressions() {
            Console.WriteLine("Unit tests: \n");
            
            Dictionary<string, string> expressionTests =
                new Dictionary<string, string>() {
                    {"1 + 2", "3"},
                    {"1 *2 + 2-1", "3"},
                    {"-1 ^ 5 - -1 - 1 + 1", "0"},
                    {"-1^0", "1"},

                    {"1.23456789 ^ 9.87654321", "8,014041"},
                };
            foreach (KeyValuePair<string, string> expressionTest in expressionTests) {
                Lexer lexer = new Lexer();
                lexer.InitializeTokenDefinitions();
                Parser parser = new Parser();
                var tokens = lexer.Tokenize(expressionTest.Key);
                var result = parser.Parse(tokens.ToList())[0].Evaluate().ToString();

                if (result == expressionTest.Value) {
                    result = "OK -- " + expressionTest.Key;
                    Console.ForegroundColor = ConsoleColor.Green;
                } else {
                    result = 
                        $"Failed -- \n\t" + 
                            $"Input:         \t{expressionTest.Key}\n\t" + 
                            $"Program anwser:\t{result}\n\t" + 
                            $"Corrent anwser:\t{expressionTest.Value}\n";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine(result);
            }
            Console.WriteLine();
        }
    }
}