namespace Leaves.Parser

open Ast
open FParsec

module DeclarationExpression =

    let ws = spaces
    let pLet = pstring "let" .>> ws
    let pIdentifier = pipe3 ( pipe2 ( letter ( pchar '_' <|> pchar '$' ) ) ) many ( letter <|> digit <|> pchar '_' <|> pchar '$' ) 
    
