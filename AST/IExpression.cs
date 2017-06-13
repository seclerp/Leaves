using System.Collections.Generic;
using leafs_lang.DataTypes;

namespace leafs_lang.AST {
    public interface IExpression {
        LeafsValue Evaluate();
    }
}