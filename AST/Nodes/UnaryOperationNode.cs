using LeafS.Complier;
using LeafS.Lexer;

namespace LeafS.AST.Nodes
{
    class UnaryOperationNode : INode
    {
        public INode Operand { get; set; }
        public string Operator;

        public void Emit(Context context)
        {
        }

        public TokenPosition CodePosition { get; set; }
    }
}