using System.Collections.Generic;
using LeafS.AST;
using LeafS.Lexer;

namespace LeafS.Parser
{
    public interface IParser
    {
        IStatement[] Parse(Token[] tokens);
    }
}