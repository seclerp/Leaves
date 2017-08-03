using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.AccessControl;
using leafs_lang.AST;
using leafs_lang.DataTypes;
using leafs_lang.Exceptions;

namespace leafs_lang {
    class Parser : IParser {
        private List<Token> _tokens;
        private int _currentPosition;
        private int _size;

        public List<IStatement> Parse(List<Token> tokens) {
            _tokens = tokens;
            _size = _tokens.Count;
            List<IStatement> result = new List<IStatement>();
            while (true) {
                if (Match(Token.TokenType.EndOfInput)) break;
                result.Add(Statement());
            }
            return result;
        }

        public IStatement Statement() {
            IStatement result = PrintStatement();
            return result;
        }

        public IStatement PrintStatement() {
            Console.WriteLine("Print");
            Token current = Get(0);
            if (current.Type == Token.TokenType.Print) {
                Match(Token.TokenType.Print);
                return new PrintStatement(Expression().Evaluate());
            }
            return AssignmentStatement();
        }


        public IStatement AssignmentStatement() {
            Console.WriteLine("Assign");
            Token current = Get(0);
            if (Match(Token.TokenType.Word) && Get(0).Type == Token.TokenType.Equal) {
                Consume(Token.TokenType.Equal);
                return new AssignmentStatement(current.Value, Expression().Evaluate());
            }
            throw new LeafsException(current.Position, "Unknown statement");
        }

        public IExpression Expression() {
            Console.WriteLine("Expression");
            return Additive();
        }

        private IExpression Additive() {
            Console.WriteLine("Additive");
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
            Console.WriteLine("Mod");
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
            Console.WriteLine("Multiplicative");
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
            Console.WriteLine("Power");

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
            Console.WriteLine("Unary");

            if (Match(Token.TokenType.Minus)) {
                return new UnaryExpression(Primary(), "-");
            }

            return Primary();
        }

        private IExpression Primary() {
            Console.WriteLine("Primary");

            Token current = Get(0);
            if (Match(Token.TokenType.String)) {
                return new StringExpression(current.Value.ToString());
            }
            if (Match(Token.TokenType.Word)) {
                if (!GlobalValues.Items.ContainsKey(current.Value + "")) {
                    throw new LeafsUnknownIdentifier(current.Position, current.Value);
                }
                return new NumberExpression((float)GlobalValues.Items[current.Value+""].Value);
            }
            if (Match(Token.TokenType.Number)) {
                return new NumberExpression(float.Parse(current.Value, CultureInfo.InvariantCulture.NumberFormat));
            }
            if (Match(Token.TokenType.LeftBrace)) {
                IExpression result = Expression();
                Match(Token.TokenType.RightBrace);
                return result;
            }
            throw new LeafsSyntaxException(current.Position, $"Unknown expression: '{current.Value}' ({current.Position.Column}:{current.Position.Row})");
        }

        public Token Consume(Token.TokenType type) {
            Token current = Get(0);
            if (type != current.Type) throw new LeafsSyntaxException(current.Position, "Token " + current + " doesn't match " + type);
            _currentPosition++;
            return current;
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