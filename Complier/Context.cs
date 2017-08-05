using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LeafS.Complier
{
    public class Context
    {
        public ILProcessor CurrentProcessor { get; private set; }
        public ModuleDefinition ModuleDef { get; private set; }
        public TypeDefinition TypeDef { get; private set; }
        public MethodDefinition MethodDef { get; private set; }

        public Context(ILProcessor currentProcessor, ModuleDefinition moduleDef, TypeDefinition typeDef, MethodDefinition methodDef)
        {
            CurrentProcessor = currentProcessor;
            ModuleDef = moduleDef;
            TypeDef = typeDef;
            MethodDef = methodDef;
        }
    }
}