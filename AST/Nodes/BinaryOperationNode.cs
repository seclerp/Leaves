using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    class BinaryOperationNode : INode
    {
        public INode Left { get; set; }
        public INode Right { get; set; }
        public string Operator;

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}