namespace leafs_lang {
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