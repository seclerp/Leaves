using leafs_lang.DataTypes;

namespace leafs_lang.AST
{
    public class AssignmentStatement : IStatement
    {
        public AssignmentStatement(string name, LeafsValue value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public LeafsValue Value { get; set; }

        public IExpression Execute()
        {
            GlobalValues.Items[Name] = Value;
            return null;
        }
    }
}