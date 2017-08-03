using System.Collections.Generic;

namespace leafs_lang {
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

            BlockStart,
            BlockEnd,

            Number,
            Word,
            Comment,

            Star,       // *
            Slash,      // /
            Plus,       // +
            Minus,      // -
            Power,      // ^
            Percent,    // %
            Equal,      // =
            TildaEqual, // ~=

            String,     // "some string", 'some string'

            RightBrace, // )
            LeftBrace,  // (

            Print,

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
}