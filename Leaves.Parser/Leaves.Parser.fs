namespace Leaves.Parser

open FParsec

module public Language =
    let ParseInput input =
        let ws = spaces // skips any whitespace
        let str_ws s = pstring s >>. ws
        let number = pfloat .>> ws
        
        let opp = new OperatorPrecedenceParser<float, unit, unit>()
        let expr = opp.ExpressionParser
        opp.TermParser <- number <|> between (str_ws "(") (str_ws ")") expr
                
        
    