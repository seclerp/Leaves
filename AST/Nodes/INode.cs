using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    public interface INode
    {
        TokenPosition CodePosition { get; set; }
        void Emit(Context context);
    }
}