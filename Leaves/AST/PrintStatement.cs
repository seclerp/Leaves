using System;
using LeafS.Complier;
using LeafS.DataTypes;
using Mono.Cecil.Cil;

namespace LeafS.AST
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

        public void Emit(Context context)
        {
            context.Processor.Emit(OpCodes.Ldstr, Argument.Value.ToString());
            context.Processor.Emit(OpCodes.Call,
                context.Module.Def.Import(typeof(Console).GetMethod("WriteLine", new[] {typeof(string)})));
        }
    }
}