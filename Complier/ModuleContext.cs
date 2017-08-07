using Mono.Cecil;

namespace LeafS.Complier
{
    public class ModuleContext
    {
        public ModuleDefinition Def;
        public TypesCache Types { get; set; }
    }
}