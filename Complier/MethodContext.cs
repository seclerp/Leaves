using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LeafS.Complier
{
    public class MethodContext
    {
        public MethodDefinition Def { get; set; }
        public Dictionary<string, int> NameMappings { get; set; }
        public int VariablesCount { get; set; }
    }
}