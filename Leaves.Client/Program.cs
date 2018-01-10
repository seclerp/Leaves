using System;
using Leaves.Parser;


namespace Leaves.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Leaves live interpreter. Leave blank input to exit");
            do
            {
                Console.Write("-> ");
                var input = Console.ReadLine();
                Console.WriteLine($">> {MathExpression.Parse(input)}");
            } while (true);
        }
    }
}