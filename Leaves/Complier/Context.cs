using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LeafS.Complier
{
    public class Context
    {
        public ILProcessor Processor { get; set; }
        public ModuleContext Module { get; set; }

        public TypeDefinition TypeDef { get; set; }
        public MethodContext Method { get; set; }
    }
}