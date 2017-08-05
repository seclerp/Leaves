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
            TypeReference typeDefinition;


            context.MethodDef.Body.Variables.Add(new VariableDefinition(Name, context.ModuleDef.Import(typeof(String))));
        }
    }
}