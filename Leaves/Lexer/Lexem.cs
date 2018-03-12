using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;

namespace Leaves.Lexer
{
    public struct Lexem
    {
        public static Lexem Empty = new Lexem(null, null);
        
        public readonly string Type;
        public readonly string Value;

        public Lexem(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type}: '{Value}'";
        }

        public override bool Equals(object obj)
        {
            if (obj is Lexem lexemObj)
                return lexemObj.Type == Type && lexemObj.Value == Value;
            
            return false;
        }

        public bool Equals(Lexem other)
        {
            return string.Equals(Type, other.Type) && string.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
}