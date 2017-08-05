using System;
using LeafS.DataTypes;
using LeafS.Exceptions;

namespace LeafS.AST
{
    public class BinaryExpression : IExpression
    {
        public BinaryExpression(IExpression rightOperand, string op, IExpression leftOperand)
        {
            LeftOperand = leftOperand;
            Operator = op;
            RightOperand = rightOperand;
        }

        public IExpression LeftOperand { get; set; }
        public IExpression RightOperand { get; set; }
        public string Operator { get; set; }

        public LeafsValue Evaluate()
        {
            var leftValue = LeftOperand.Evaluate();
            var rightValue = RightOperand.Evaluate();

            switch (Operator)
            {
                case "+":
                    // If right or left operand is string - return string 
                    if (rightValue.Type == "string" || leftValue.Type == "string")
                        return new LeafsValue("string", rightValue + leftValue.ToString());
                    return new LeafsValue("number", (float) rightValue.Value + (float) leftValue.Value);
                case "-":
                    return new LeafsValue("number", (float) rightValue.Value - (float) leftValue.Value);
                case "*":
                    return new LeafsValue("number", (float) rightValue.Value * (float) leftValue.Value);
                case "/":
                    return new LeafsValue("number", (float) rightValue.Value / (float) leftValue.Value);
                case "^":
                    return new LeafsValue("number",
                        (float) Math.Pow((float) rightValue.Value, (float) leftValue.Value));
                case "%":
                    return new LeafsValue("number", (float) rightValue.Value % (float) leftValue.Value);
            }

            throw new LeafsSyntaxException(null, $"Unknown type of binary operator: '{Operator}'");
        }
    }
}