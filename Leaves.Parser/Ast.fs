namespace Leaves.Parser
open FParsec

module Ast =

    let this s = charReturn '@' s
    
    type Grammar = Definition list
    
    and  Definition = Def of string * Expression
    
    and  Expression = SingleExpression list
    
    and  SingleExpression = 
            // | DeclarationExpression of DeclarationExpression
            | MathExpression
            // | 