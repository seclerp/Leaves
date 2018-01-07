using LeafS.Complier;

namespace LeafS.AST
{
    public interface IStatement
    {
        IExpression Execute();
        void Emit(Context context);
    }
}