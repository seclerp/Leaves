using leafs_lang.DataTypes;

namespace leafs_lang.AST {
    public class StringExpression : IExpression {
        public string Value { get; set; }
        public StringExpression(string value) {
            Value = value;
        }

        public LeafsValue Evaluate() {
            return new LeafsValue("string", Value);
        }
    }
}