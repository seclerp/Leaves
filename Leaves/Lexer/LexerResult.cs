namespace Leaves.Lexer
{
    public class LexerResult
    {
        public static readonly LexerResult Unsuccessfull = new LexerResult(Lexem.Empty, false);
        
        public Lexem Result { get; }
        public bool Success { get; }

        public LexerResult(Lexem result, bool success)
        {
            Result = result;
            Success = success;
        }
    }
}