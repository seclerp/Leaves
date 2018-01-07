using System;
using System.Collections.Generic;
using LeafS.AST;
using LeafS.AST.Nodes;
using LeafS.AST.Nodes.Leafs;
using LeafS.Exceptions;
using LeafS.Lexer;

namespace LeafS.Parser
{
    internal class Parser : IParser
    {
        private int _currentPosition;
        private int _size;
        private Token[] _tokens;

        public INode Parse(Token[] tokens)
        {
            _currentPosition = 0;
            _tokens = tokens;
            _size = tokens.Length;

            return Module();
        }

        /*  
            class 
		    | component	# TODO
		    | container	# TODO
        */
        private INode Module()
        {
            var moduleNode = new ModuleNode();
            moduleNode.Members = new List<INode>();

            while (Match(TokenType.Word, "class"))
                moduleNode.Members.Add(Class());

            return moduleNode;
        }

        /*
            access "class" name class_declaration 
			| "class" name class_declaration
        */
        private INode Class()
        {
            Skip(); // "class"

            if (!Check(TokenType.Word))
                return null;

            var name = Current().Value;
            Skip();

            if (!Check(TokenType.BlockStart))
                throw new LeafsSyntaxException(Current().Position, "Class start expected");

            var node = new ClassNode {Name = name};

            // TODO Add access modifier
            node.Access = AccessModifier.Public;

            var members = new List<INode>();
            INode member;
            while ((member = Member()) != null)
                members.Add(member);

            node.Members = members;

            if (!Check(TokenType.BlockEnd))
                throw new LeafsSyntaxException(Current().Position, "Class end expected");

            Skip(); // Block end

            return node;
        }


        /*
            field
			| property # TODO
			| method
			| constructor # TODO
        */
        private INode Member()
        {
            INode result;
            if ((result = Field()) != null)
                return result;

            if ((result = Method()) != null)
                return result;

            return null;
        }

        /*
            access identifier identifier ";"
			| access identifier identifier "=" expression ";"
        */
        private INode Field()
        {
            if (!Check(TokenType.Word)) // Type name
                return null;

            var node = new ClassFieldNode();
            node.TypeName = Current().Value;
            Skip();

            if (!Check(TokenType.Word)) // Name
                return null;

            node.Name = Current().Value;
            Skip();

            node.Expression = Expression();

            return node;
        }


        /*
            access identifier identifier "(" args ")" block_start 
			    block_statement
			block_end
            | identifier identifier "(" args ")" block_start 
			    block_statement
			block_end
        */
        private INode Method()
        {
            if (!Check(TokenType.Word)) // Type name
                return null;

            var node = new ClassFieldNode();
            node.TypeName = Current().Value;
            Skip();

            if (!Check(TokenType.Word)) // Name
                return null;

            node.Name = Current().Value;
            Skip();

            node.Expression = Expression();

            return node;
            Skip();     // block end
        }


        private INode Statement()
        {
            INode result;
            while ((result = Field()) != null)
                return result;
            return null;
        }

        /*
            term
			| expression PLUS term
			| expression MINUS term
        */
        private INode Expression()
        {
            INode result;
            if ((result = Term()) != null)
                return result;

            if ((result = Expression()) != null)
            {
                if (!Check(TokenType.Plus) && !Check(TokenType.Minus))
                    throw new LeafsSyntaxException(Current().Position, "Binary operator expected");

                var op = Current().Value;
                Skip();

                var node = new BinaryOperationNode
                {
                    Operator = op,
                    Left = result
                };

                if ((result = Term()) == null)
                    throw new LeafsSyntaxException(Current().Position, "Right expression expected");

                node.Right = result;

                return node;
            }

            return null;
        }

        /*
			factor
			| term STAR factor
			| term SLASH factor
			| term PERCENT factor
        */
        private INode Term()
        {
            INode result;
            if ((result = Factor()) != null)
                return result;

            if ((result = Term()) != null)
            {
                if (!Check(TokenType.Star) && !Check(TokenType.Slash) && !Check(TokenType.Percent))
                    throw new LeafsSyntaxException(Current().Position, "Expression expected");

                var op = Current().Value;
                Skip();

                var node = new BinaryOperationNode
                {
                    Operator = op,
                    Left = result
                };

                if ((result = Factor()) == null)
                    throw new LeafsSyntaxException(Current().Position, "Expression expected");

                node.Right = result;

                return node;
            }

            return null;
        }

        /*
            primary
			| MINUS factor
			| PLUS factor
        */
        private INode Factor()
        {
            INode result;
            if ((result = Primary()) != null)
                return result;

            if (Check(TokenType.Plus) || Check(TokenType.Minus))
            {
                var op = Current().Value;
                Skip();

                var node = new UnaryOperationNode
                {
                    Operator = op
                };

                if ((result = Factor()) == null)
                    throw new LeafsSyntaxException(Current().Position, "Expression expected");

                node.Operand = result;

                return node;
            }

            throw new LeafsSyntaxException(Current().Position, "");
        }

        /*
            IDENTIFIER
			| NUMBER
			| STRING
			| LPAREN expression RPAREN 
         */
        private INode Primary()
        {
            if (Check(TokenType.Word))
            {
                var node = new IdentifierNode();
                node.Value = Current().Value;
                Skip();
                return node;
            }

            if (Check(TokenType.Number))
            {
                var node = new NumberLiteralNode();
                node.Value = Current().Value;
                Skip();
                return node;
            }

            if (Check(TokenType.String))
            {
                var node = new StringLiteralNode();
                node.Value = Current().Value;
                Skip();
                return node;
            }

            if (Check(TokenType.LeftBrace))
            {
                Skip();     // (
                var node = Expression();
                if (node == null)
                    throw new LeafsSyntaxException(Current().Position, "Expression expected");

                CheckAndSkip(TokenType.RightBrace, "')' expected");
            }

            // Vsjo ochen' ploho ;c
            throw new LeafsSyntaxException(Current().Position, "Unexpected token");
        }

        #region Token handlers

        private Token Get(int offset)
        {
            return _currentPosition + offset >= _size - 1
                ? _tokens[_currentPosition + offset]
                : new Token(TokenType.EndOfInput, null);
        }

        private Token Current()
        {
            return Get(0);
        }

        // Move token cursor and return new token
        private Token Move(int amount)
        {
            _currentPosition += amount;
            return Get(0);
        }

        // Move cursor to next token and return token
        private Token Next()
        {
            return Move(1);
        }

        private void Skip()
        {
            Next();
        }

        // Check works with current token
        private bool Check(TokenType type, string message = null)
        {
            var current = Current();
            if (current.Type != type)
            {
                if (!string.IsNullOrEmpty(message))
                    throw new LeafsSyntaxException(current.Position, message);

                return false;
            }
            return true;
        }

        private bool CheckAndSkip(TokenType type, string message = null)
        {
            var result = Check(type, message);
            if (result)
                Skip();
            return result;
        }

        private bool Match(TokenType type, string value)
        {
            var typeMatch = Check(type);

            if (Get(0).Value != value)
                return false;

            return typeMatch;
        }

        #endregion
    }
}
 