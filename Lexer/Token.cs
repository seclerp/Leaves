namespace LeafS.Lexer
{
    /// <summary>
    ///     Represents simplest instance in lexer
    /// </summary>
    public class Token
    {
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public Token(TokenType type, string value, int column, int row) : this(type, value)
        {
            Position = new TokenPosition(column, row);
        }

        public TokenType Type { get; set; }
        public TokenPosition Position { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Type + ": (" + Value + ")";
        }
    }
}