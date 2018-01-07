using System.Collections.Generic;
using Mono.Cecil;

namespace LeafS.Complier
{
    public class MethodContext
    {
        public MethodDefinition Def { get; set; }
        public Dictionary<string, int> NameMappings { get; set; }
        public int VariablesCount { get; set; }
    }
}