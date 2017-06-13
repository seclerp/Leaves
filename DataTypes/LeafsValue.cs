namespace leafs_lang.DataTypes {
    public class LeafsValue {
        public string Type { get; set; }
        public object Value { get; set; }

        public LeafsValue(string type, object value) {
            Type = type;
            Value = value;
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}