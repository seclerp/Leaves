namespace Leaves.Parser
open FParsec

module Ast =

    type Identifier = string

    type SingleExpression = 
        | DeclarationExpression of DeclarationExpression
        | AssignmentExpression of AssignmentExpression
        | BinaryExpression of BinaryExpression
        | UnaryExpression of UnaryExpression
        | ConditionExpression of ConditionExpression
        | LoopExpression of LoopExpression
        | LoopControlExpression of LoopControlExpression
        | FunctionCallExpression of FunctionCallExpression
        | FunctionControlExpression of FunctionControlExpression
    and Expression = SingleExpression list
    
    and DeclarationExpression = 
        | VariableDeclaration of VariableDeclaration
        | FunctionDeclaration of FunctionDeclaration
        | DataStructureDeclaration of DataStructureDeclaration
        | TypeStructureDeclaration of TypeStructureDeclaration
        
    and ValueType =
        | Default
        | Immutable
        | Lazy
        | LazyImmutable
        
    and VariableDeclaration =
        (*ValueType *  *)           // immutable, lazy
        Identifier *                // name
        Identifier option *         // type name
        Identifier            // value

    and FunctionDeclaration =
        ValueType *                                 // immutable, lazy
        Identifier *                                // name
        VariableDeclaration list *                  // arguments list
        Identifier *                                // return type name
        Expression                                  // body
    and DataStructureDeclaration =
        Identifier *                                // name
        VariableDeclaration list *                  // values
        Expression *                                // before insert
        Expression                                  // after insert
    and TypeMemberDeclaration =
        | FieldDeclaration of FieldDeclaration
        | MethodDeclaration of MethodDeclaration
        | PropertyDeclaration of PropertyDeclaration
    and AccessLevel =
        | Public
        | Protected
        | Internal
        | ProtectedInternal
        | Private
    and TypeStructureDeclaration =
        Identifier *                                // name
        Identifier *                                // extends type name
        Identifier *                                // implements type name
        Identifier * Identifier *                   // generic where
        TypeMemberDeclaration list                  // members
    and FieldDeclaration =
        AccessLevel *                               // access level
        VariableDeclaration                         // declaration
    and MethodDeclaration =
        AccessLevel *                               // access level
        FunctionDeclaration                         // declaration
    and PropertyDeclaration =
        AccessLevel *                               // access level
        Identifier *                                // name
        SingleExpression *                          // default value
        Expression *                                // get
        Expression                                  // set

    and AssignmentExpression =
        Identifier list *                           // identifiers
        SingleExpression list                       // values
        
    and UnaryOperation = 
        | Minus
        | GetLength
    and UnaryExpression = 
        UnaryOperation *                            // operation
        Identifier                                  // operand
        
    and BinaryOperation = 
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
    and BinaryExpression = 
        Identifier *                                // first operand
        BinaryOperation *                           // operation
        Identifier                                  // second operand

    and ConditionExpression =
        | IfExpression of IfExpression
        | IfElseExpression of IfElseExpression
        | OptionExpression of OptionExpression
    and IfExpression =
        SingleExpression *                          // condition
        Expression                                  // body
    and IfElseExpression =
        SingleExpression *                          // condition
        Expression *                                // if body
        Expression                                  // else body
    and WhenExpression =
        SingleExpression *                          // case
        Expression                                  // body
    and OptionExpression =
        Identifier *                                // identifier
        WhenExpression list                         // cases
    
    and LoopExpression =
        | ForExpression of ForExpression
        | WhileExpression of WhileExpression
        | DoWhileExpression of DoWhileExpression
    and ForExpression =
        Identifier *                                // counter
        SingleExpression *                          // collection
        Expression                                  // body
    and WhileExpression =
        SingleExpression *                          // condition
        Expression                                  // body
    and DoWhileExpression =
        Expression *                                // body
        SingleExpression                            // condition

    and LoopControlExpression =
        | Next
        | Break

    and FunctionCallExpression =
        Identifier *                                // name
        SingleExpression list                       // parameters

    and FunctionControlExpression =
        | Return