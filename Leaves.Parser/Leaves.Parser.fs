namespace Leaves.Parser

open FParsec.CharParsers

module public Language =

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

    