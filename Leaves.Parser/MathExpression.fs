namespace Leaves.Parser

open Ast
open FParsec

module MathExpression =
    let ws = spaces
    let stringWs s = pstring s .>> ws
    let parseNumber = pfloat .>> ws
    let operatorPrecedenceParser = new OperatorPrecedenceParser<float, unit, unit>()
    let expressionParser = operatorPrecedenceParser.ExpressionParser

    operatorPrecedenceParser.TermParser <- parseNumber <|> between (stringWs "(") (stringWs ")") expressionParser

    operatorPrecedenceParser.AddOperator(InfixOperator("+", ws, 1, Associativity.Left, (+)))
    operatorPrecedenceParser.AddOperator(InfixOperator("-", ws, 1, Associativity.Left, (-)))
    operatorPrecedenceParser.AddOperator(InfixOperator("*", ws, 2, Associativity.Left, (*)))
    operatorPrecedenceParser.AddOperator(InfixOperator("/", ws, 2, Associativity.Left, (/)))
    operatorPrecedenceParser.AddOperator(InfixOperator("%", ws, 2, Associativity.Left, (%)))
    operatorPrecedenceParser.AddOperator(InfixOperator("^", ws, 3, Associativity.Right, fun x y -> System.Math.Pow(x, y)))
    operatorPrecedenceParser.AddOperator(PrefixOperator("-", ws, 4, true, fun x -> -x))
    operatorPrecedenceParser.AddOperator(PrefixOperator("+", ws, 4, true, fun x -> x))

    let completeParser = ws >>. expressionParser .>> eof

    // Main parser
    let Parse input = 
        let parserResult = run completeParser input
        match parserResult with
        | Success (v, _, _) -> v
        | Failure (msg, _, _) -> printf "%s" msg; failwith msg