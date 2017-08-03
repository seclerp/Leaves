using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using leafs_lang.Exceptions;

namespace leafs_lang
{
    /// <summary>
    ///     Implementation of regular expression lexer for Leafs
    /// </summary>
    public class Lexer : ILexer
    {
        private readonly List<TokenDefinition> _definitions = new List<TokenDefinition>();

        public bool TokenDebug { get; set; }

        public void AddDefinition(TokenDefinition tokenDefinition)
        {
            _definitions.Add(tokenDefinition);
        }

        public IEnumerable<Token> Tokenize(string source)
        {
            // Need for continous regexp matching (offset)
            var currentIndex = 0;
            // Current token line
            var currentLine = 1;
            // Current token column
            var currentColumn = 1;
            var endOfLineRegex = new Regex("\n");

            while (currentIndex < source.Length)
            {
                TokenDefinition matchedDefinition = null;
                var matchLength = 0;
                Match match = null;

                foreach (var rule in _definitions)
                {
                    match = rule.Regex.Match(source, currentIndex);

                    if (match.Success && match.Index - currentIndex == 0)
                    {
                        matchedDefinition = rule;
                        matchLength = match.Length;
                        break;
                    }
                }

                if (matchedDefinition == null)
                    throw new LeafsSyntaxException(new TokenPosition(currentLine, currentColumn),
                        $"Unrecognized symbol '({source[currentIndex]}'");

                var value = "";
                if (matchedDefinition.UseMask == -1) value = source.Substring(currentIndex, matchLength);
                else
                    value = source.Substring(match.Groups[matchedDefinition.UseMask].Index,
                        match.Groups[matchedDefinition.UseMask].Length);

                if (!matchedDefinition.IsIgnored)
                {
                    if (TokenDebug) Console.WriteLine($"[{matchedDefinition.Type}]: {value}");
                    yield return new Token(matchedDefinition.Type, value, currentLine, currentColumn);
                }

                var endOfLineMatch = endOfLineRegex.Match(value);
                if (endOfLineMatch.Success)
                {
                    currentLine++;
                    currentColumn = value.Length - (endOfLineMatch.Index + endOfLineMatch.Length);
                }
                else
                {
                    currentColumn += matchLength;
                }

                currentIndex += matchLength;
            }

            yield return new Token(Token.TokenType.EndOfInput, "(end)", currentLine, currentColumn);
        }

        public void InitializeTokenDefinitions()
        {
            //TODO Add token regex and order it here

            // TODO Move to some serializable format

            // Ident
            AddDefinition(new TokenDefinition(new Regex(@"^[ \t]+"), Token.TokenType.Ident));

            // Numbers, unary digits are not included
            AddDefinition(new TokenDefinition(new Regex(@"[0-9]+(\.[0-9]+)?"), Token.TokenType.Number));

            // Keywords
            // Must be before words to avoid conflicts
            AddDefinition(new TokenDefinition(new Regex(@"(\s|^)(print)(\s|$)"), Token.TokenType.Print, false, 2));

            // Word - starts from letter, letters, digits, _, `, $ 
            AddDefinition(new TokenDefinition(new Regex(@"\p{L}[\w\`\$]*"), Token.TokenType.Word));

            // Comments - single line started from # and //, multiline by /* */
            AddDefinition(new TokenDefinition(new Regex(@"((\#|\/\/)(.*))(\n|\Z)"), Token.TokenType.Comment, true));
            AddDefinition(new TokenDefinition(new Regex(@"\/\*(.*)\*\/"), Token.TokenType.Comment, true));

            // Operators
            AddDefinition(new TokenDefinition(new Regex(@"\~="), Token.TokenType.Equal));
            AddDefinition(new TokenDefinition(new Regex(@"\="), Token.TokenType.Equal));
            AddDefinition(new TokenDefinition(new Regex(@"\+"), Token.TokenType.Plus));
            AddDefinition(new TokenDefinition(new Regex(@"\-"), Token.TokenType.Minus));
            AddDefinition(new TokenDefinition(new Regex(@"\*"), Token.TokenType.Star));
            AddDefinition(new TokenDefinition(new Regex(@"\/"), Token.TokenType.Slash));
            AddDefinition(new TokenDefinition(new Regex(@"\%"), Token.TokenType.Percent));
            AddDefinition(new TokenDefinition(new Regex(@"\^"), Token.TokenType.Power));

            // Strings
            AddDefinition(new TokenDefinition(new Regex(@"\" + "\"" + @"((?:[^\" + "\"" + @"\\]|\\.)*)\" + "\""),
                Token.TokenType.String, false, 1));
            AddDefinition(new TokenDefinition(new Regex(@"\'((?:[^\'\\]|\\.)*)\'"), Token.TokenType.String, false, 1));

            // Braces
            AddDefinition(new TokenDefinition(new Regex(@"\("), Token.TokenType.LeftBrace));
            AddDefinition(new TokenDefinition(new Regex(@"\)"), Token.TokenType.RightBrace));

            // Whitespace need to be prcoessed AFTER IDENT, because of conflicts with ident
            // it also must be ignored
            AddDefinition(new TokenDefinition(new Regex(@"\s"), Token.TokenType.Whitespace, true));
        }
    }
}