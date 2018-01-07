using System.Text.RegularExpressions;

namespace LeafS.DataTypes
{
    public class LeafsValue
    {
        public LeafsValue(string type, object value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            switch (Type)
            {
                /*case "string":
                    return $"\"{Value}\"";
                    break;*/
                default:
                    return Regex.Unescape(Value.ToString());
                    break;
            }
        }
    }
}