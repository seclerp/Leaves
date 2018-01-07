using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    internal class PrintNode : INode
    {
        public INode Expression { get; set; }

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}