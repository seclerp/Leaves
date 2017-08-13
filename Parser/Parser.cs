using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using LeafS.AST;
using LeafS.AST.Nodes;
using LeafS.Exceptions;
using LeafS.Lexer;

namespace LeafS.Parser
{
    internal class Parser : IParser
    {
        private int _currentPosition;
        private int _size;
        private Token[] _tokens;

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
            {
                return false;
            }

            return typeMatch;
        }

        #endregion

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
            if (Match(TokenType.Word, "class"))
                return Class();

            throw new LeafsSyntaxException(Get(0).Position, "Cannot parse this module");
        }

        /*
            access "class" name class_declaration 
			| "class" name class_declaration
        */
        private INode Class()
        {
            Skip();     // "class"

            if (!Check(TokenType.Word))
                return null;

            var name = Get(0).Value;
            Skip();

            if (!Check(TokenType.BlockStart))
                throw new LeafsSyntaxException(Get(0).Position, "Class start expected");

            var node = new ClassNode { Name = name };

            var members = new List<INode>();
            INode member;
            while ((member = Member()) != null)
            {
                members.Add(member);
            }

            node.Members = members;

            if (!Check(TokenType.BlockEnd))
                throw new LeafsSyntaxException(Get(0).Position, "Class end expected");

            Skip();     // Block end

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
            if (!Check(TokenType.Word))     // Type name
                return null;

            var node = new ClassFieldNode();
            node.TypeName = Get(0).Value;
            Skip();

            if (!Check(TokenType.Word))     // Name
                return null;

            node.Name = Get(0).Value;
            Skip();

            node.Expression = Expression();

            return node;
        }




        /*
            access identifier identifier "(" args ")" block_start 
			    block_statement
			block_end
        */
        private INode Method()
        {
            return null;
        }

        // TOOD
        private INode Expression()
        {
            throw new NotImplementedException();
        }
    }
}
 