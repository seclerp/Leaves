using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    public interface INode
    {
        void Emit(Context context);
        TokenPosition CodePosition { get; set; }
    }
}