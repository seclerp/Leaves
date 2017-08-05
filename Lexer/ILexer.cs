using System.Collections.Generic;

namespace LeafS.Lexer
{
    public interface ILexer
    {
        void AddDefinition(TokenRule tokenDefinition);
        IEnumerable<Token> Tokenize(string source);
    }
}