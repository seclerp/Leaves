namespace Leaves.Parser
open FParsec

module Ast =

    type Identifier = string

    type SingleExpression = 
        | DeclarationExpression
        | AssignmentExpression
        | BinaryExpression
        | UnaryExpression
        | ConditionExpression
        | LoopExpression
        | LoopControlExpression
        | FunctionCallExpression
        | FunctionControlExpression
    type Expression = SingleExpression list
    
    type DeclarationExpression = 
        | VariableDeclaration
        | FunctionDeclaration
        | DataStructureDeclaration
        | TypeStructureDeclaration
    type ValueType =
        | Default
        | Immutable
        | Lazy
        | LazyImmutable
    type VariableDeclaration =
        ValueType *                 // immutable, lazy
        Identifier *                // name
        Identifier *                // type name
        SingleExpression            // value

    type FunctionDeclaration =
        ValueType *                                 // immutable, lazy
        Identifier *                                // name
        VariableDeclaration list *                  // arguments list
        Identifier *                                // return type name
        Expression                                  // body
    type DataStructureDeclaration =
        Identifier *                                // name
        VariableDeclaration list *                  // values
        Expression *                                // before insert
        Expression                                  // after insert
    type TypeMemberDeclaration =
        | FieldDeclaration
        | MethodDeclaration
        | PropertyDeclaration
    type AccessLevel =
        | Public
        | Protected
        | Internal
        | ProtectedInternal
        | Private
    type TypeStructureDeclaration =
        Identifier *                                // name
        Identifier *                                // extends type name
        Identifier *                                // implements type name
        Identifier * Identifier *                   // generic where
        TypeMemberDeclaration list                  // members
    type FieldDeclaration =
        AccessLevel *                               // access level
        VariableDeclaration                         // declaration
    type MethodDeclaration =
        AccessLevel *                               // access level
        FunctionDeclaration                         // declaration
    type PropertyDeclaration =
        AccessLevel *                               // access level
        Identifier *                                // name
        SingleExpression *                          // default value
        Expression *                                // get
        Expression                                  // set

    type AssignmentExpression =
        Identifier list *                           // identifiers
        SingleExpression list                       // values
        
    type UnaryOperation = 
        | Minus
        | GetLength
    type UnaryExpression = 
        UnaryOperation *                            // operation
        Identifier                                  // operand
        
    type BinaryOperation = 
        | Plus
        | Minus
        | Star
        | Slash
        | Percent
        | Arrow
        | Equals
        | NotEquals
        | GreaterThan
        | LessThan
        | GreaterEqualsThan
        | LessEqualsThan
    type BinaryExpression = 
        Identifier *                                // first operand
        BinaryOperation *                           // operation
        Identifier                                  // second operand

    type ConditionExpression =
        | IfExpression
        | IfElseExpression
        | OptionExpression
    type IfExpression =
        SingleExpression *                          // condition
        Expression                                  // body
    type IfElseExpression =
        SingleExpression *                          // condition
        Expression *                                // if body
        Expression                                  // else body
    type WhenExpression =
        SingleExpression *                          // case
        Expression                                  // body
    type OptionExpression =
        Identifier *                                // identifier
        WhenExpression list                         // cases
    
    type LoopExpression =
        | ForExpression
        | WhileExpression
        | DoWhileExpression
    type ForExpression =
        Identifier *                                // counter
        SingleExpression *                          // collection
        Expression                                  // body
    type WhileExpression =
        SingleExpression *                          // condition
        Expression                                  // body
    type DoWhileExpression =
        Expression *                                // body
        SingleExpression                            // condition

    type LoopControlExpression =
        | Next
        | Break

    type FunctionCallExpression =
        Identifier *                                // name
        SingleExpression list                       // parameters

    type FunctionControlExpression =
        | Return