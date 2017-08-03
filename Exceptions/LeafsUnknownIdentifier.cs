using System;

namespace leafs_lang.Exceptions {
    public class LeafsUnknownIdentifier : LeafsException {

        public LeafsUnknownIdentifier(TokenPosition position, string identifierName) : base(position, $"Unknown identifier: '{identifierName}'") {
        }
    }
}