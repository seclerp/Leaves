namespace LeafS.Lexer
{
    /// <summary>
    ///     Include all Leafs token types
    /// </summary>
    public enum TokenType
    {
        Whitespace,
        Ident,

        BlockStart,
        BlockEnd,

        Number,
        Word,
        Comment,

        Star, // *
        Slash, // /
        Plus, // +
        Minus, // -
        Power, // ^
        Percent, // %
        Equal, // =
        TildaEqual, // ~=

        String, // "some string", 'some string'

        RightBrace, // )
        LeftBrace, // (

        Print,

        EndOfInput
    }
}