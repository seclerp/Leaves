using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace leafs_lang {
    public class Lexer : ILexer {
        List<TokenDefinition> _definitions = new List<TokenDefinition>();
        
        public void AddDefinition(TokenDefinition tokenDefinition) {
            _definitions.Add(tokenDefinition);
        }

        public IEnumerable<Token> Tokenize(string source)
        {
            // Need for continous regexp matching (offset)
            int currentIndex = 0;
            // Current token line
            int currentLine = 1;
            // Current token column
            int currentColumn = 0;
            Regex endOfLineRegex = new Regex("\n");

            while (currentIndex < source.Length) {
                TokenDefinition matchedDefinition = null;
                int matchLength = 0;

                foreach (var rule in _definitions) {
                    var match = rule.Regex.Match(source, currentIndex);

                    if (match.Success && (match.Index - currentIndex) == 0) {
                        matchedDefinition = rule;
                        matchLength = match.Length;
                        break;
                    }
                }

                if (matchedDefinition == null) {
                    throw new Exception(
                        $"Unrecognized symbol '{source[currentIndex]}' {currentLine}:{currentColumn}).");
                }
                
                var value = source.Substring(currentIndex, matchLength);

                if (!matchedDefinition.IsIgnored)
                    yield return new Token(matchedDefinition.Type, value, currentLine, currentColumn);

                var endOfLineMatch = endOfLineRegex.Match(value);
                if (endOfLineMatch.Success) {
                    currentLine++;
                    currentColumn = value.Length - (endOfLineMatch.Index + endOfLineMatch.Length);
                } else {
                    currentColumn += matchLength;
                }

                currentIndex += matchLength;
            }

            yield return new Token(Token.TokenType.EndOfInput, "(end)",  currentLine, currentColumn);
        }
    }
}