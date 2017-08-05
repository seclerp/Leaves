using LeafS.Lexer;

namespace LeafS.Exceptions
{
    public class LeafsSyntaxException : LeafsException
    {
        public LeafsSyntaxException(TokenPosition position, string message) : base(position, message)
        {
        }
    }
}