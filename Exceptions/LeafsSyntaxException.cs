using System;

namespace leafs_lang.Exceptions {
    public class LeafsSyntaxException : Exception {
        private string message;
        public override string Message => message;

        public LeafsSyntaxException(string message) : base(message) {
            this.message = message;
        }
    }
}