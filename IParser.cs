using System.Collections.Generic;
using System.Linq.Expressions;
using leafs_lang.AST;
using leafs_lang.DataTypes;

namespace leafs_lang {
    public interface IParser {
        List<IStatement> Parse(List<Token> tokens);
    }
}