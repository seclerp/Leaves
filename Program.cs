using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace leafs_lang
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Lexer lexer = new Lexer();
            
            lexer.AddDefinition(new TokenDefinition(new Regex(@"^[ \t]*"), Token.TokenType.Ident));
            lexer.AddDefinition(new TokenDefinition(new Regex(@"[0-9]"), Token.TokenType.Digit));
            lexer.AddDefinition(new TokenDefinition(new Regex(@"[-+*/\^]"), Token.TokenType.Operator));
            lexer.AddDefinition(new TokenDefinition(new Regex(@"\s"), Token.TokenType.Whitespace));

            // TODO
            do {
                Console.Write(">> ");
                string input = Console.ReadLine();
                string tokens = string.Join(", ", lexer.Tokenize(input).ToList());
                Console.Write("Tokens: " + tokens);
                Console.WriteLine();
            } while (true);
        }
    }
}