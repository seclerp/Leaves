using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using leafs_lang.Exceptions;

namespace leafs_lang {
    /// <summary>
    /// Implementation of regular expression lexer for Leafs
    /// </summary>
    public class Lexer : ILexer {
        List<TokenDefinition> _definitions = new List<TokenDefinition>();
        
        public void AddDefinition(TokenDefinition tokenDefinition) {
            _definitions.Add(tokenDefinition);
        }

        public void InitializeTokenDefinitions() {
            //TODO Add token rege and order it heer

            // Ident
            AddDefinition(new TokenDefinition(new Regex(@"^[ \t]+"), Token.TokenType.Ident));

            // Numbers, unary digits are not included
            AddDefinition(new TokenDefinition(new Regex(@"[0-9]+(\.[0-9]+)?"), Token.TokenType.Number));

            // Operators
            AddDefinition(new TokenDefinition(new Regex(@"\+"), Token.TokenType.Plus));
            AddDefinition(new TokenDefinition(new Regex(@"\-"), Token.TokenType.Minus));
            AddDefinition(new TokenDefinition(new Regex(@"\*"), Token.TokenType.Star));
            AddDefinition(new TokenDefinition(new Regex(@"\/"), Token.TokenType.Slash));
            AddDefinition(new TokenDefinition(new Regex(@"\%"), Token.TokenType.Percent));
            AddDefinition(new TokenDefinition(new Regex(@"\^"), Token.TokenType.Power));

            // Braces
            AddDefinition(new TokenDefinition(new Regex(@"\("), Token.TokenType.LeftBrace));
            AddDefinition(new TokenDefinition(new Regex(@"\)"), Token.TokenType.RightBrace));

            // Whitespace need to be prcoessed AFTER IDENT, because of conflicts with ident
            // it also must be ignored
            AddDefinition(new TokenDefinition(new Regex(@"\s"), Token.TokenType.Whitespace, true));
        }

        public IEnumerable<Token> Tokenize(string source)
        {
            // Need for continous regexp matching (offset)
            int currentIndex = 0;
            // Current token line
            int currentLine = 1;
            // Current token column
            int currentColumn = 1;
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
                    throw new LeafsSyntaxException($"Unrecognized symbol '({source[currentIndex]}' {currentLine}:{currentColumn})");
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