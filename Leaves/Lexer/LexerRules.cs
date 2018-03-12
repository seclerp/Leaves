using System.Text.RegularExpressions;

namespace Leaves.Lexer
{
    public static class LexerRules
    {
        public static readonly Regex IdentifierRegex = new Regex(@"[A-Za-z_$][\w$]*");
        public static readonly string IdentifierLexemType = "IDENTIFIER";
    }
}