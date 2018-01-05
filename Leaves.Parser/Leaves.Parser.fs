namespace Leaves.Parser

open FParsec.CharParsers

module public Language =
    let Literal =
        | Number of float
        | String of string
        | Identifier of string

    

    let private test parser inputStr =
        match run parser inputStr with
        | Success(result, _, _) -> printf "Success: %A" result
        | Failure(errMsg, _, _) -> printf "Error: %s" errMsg
        
    // Parses float (2.25) and prints on screen
    let public ParseFloat inputStr =
        test pfloat inputStr

    // Parses float (2.25) and prints on screen
    let public ParseFloat inputStr =
        test pfloat inputStr

    