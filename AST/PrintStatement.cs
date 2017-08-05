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
            context.CurrentProcessor.Emit(OpCodes.Ldstr, (string)Argument.Value);
            context.CurrentProcessor.Emit(OpCodes.Call, context.ModuleDef.Import(typeof(Console).GetMethod("WriteLine", new [] { typeof( string ) })));
        }
    }
}