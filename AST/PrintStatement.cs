using System;
using leafs_lang.DataTypes;

namespace leafs_lang.AST
{
    public class PrintStatement : IStatement
    {
        public PrintStatement(LeafsValue argument)
        {
            Argument = argument;
        }

        public LeafsValue Argument { get; set; }

        public IExpression Execute()
        {
            Console.WriteLine(">> " + Argument.Value);
            return null;
        }
    }
}