using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    public abstract class LeafNode : INode
    {
        public string Value;

        public virtual void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}