using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    internal class BinaryOperationNode : INode
    {
        public string Operator;
        public INode Left { get; set; }
        public INode Right { get; set; }

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}