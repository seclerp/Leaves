using System.Collections.Generic;
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
    
    /// <summary>
    /// Represents simplest instance in lexer
    /// </summary>
    public class Token {
        /// <summary>
        /// Include all Leafs token types
        /// </summary>
        public enum TokenType {

            Whitespace,
            Ident,

            Number,

            Star,
            Slash,
            Plus,
            Minus,
            Power,
            Percent,

            String,

            RightBrace, // (
            LeftBrace,  // )

            EndOfInput,
        }

        public TokenType Type { get; set; }
        public TokenPosition Position { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value) {
            Type = type;
            Value = value;
        }

        public Token(TokenType type, string value, int column, int row) : this(type, value) {
            Position = new TokenPosition(column, row);
        }

        public override string ToString() {
            return Type + ": (" + Value + ")";
        }
    }

    /// <summary>
    /// Determines token position in source code
    /// </summary>
    public class TokenPosition {
        public int Column { get; set; }
        public int Row { get; set; }

        public TokenPosition(int column, int row) {
            Column = column;
            Row = row;
        }
    }
}