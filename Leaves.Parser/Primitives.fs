namespace Leaves.Parser

open Ast
open FParsec

module Primitives =

    let a = pstring "213" >>= fun x -> VariableDeclaration (x, x, x)
        
    let ws = spaces
    let pLet:Parser<string, unit> = pstring "let" .>> ws
    let pDoubleDot:Parser<char, unit> = pchar ':'  .>> ws
    let pEquals:Parser<char, unit> = pchar '=' .>> ws
    let pIdentifier =
        let isIdentifierFirstChar c = isLetter c || c = '_'
        let isIdentifierChar c = isLetter c || isDigit c || c = '_' || c = '$'

        many1Satisfy2L isIdentifierFirstChar isIdentifierChar "identifier"
        .>> ws // skips trailing whitespace
    let pTypeDeclaration =
        pDoubleDot >>. pIdentifier
    
        


    (*let pDeclaration =
        many1Satisfy3L *)
