using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LeafS.AST;
using LeafS.Exceptions;
using LeafS.Lexer;

namespace LeafS.Parser
{
    internal class Parser : IParser
    {
        private int _currentPosition;
        private int _size;
        private Token[] _tokens;

        public IStatement[] Parse(Token[] tokens)
        {
            _tokens = tokens;
            _size = _tokens.Count();
            var result = new List<IStatement>();
            while (!Match(TokenType.EndOfInput))
            {
                result.Add(Statement());
            }
            return result.ToArray();
        }

        public IStatement Statement()
        {
            var result = PrintStatement();
            return result;
        }

        public IStatement PrintStatement()
        {
            Console.WriteLine("Print");
            var current = Get(0);
            if (current.Type == TokenType.Print)
            {
                Match(TokenType.Print);
                return new PrintStatement(Expression().Evaluate());
            }
            return AssignmentStatement();
        }

        public IStatement AssignmentStatement()
        {
            Console.WriteLine("Assign");
            var current = Get(0);
            if (Match(TokenType.Word) && Get(0).Type == TokenType.Equal)
            {
                Consume(TokenType.Equal);
                return new AssignmentStatement(current.Value, Expression().Evaluate());
            }
            throw new LeafsException(current.Position, "Unknown statement");
        }

        public IExpression Expression()
        {
            Console.WriteLine("Expression");
            return Additive();
        }

        private IExpression Additive()
        {
            Console.WriteLine("Additive");
            var result = Mod();

            while (true)
            {
                if (Match(TokenType.Plus))
                {
                    result = new BinaryExpression(result, "+", Mod());
                    continue;
                }
                if (Match(TokenType.Minus))
                {
                    result = new BinaryExpression(result, "-", Mod());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Mod()
        {
            Console.WriteLine("Mod");
            var result = Multiplicative();

            while (true)
            {
                if (Match(TokenType.Percent))
                {
                    result = new BinaryExpression(result, "%", Multiplicative());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Multiplicative()
        {
            Console.WriteLine("Multiplicative");
            var result = Power();

            while (true)
            {
                if (Match(TokenType.Star))
                {
                    result = new BinaryExpression(result, "*", Power());
                    continue;
                }
                if (Match(TokenType.Slash))
                {
                    result = new BinaryExpression(result, "/", Power());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Power()
        {
            Console.WriteLine("Power");

            var result = Unary();

            while (true)
            {
                if (Match(TokenType.Power))
                {
                    result = new BinaryExpression(result, "^", Unary());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Unary()
        {
            Console.WriteLine("Unary");

            if (Match(TokenType.Minus)) return new UnaryExpression(Primary(), "-");

            return Primary();
        }

        private IExpression Primary()
        {
            Console.WriteLine("Primary");

            var current = Get(0);
            if (Match(TokenType.String)) return new StringExpression(current.Value);
            if (Match(TokenType.Word))
            {
                if (!GlobalValues.Items.ContainsKey(current.Value + ""))
                    throw new LeafsUnknownIdentifier(current.Position, current.Value);

                if (GlobalValues.Items[current.Value + ""].Type == "string")
                {
                    return new StringExpression((string) GlobalValues.Items[current.Value + ""].Value);
                }
                else if (GlobalValues.Items[current.Value + ""].Type == "nubmer")
                {
                    return new NumberExpression((float) GlobalValues.Items[current.Value + ""].Value);
                }
            }
            if (Match(TokenType.Number))
                return new NumberExpression(float.Parse(current.Value, CultureInfo.InvariantCulture.NumberFormat));
            if (Match(TokenType.LeftBrace))
            {
                var result = Expression();
                Match(TokenType.RightBrace);
                return result;
            }
            throw new LeafsSyntaxException(current.Position,
                $"Unknown expression: '{current.Value}' ({current.Position.Column}:{current.Position.Row})");
        }

        public Token Consume(TokenType type)
        {
            var current = Get(0);
            if (type != current.Type)
                throw new LeafsSyntaxException(current.Position, "Token " + current + " doesn't match " + type);
            _currentPosition++;
            return current;
        }

        private bool Match(TokenType type)
        {
            var current = Get(0);
            if (type != current.Type)
                return false;
            _currentPosition++;
            return true;
        }

        private Token Peek()
        {
            var current = Get(0);
            return current;
        }

        private void Skip(int amount = 1)
        {
            _currentPosition+=amount;
        }

        private Token Get(int relativePosition)
        {
            var position = _currentPosition + relativePosition;
            if (position >= _size)
                return new Token(TokenType.EndOfInput, "(end)");
            return _tokens[position];
        }
    }
}