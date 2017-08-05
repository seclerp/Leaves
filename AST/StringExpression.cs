using LeafS.DataTypes;

namespace LeafS.AST
{
    public class StringExpression : IExpression
    {
        public StringExpression(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public LeafsValue Evaluate()
        {
            return new LeafsValue("string", Value);
        }
    }
}