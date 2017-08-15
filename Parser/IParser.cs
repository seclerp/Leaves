using LeafS.AST.Nodes;
using LeafS.Lexer;

namespace LeafS.Parser
{
    public interface IParser
    {
        INode Parse(Token[] tokens);
    }
}