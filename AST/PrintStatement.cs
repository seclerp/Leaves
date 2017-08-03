using System;
using System.Runtime.ConstrainedExecution;
using leafs_lang.DataTypes;

namespace leafs_lang.AST {
    public class PrintStatement : IStatement {
        public LeafsValue Argument { get; set; }

        public PrintStatement(LeafsValue argument) {
            Argument = argument;
        }

        public IExpression Execute() {
            Console.WriteLine(">> " + Argument.Value.ToString());
            return null;
        }
    }
}