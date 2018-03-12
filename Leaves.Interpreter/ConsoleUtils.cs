using System;

namespace Leaves.Interpreter
{
    public class ConsoleUtils
    {
        public static void WriteColored(string text, ConsoleColor color)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = current;
        }
        
        public static void WriteLineColored(string text, ConsoleColor color)
        {
            WriteColored(text + '\n', color);
        }
        
        public static int ReadColored(ConsoleColor color)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            var result = Console.Read();
            Console.ForegroundColor = current;
            return result;
        }
        
        public static string ReadLineColored(ConsoleColor color)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            var result = Console.ReadLine();
            Console.ForegroundColor = current;
            return result;
        }
    }
}