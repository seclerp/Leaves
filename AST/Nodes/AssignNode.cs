using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    internal class AssignNode : INode
    {
        public string Name { get; set; }
        public INode Expression { get; set; }

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}