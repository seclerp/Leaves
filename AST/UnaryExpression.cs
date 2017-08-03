using leafs_lang.DataTypes;
using leafs_lang.Exceptions;

namespace leafs_lang.AST {
    public class UnaryExpression : IExpression {
        public IExpression Operand { get; set; }
        public string Operator { get; set; }
        public UnaryExpression(IExpression operand, string op) {
            Operand = operand;
            Operator = op;
        }

        public LeafsValue Evaluate() {
            LeafsValue value = Operand.Evaluate();

            switch (Operator) {
                case "-":
                    return new LeafsValue("number", -(float)value.Value);
            }

            throw new LeafsSyntaxException(null, $"Unknown type of unary operator: '{Operator}' ");
        }
    }
}