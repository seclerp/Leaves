namespace LeafS.Testing
{
    public class UnitTests
    {
        public static void TestExpressions()
        {
            /*Console.WriteLine("Unit tests: \n");

            var expressionTests =
                new Dictionary<string, string>
                {
                    {"1 + 2", "3"},
                    {"1 *2 + 2-1", "3"},
                    {"-1 ^ 5 - -1 - 1 + 1", "0"},
                    {"-1^0", "1"},

                    {"1.23456789 ^ 9.87654321", "8,014041"},
                    {"((1+3+7/3) *45-17-289+347-137(76+6+7/829/23/329/3+78+28))", "8,014041"}
                };
            foreach (var expressionTest in expressionTests)
            {
                var lexer = new Lexer.Lexer();
                lexer.InitializeTokenDefinitions();
                var parser = new Parser.Parser();
                var tokens = lexer.Tokenize(expressionTest.Key);
                var result = parser.Parse(tokens.ToList())[0].Execute().Evaluate().ToString();

                if (result == expressionTest.Value)
                {
                    result = "OK -- " + expressionTest.Key;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    result =
                        $"Failed -- \n\t" +
                        $"Input:         \t{expressionTest.Key}\n\t" +
                        $"Program anwser:\t{result}\n\t" +
                        $"Corrent anwser:\t{expressionTest.Value}\n";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine(result);
            }
            Console.WriteLine();*/
        }
    }
}