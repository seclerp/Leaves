using LeafS.Complier;

namespace LeafS.AST.Nodes
{
    class NewVarNode : AssignNode
    {
        public string TypeName { get; set; }

        public void Emit(Context context)
        {
            
        }
    }
}