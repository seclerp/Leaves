using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    class ClassFieldNode : INode, IAccessControl
    {
        public AccessModifier Access { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public INode Expression{ get; set; }
        public void Emit(Context context)
        {
            
        }

        public TokenPosition CodePosition { get; set; }
    }
}