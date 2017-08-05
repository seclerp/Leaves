using System.Runtime.Remoting.Contexts;
using Mono.Cecil.Cil;
using Context = LeafS.Complier.Context;

namespace LeafS.AST
{
    public interface IStatement
    {
        IExpression Execute();
        void Emit(Context context);
    }
}