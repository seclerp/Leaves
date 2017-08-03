using System;
using System.Collections.Generic;
using System.Globalization;
using leafs_lang.AST;
using leafs_lang.Exceptions;

namespace leafs_lang
{
    internal class Parser : IParser
    {
        private int _currentPosition;
        private int _size;
        private List<Token> _tokens;

        public List<IStatement> Parse(List<Token> tokens)
        {
            _tokens = tokens;
            _size = _tokens.Count;
            var result = new List<IStatement>();
            while (true)
            {
                if (Match(Token.TokenType.EndOfInput)) break;
                result.Add(Statement());
            }
            return result;
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
            if (current.Type == Token.TokenType.Print)
            {
                Match(Token.TokenType.Print);
                return new PrintStatement(Expression().Evaluate());
            }
            return AssignmentStatement();
        }


        public IStatement AssignmentStatement()
        {
            Console.WriteLine("Assign");
            var current = Get(0);
            if (Match(Token.TokenType.Word) && Get(0).Type == Token.TokenType.Equal)
            {
                Consume(Token.TokenType.Equal);
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
                if (Match(Token.TokenType.Plus))
                {
                    result = new BinaryExpression(result, "+", Mod());
                    continue;
                }
                if (Match(Token.TokenType.Minus))
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
                if (Match(Token.TokenType.Percent))
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
                if (Match(Token.TokenType.Star))
                {
                    result = new BinaryExpression(result, "*", Power());
                    continue;
                }
                if (Match(Token.TokenType.Slash))
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
                if (Match(Token.TokenType.Power))
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

            if (Match(Token.TokenType.Minus)) return new UnaryExpression(Primary(), "-");

            return Primary();
        }

        private IExpression Primary()
        {
            Console.WriteLine("Primary");

            var current = Get(0);
            if (Match(Token.TokenType.String)) return new StringExpression(current.Value);
            if (Match(Token.TokenType.Word))
            {
                if (!GlobalValues.Items.ContainsKey(current.Value + ""))
                    throw new LeafsUnknownIdentifier(current.Position, current.Value);
                return new NumberExpression((float) GlobalValues.Items[current.Value + ""].Value);
            }
            if (Match(Token.TokenType.Number))
                return new NumberExpression(float.Parse(current.Value, CultureInfo.InvariantCulture.NumberFormat));
            if (Match(Token.TokenType.LeftBrace))
            {
                var result = Expression();
                Match(Token.TokenType.RightBrace);
                return result;
            }
            throw new LeafsSyntaxException(current.Position,
                $"Unknown expression: '{current.Value}' ({current.Position.Column}:{current.Position.Row})");
        }

        public Token Consume(Token.TokenType type)
        {
            var current = Get(0);
            if (type != current.Type)
                throw new LeafsSyntaxException(current.Position, "Token " + current + " doesn't match " + type);
            _currentPosition++;
            return current;
        }

        private bool Match(Token.TokenType type)
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
            _currentPosition++;
            return current;
        }

        private Token Get(int relativePosition)
        {
            var position = _currentPosition + relativePosition;
            if (position >= _size)
                return new Token(Token.TokenType.EndOfInput, "(end)");
            return _tokens[position];
        }
    }
}