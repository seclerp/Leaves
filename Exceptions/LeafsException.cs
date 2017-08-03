using System;

namespace leafs_lang.Exceptions
{
    public class LeafsException : Exception
    {
        public TokenPosition ExceptionPosition;

        public LeafsException(TokenPosition exceptionPosition, string message) : base(message)
        {
            ExceptionPosition = exceptionPosition;
        }

        public string GetPositionString()
        {
            return ExceptionPosition != null ? $"({ExceptionPosition.Row}:{ExceptionPosition.Column})" : "";
        }

        public override string ToString()
        {
            return $"[{GetType().Name}]: {Message} {GetPositionString()} \n\nC# stacktrace:\n{StackTrace}";
        }
    }
}