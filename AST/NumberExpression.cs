using leafs_lang.DataTypes;
using leafs_lang.Exceptions;

namespace leafs_lang.AST {
    public class NumberExpression : IExpression {
        public float Value { get; set; }
        public NumberExpression(float value) {
            Value = value;
        }

        public LeafsValue Evaluate() {
            return new LeafsValue("number", Value);
        }
    }
}