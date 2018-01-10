namespace Leaves.Parser
open FParsec

module Ast =

    type Grammar = Definition list
    type Definition = string * Expression
    type Expression = SingleExpression list

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

    type DeclarationExpression = 
        | VariableDeclaration
        | FunctionDeclaration
        | DataStructureDeclaration
        | TypeStructureDeclaration
    type VariableDeclaration =
        name of Identifier * 
        type of Identifier
        value of SingleExpression *
        valueType of ValueType
    type ValueType =
        | Default
        | Immutable
        | Lazy
        | LazyImmutable
    type FunctionDeclaration =
        name of Identifier * 
        arguments of VariableDeclaration list *
        returnType of Identifier *
        body of Expression *
        functionType of ValueType
    type DataStructureDeclaration =
        name of Identifier * 
        values of VariableDeclaration list *
        before of Expression *
        after of Expression
    type TypeStructureDeclaration =
        name of Identifier * 
        extends of Identifier *
        implements of Identifier *
        genericWhere of Identifier * Identifier *
        members of TypeMemberDeclaration list
    type TypeMemberDeclaration =
        | FieldDeclaration
        | MethodDeclaration
        | PropertyDeclaration
    type FieldDeclaration =
        accessLevel of AccessLevel *
        declaration of VariableDeclaration
    type MethodDeclaration =
        accessLevel of AccessLevel *
        declaration of FunctionDeclaration
    type PropertyDeclaration =
        accessLevel of AccessLevel *
        name of Identifier *
        defaultValue of SingleExpression *
        get of Expression *
        set of Expression
    type AccessLevel =
        | Public
        | Protected
        | Internal
        | ProtectedInternal
        | Private

    type AssignmentExpression =
        identifiers of Identifier list
        values of SingleExpression list

    type UnaryExpression = 
        operation of UnaryOperation *
        a of Identifier
    type UnaryOperation = 
        | Minus
        | GetLength
    
    type BinaryExpression = 
        a of Identifier * 
        operation of UnaryOperation * 
        b of Identifier
    type BinaryOperation = 
        | Plus
        | Minus
        | Star
        | Slash
        | Percent
        | Arrow

    type ConditionExpression =
        | IfExpression
        | IfElseExpression
        | OptionExpression
    type IfExpression =
        condition of SingleExpression *
        body of Expression
    type IfElseExpression =
        condition of SingleExpression *
        ifBody of Expression *
        elseBody of Expression
    type OptionExpression =
        identifier of Identifier *
        cases of WhenExpression list
    type WhenExpression =
        case of SingleExpression
        body of Expression
    
    type LoopExpression =
        | ForExpression
        | WhileExpression
        | DoWhileExpression
    type ForExpression =
        counterIdentifier of Identifier
        collection of SingleExpression
        body of Expression
    type WhileExpression =
        condition of SingleExpression *
        body of Expression  
    type DoWhileExpression =
        body of Expression *
        condition of SingleExpression

    type LoopControlExpression =
        | Next
        | Break

    type FunctionCallExpression =
        name of Identifier *
        parameters of SingleExpression list

    type FunctionControlExpression =
        | Return