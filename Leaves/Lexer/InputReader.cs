using System.Text.RegularExpressions;

namespace Leaves.Lexer
{
    public class InputReader
    {
        public string Input { get; }
        public int Position { get; private set; }
        
        public InputReader(string input)
        {
            Input = input;
            Position = 0;
        }

        /// <summary>
        ///     Get from input with given offset (Position + offset)
        /// </summary>
        /// <returns>Char in given position</returns>
        public char Get(int offset = 0)
        {
            return Position + offset >= Input.Length ||
                   Position + offset < 0
                ? '\0' 
                : Input[Position + offset];
        }

        /// <summary>
        ///     Read current and increments position
        /// </summary>
        /// <returns></returns>
        public char Read()
        {
            var result = Get();
            
            if (result != '\0')
                Position++;
            
            return result;
        }

        /// <summary>
        ///     Returns true if new part of string is match for regex
        /// </summary>
        /// <param name="regex">Regex to check</param>
        /// <param name="match">Result of match</param>
        /// <returns>True if success</returns>
        public bool TryRegex(Regex regex, out string match)
        {
            match = null;
            var resultOfMatch = regex.Match(Input, Position);
            
            if (!resultOfMatch.Success || resultOfMatch.Index != Position) 
                return false;
            
            match = resultOfMatch.Value;
            return true;
        }

        /// <summary>
        ///     Skips specified characters
        /// </summary>
        public void Skip(uint amount = 1)
        {
            Position += (int) amount;
        }
    }
}