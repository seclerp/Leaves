using System.Text.RegularExpressions;

namespace leafs_lang {
    /// <summary>
    /// This class is used to match source code and token
    /// </summary>
    public class TokenDefinition {
        public bool IsIgnored { get; set; }
        public Regex Regex { get; set; }
        public Token.TokenType Type { get; set; }
        // If not equals -1 uses submask result and not full result
        public int UseMask { get; set; }

        public TokenDefinition(Regex regex, Token.TokenType type, bool isIgnored = false, int useMask = -1) {
            Regex = regex;
            Type = type;
            IsIgnored = isIgnored;
            UseMask = useMask;
        }
    }
}