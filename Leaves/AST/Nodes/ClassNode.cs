using System.Collections.Generic;
using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    internal class ClassNode : INode, IAccessControl
    {
        public string Name { get; set; }
        public List<INode> Members { get; set; }
        public AccessModifier Access { get; set; }

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}