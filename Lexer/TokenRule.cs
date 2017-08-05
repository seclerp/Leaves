using System.Text.RegularExpressions;

namespace LeafS.Lexer
{
    /// <summary>
    ///     This class is used to match source code and token
    /// </summary>
    public class TokenRule
    {
        public TokenRule(Regex regex, TokenType type, bool isIgnored = false, int useMask = -1)
        {
            Regex = regex;
            Type = type;
            IsIgnored = isIgnored;
            UseMask = useMask;
        }

        public Regex Regex { get; private set; }
        // If not equals -1 uses submask result and not full result
        public int UseMask { get; private set; }
        public TokenType Type { get; protected set; }
        public bool IsIgnored { get; protected set; }
    }
}