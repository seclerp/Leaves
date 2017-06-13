using System;
using System.Collections.Generic;
using leafs_lang.DataTypes;
using leafs_lang.Exceptions;

namespace leafs_lang.AST {
    public class BinaryExpression : IExpression {
        public IExpression LeftOperand { get; set; }
        public IExpression RightOperand { get; set; }
        public string Operator { get; set; }
        public BinaryExpression(IExpression rightOperand, string op, IExpression leftOperand) {
            LeftOperand = leftOperand;
            Operator = op;
            RightOperand = rightOperand;
        }

        public LeafsValue Evaluate() {
            LeafsValue leftValue = LeftOperand.Evaluate();
            LeafsValue rightValue = RightOperand.Evaluate();

            switch (Operator) {
                case "+":
                    // TODO: If right operand is string - return string 
                    //if (rightValue)
                    return new LeafsValue("number", (float)rightValue.Value + (float)leftValue.Value);
                case "-":
                    return new LeafsValue("number", (float)rightValue.Value - (float)leftValue.Value);
                case "*":
                    return new LeafsValue("number", (float)rightValue.Value * (float)leftValue.Value);
                case "/":
                    return new LeafsValue("number", (float)rightValue.Value / (float)leftValue.Value);
                case "^":
                    return new LeafsValue("number", (float)Math.Pow((float)rightValue.Value, (float)leftValue.Value));
                case "%":
                    return new LeafsValue("number", (float)rightValue.Value % (float)leftValue.Value);
            }

            throw new LeafsSyntaxException($"Unknown type of binary operator: '{Operator}'");
        }
    }
}