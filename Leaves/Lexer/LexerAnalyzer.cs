using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using Leaves.Lexer.Exception;

namespace Leaves.Lexer
{
    public class LexerAnalyzer
    {
        private List<Func<LexerResult>> _lexers;
        private InputReader _inputReader;

        public LexerAnalyzer(InputReader inputReader)
        {
            _inputReader = inputReader;
            
            _lexers = new List<Func<LexerResult>>
            {
                TryIdentifier,
            };
        }

        public List<Lexem> GetLexems()
        {
            var lexems = new List<Lexem>();
            SkipWhiteSpace();
            while (_inputReader.Get(0) != '\0')
            {
                bool success = false;
                foreach (var lexer in _lexers)
                {
                    var result = lexer();
                    if (result.Success)
                    {
                        lexems.Add(result.Result);
                        success = true;
                        break;
                    }
                }
                if (!success)
                    throw new LexerException($"Unknown lexem near '{_inputReader.Get(0)}{_inputReader.Get(1)}{_inputReader.Get(2)}'");
                
                SkipWhiteSpace();
            }

            return lexems;
        }

        private void SkipWhiteSpace()
        {
            char current;
            while (char.IsWhiteSpace(_inputReader.Get()))
            {
                _inputReader.Skip();
            }
        }
        
        private LexerResult TryIdentifier()
        {
            if (_inputReader.TryRegex(LexerRules.IdentifierRegex, out var result))
            {
                _inputReader.Skip((uint) result.Length);
                return new LexerResult(new Lexem(LexerRules.IdentifierLexemType, result), true);
            }

            return LexerResult.Unsuccessfull;
        }
    }
}