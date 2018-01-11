namespace Leaves.Parser
open System
open Ast
open Primitives
open FParsec

module DeclarationExpression =
    
    let pVariableDeclaration =
        let pOptionalTypeDeclarationParser = 
            opt pTypeDeclaration
                    
        pLet >>. tuple3 pIdentifier pOptionalTypeDeclarationParser ( pEquals >>. ws >>. pIdentifier ) .>> ws
    
    let pFunctionDeclaration =
        pLet >>. tuple3 pIdentifier ( many pIdentifier ) pIdentifier .>> ws

    let pDeclaration =
            pVariableDeclaration 
            <|> pFunctionDeclaration
            //pDataStructureDeclaration
            //pTypeDeclaration
        ]



    let Parse input = 
        match run pDeclaration input with
        | Success(result, _, _) -> 
            match result with
            | (name, _type, value) -> VariableDeclaration ( name, _type, value )
            | _ -> failwith "Unknown declaration"
        | Failure(errorMsg, _, _) -> failwith errorMsg

    (*let pDeclaration =
        many1Satisfy3L *)
