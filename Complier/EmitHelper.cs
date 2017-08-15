using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LeafS.Complier
{
    public static class EmitHelper
    {
        // Returns index in variables
        public static int AssignVar(Context context, string name, object value, bool newVar = false)
        {
            if (newVar)
            {
                TypeReference typeDefinition;
                context.Method.Def.Body.Variables.Add(
                    new VariableDefinition(name, context.Module.Def.Import(typeof(string))));
                context.Method.NameMappings[name] = context.Method.Def.Body.Variables.Count - 1;
                context.Method.VariablesCount++;
            }
            if (!context.Method.NameMappings.ContainsKey(name))
                throw new KeyNotFoundException(name);

            var index = context.Method.NameMappings[name];

            context.Processor.Emit(OpCodes.Ldstr, value.ToString());
            context.Processor.Emit(OpCodes.Stloc, index);

            return index;
        }
    }
}