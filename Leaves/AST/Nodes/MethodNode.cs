using System.Collections.Generic;
using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    internal class MethodNode : INode, IAccessControl
    {
        public string Name { get; set; }
        public List<INode> Statements { get; set; }
        public AccessModifier Access { get; set; }

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}