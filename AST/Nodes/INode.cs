using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    public interface INode
    {
        void Emit(Context context);
        TokenPosition CodePosition { get; set; }
    }

    class PrintNode : INode
    {
        public INode Expression { get; set; }
        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}