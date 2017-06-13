using System;
using System.Collections.Generic;
using System.Globalization;
using leafs_lang.AST;
using leafs_lang.DataTypes;
using leafs_lang.Exceptions;

namespace leafs_lang {
    class Parser : IParser {
        private List<Token> _tokens;
        private int _currentPosition;
        private int _size;

        public List<IExpression> Parse(List<Token> tokens) {
            _tokens = tokens;
            _size = _tokens.Count;
            List<IExpression> result = new List<IExpression>();
            while (true) {
                if (Match(Token.TokenType.EndOfInput)) break;
                result.Add(Expression());
            }
            return result;
        }

        public IExpression Expression() {
            IExpression result = Additive();
            return result;
        }

        private IExpression Additive() {
            IExpression result = Mod();

            while (true) {
                if (Match(Token.TokenType.Plus)) {
                    result = new BinaryExpression(result, "+", Mod());
                    continue;
                }
                if (Match(Token.TokenType.Minus)) {
                    result = new BinaryExpression(result, "-", Mod());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Mod() {
            IExpression result = Multiplicative();

            while (true) {
                if (Match(Token.TokenType.Percent)) {
                    result = new BinaryExpression(result, "%", Multiplicative());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Multiplicative() {
            IExpression result = Power();

            while (true) {
                if (Match(Token.TokenType.Star)) {
                    result = new BinaryExpression(result, "*", Power());
                    continue;
                }
                if (Match(Token.TokenType.Slash)) {
                    result = new BinaryExpression(result, "/", Power());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Power() {
            IExpression result = Unary();

            while (true) {
                if (Match(Token.TokenType.Power)) {
                    result = new BinaryExpression(result, "^", Unary());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Unary() {
            if (Match(Token.TokenType.Minus)) {
                return new UnaryExpression(Primary(), "-");
            }

            return Primary();
        }

        private IExpression Primary() {
            Token current = Get(0);
            if (Match(Token.TokenType.Number)) {
                return new NumberExpression(float.Parse(current.Value, CultureInfo.InvariantCulture.NumberFormat));
            }
            if (Match(Token.TokenType.LeftBrace)) {
                IExpression result = Expression();
                Match(Token.TokenType.RightBrace);
                return result;
            }
            throw new LeafsSyntaxException($"Unknown expression: '{current.Value}' ({current.Position.Column}:{current.Position.Row})");
        }

        private bool Match(Token.TokenType type) {
            Token current = Get(0);
            if (type != current.Type)
                return false;
            _currentPosition++;
            return true;
        }

        private Token Peek() {
            Token current = Get(0);
            _currentPosition++;
            return current;
        }

        private Token Get(int relativePosition) {
            int position = _currentPosition + relativePosition;
            if (position >= _size)
                return new Token(Token.TokenType.EndOfInput, "(end)");
            return _tokens[position];
        }
    }
}