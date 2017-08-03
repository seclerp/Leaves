using System.Collections.Generic;
using leafs_lang.AST;

namespace leafs_lang
{
    public interface IParser
    {
        List<IStatement> Parse(List<Token> tokens);
    }
}