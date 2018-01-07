using LeafS.DataTypes;
using LeafS.Exceptions;

namespace LeafS.AST
{
    public class UnaryExpression : IExpression
    {
        public UnaryExpression(IExpression operand, string op)
        {
            Operand = operand;
            Operator = op;
        }

        public IExpression Operand { get; set; }
        public string Operator { get; set; }

        public LeafsValue Evaluate()
        {
            var value = Operand.Evaluate();

            switch (Operator)
            {
                case "-":
                    return new LeafsValue("number", -(float) value.Value);
            }

            throw new LeafsSyntaxException(null, $"Unknown type of unary operator: '{Operator}' ");
        }
    }
}