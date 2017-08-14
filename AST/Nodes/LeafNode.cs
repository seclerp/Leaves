using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    public abstract class SimpleNode : INode
    {
        public string Value;
        public virtual void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }

    class StringLiteralNode : SimpleNode
    {
        public override void Emit(Context context)
        {
        }
    }
}