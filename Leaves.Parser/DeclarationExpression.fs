namespace Leaves.Parser

open Ast
open FParsec

module DeclarationExpression =

    let ws = spaces
    let pLet = pstring "let" .>> ws
    let pIdentifier =
        let isIdentifierFirstChar c = isLetter c || c = '_'
        let isIdentifierChar c = isLetter c || isDigit c || c = '_' || c = '$'

        many1Satisfy2L isIdentifierFirstChar isIdentifierChar "identifier"
        .>> ws // skips trailing whitespace
    let pTypeDeclaration =
        let pDoubleDot = satisfy ':' 



    let pDeclaration =
        many1Satisfy3L 
