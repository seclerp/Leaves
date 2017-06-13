using System.Collections.Generic;

namespace leafs_lang {
    public interface ILexer {
        void AddDefinition(TokenDefinition tokenDefinition);
        IEnumerable<Token> Tokenize(string source);
    }
}