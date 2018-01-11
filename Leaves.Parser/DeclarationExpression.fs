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
    
    let pDeclaration =
        choice [
            pVariableDeclaration
            //pFunctionDeclaration
            //pDataStructureDeclaration
            //pTypeDeclaration
        ]

    let Parse input = 
        match run pDeclaration input with
        | Success(result, _, _) -> VariableDeclaration result
        | Failure(errorMsg, _, _) -> printfn "%s" errorMsg

    (*let pDeclaration =
        many1Satisfy3L *)
