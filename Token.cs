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

        public TokenDefinition(Regex regex, Token.TokenType type, bool isIgnored = false) {
            Regex = regex;
            Type = type;
            IsIgnored = isIgnored;
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
            EndOfInput,
            Digit,
            Operator,
            Ident,
            Whitespace
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
            return Type.ToString() + ": (" + Value + ")";
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