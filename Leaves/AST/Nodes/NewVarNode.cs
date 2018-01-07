using LeafS.Complier;

namespace LeafS.AST.Nodes
{
    internal class NewVarNode : AssignNode
    {
        public string TypeName { get; set; }

        public void Emit(Context context)
        {
        }
    }
}