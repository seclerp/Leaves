using System;
using System.Collections.Generic;
using System.Linq;
using LeafS.Complier;
using LeafS.DataTypes;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LeafS.AST
{
    public class AssignmentStatement : IStatement
    {
        public AssignmentStatement(string name, LeafsValue value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public LeafsValue Value { get; set; }

        public IExpression Execute()
        {
            GlobalValues.Items[Name] = Value;
            return null;
        }

        public void Emit(Context context)
        {
            var varIndex = -1;
            bool createNew = !context.Method.NameMappings.ContainsKey(Name);

            EmitHelper.AssignVar(context, Name, Value.Value, createNew);
        }
    }
}