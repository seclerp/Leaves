using System.Text.RegularExpressions;

namespace LeafS.Lexer
{
    internal class KeywordRule : TokenRule
    {
        public KeywordRule(string word, TokenType type)
            : base(new Regex($@"(\s|\W|^)({word})(\s|\W|$)"), type, false, 2)
        {
        }
    }
}