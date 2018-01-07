using LeafS.DataTypes;

namespace LeafS.AST
{
    public interface IExpression
    {
        LeafsValue Evaluate();
    }
}