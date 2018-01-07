using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LeafS.Exceptions;

namespace LeafS.Lexer
{
    /// <summary>
    ///     Implementation of regular expression lexer for Leafs
    /// </summary>
    public class Lexer : ILexer
    {
        private readonly List<TokenRule> _definitions = new List<TokenRule>();

        public bool TokenDebug { get; set; }

        public void AddDefinition(TokenRule tokenDefinition)
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
                TokenRule matchedDefinition = null;
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
                        $"Unrecognized symbol '{source[currentIndex]}'");

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

            yield return new Token(TokenType.EndOfInput, "(end)", currentLine, currentColumn);
        }

        public void InitializeTokenDefinitions()
        {
            // TODO Add token regex and order it here

            // TODO Move to some serializable format

            // Ident
            AddDefinition(new TokenRule(new Regex(@"^[ \t]+"), TokenType.Ident));

            // Numbers, unary digits are not included
            AddDefinition(new TokenRule(new Regex(@"[0-9]+(\.[0-9]+)?"), TokenType.Number));

            // Keywords
            // Must be before words to avoid conflicts
            AddDefinition(new TokenRule(new Regex(@"(\s|^)(print)(\s|$)"), TokenType.Print, false, 2));

            // Word - starts from letter, letters, digits, _, `, $ 
            AddDefinition(new TokenRule(new Regex(@"\p{L}[\w\`\$]*"), TokenType.Word));

            // Comments - single line started from # and //, multiline by /* */
            AddDefinition(new TokenRule(new Regex(@"((\#|\/\/)(.*))(\n|\Z)"), TokenType.Comment, true));
            AddDefinition(new TokenRule(new Regex(@"\/\*(.*)\*\/"), TokenType.Comment, true));

            // Operators
            AddDefinition(new TokenRule(new Regex(@"\~="), TokenType.Equal));
            AddDefinition(new TokenRule(new Regex(@"\="), TokenType.Equal));
            AddDefinition(new TokenRule(new Regex(@"\+"), TokenType.Plus));
            AddDefinition(new TokenRule(new Regex(@"\-"), TokenType.Minus));
            AddDefinition(new TokenRule(new Regex(@"\*"), TokenType.Star));
            AddDefinition(new TokenRule(new Regex(@"\/"), TokenType.Slash));
            AddDefinition(new TokenRule(new Regex(@"\%"), TokenType.Percent));
            AddDefinition(new TokenRule(new Regex(@"\^"), TokenType.Power));

            // Strings
            AddDefinition(new TokenRule(new Regex(@"\" + "\"" + @"((?:[^\" + "\"" + @"\\]|\\.)*)\" + "\""),
                TokenType.String, false, 1));
            AddDefinition(new TokenRule(new Regex(@"\'((?:[^\'\\]|\\.)*)\'"), TokenType.String, false, 1));

            // Braces
            AddDefinition(new TokenRule(new Regex(@"\("), TokenType.LeftBrace));
            AddDefinition(new TokenRule(new Regex(@"\)"), TokenType.RightBrace));

            // Whitespace need to be prcoessed AFTER IDENT, because of conflicts with ident
            // it also must be ignored
            AddDefinition(new TokenRule(new Regex(@"\s"), TokenType.Whitespace, true));
        }
    }
}