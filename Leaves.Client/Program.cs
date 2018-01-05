using System;
using Leaves.Parser;


namespace Leaves.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Language.ParseFloat("2.25");
            Console.ReadKey();
        }
    }
}