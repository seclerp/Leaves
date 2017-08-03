using System;

namespace leafs_lang.Exceptions {
    public class LeafsSyntaxException : LeafsException {
        public LeafsSyntaxException(TokenPosition position, string message) : base(position, message) {
        }
    }
}