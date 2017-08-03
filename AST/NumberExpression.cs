using leafs_lang.DataTypes;

namespace leafs_lang.AST
{
    public class NumberExpression : IExpression
    {
        public NumberExpression(float value)
        {
            Value = value;
        }

        public float Value { get; set; }

        public LeafsValue Evaluate()
        {
            return new LeafsValue("number", Value);
        }
    }
}