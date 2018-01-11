namespace Leaves.Parser
open FParsec

module Ast =

    type Identifier = string

    type SingleExpression = 
        | DeclarationExpression of DeclarationExpressionType
        | AssignmentExpression of AssignmentExpressionType
        | BinaryExpression of BinaryExpressionType
        | UnaryExpression of UnaryExpressionType
        | ConditionExpression of ConditionExpressionType
        | LoopExpression of LoopExpressionType
        | LoopControlExpression of LoopControlExpressionType
        | FunctionCallExpression of FunctionCallExpressionType
        | FunctionControlExpression of FunctionControlExpressionType
    and Expression = SingleExpression list
    
    and DeclarationExpressionType = 
        | VariableDeclaration of VariableDeclarationType
        | FunctionDeclaration of FunctionDeclarationType
        | DataStructureDeclaration of DataStructureDeclarationType
        | TypeStructureDeclaration of TypeStructureDeclarationType
        
    and ValueType =
        | Default
        | Immutable
        | Lazy
        | LazyImmutable
        
    and VariableDeclarationType =
        (*ValueType *  *)           // immutable, lazy
        Identifier *                // name
        Identifier option *         // type name
        Identifier            // value

    and FunctionDeclarationType =
        (*ValueType *  *)                                // immutable, lazy
        Identifier *                                // name
        VariableDeclarationType list *                  // arguments list
        Identifier *                                // return type name
        Expression                                  // body
    and DataStructureDeclarationType =
        Identifier *                                // name
        VariableDeclarationType list *                  // values
        Expression *                                // before insert
        Expression                                  // after insert
    and TypeMemberDeclarationType =
        | FieldDeclaration of FieldDeclarationType
        | MethodDeclaration of MethodDeclarationType
        | PropertyDeclaration of PropertyDeclarationType
    and AccessLevel =
        | Public
        | Protected
        | Internal
        | ProtectedInternal
        | Private
    and TypeStructureDeclarationType =
        Identifier *                                // name
        Identifier *                                // extends type name
        Identifier *                                // implements type name
        Identifier * Identifier *                   // generic where
        TypeMemberDeclarationType list                  // members
    and FieldDeclarationType =
        AccessLevel *                               // access level
        VariableDeclarationType                         // declaration
    and MethodDeclarationType =
        AccessLevel *                               // access level
        FunctionDeclarationType                         // declaration
    and PropertyDeclarationType =
        AccessLevel *                               // access level
        Identifier *                                // name
        SingleExpression *                          // default value
        Expression *                                // get
        Expression                                  // set

    and AssignmentExpressionType =
        Identifier list *                           // identifiers
        SingleExpression list                       // values
        
    and UnaryOperationType = 
        | Minus
        | GetLength
    and UnaryExpressionType = 
        UnaryOperationType *                            // operation
        Identifier                                  // operand
        
    and BinaryOperationType = 
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
    and BinaryExpressionType = 
        Identifier *                                // first operand
        BinaryOperationType *                           // operation
        Identifier                                  // second operand

    and ConditionExpressionType =
        | IfExpression of IfExpressionType
        | IfElseExpression of IfElseExpressionType
        | OptionExpression of OptionExpressionType
    and IfExpressionType =
        SingleExpression *                          // condition
        Expression                                  // body
    and IfElseExpressionType =
        SingleExpression *                          // condition
        Expression *                                // if body
        Expression                                  // else body
    and WhenExpressionType =
        SingleExpression *                          // case
        Expression                                  // body
    and OptionExpressionType =
        Identifier *                                // identifier
        WhenExpressionType list                         // cases
    
    and LoopExpressionType =
        | ForExpression of ForExpressionType
        | WhileExpression of WhileExpressionType
        | DoWhileExpression of DoWhileExpressionType
    and ForExpressionType =
        Identifier *                                // counter
        SingleExpression *                          // collection
        Expression                                  // body
    and WhileExpressionType =
        SingleExpression *                          // condition
        Expression                                  // body
    and DoWhileExpressionType =
        Expression *                                // body
        SingleExpression                            // condition

    and LoopControlExpressionType =
        | Next
        | Break

    and FunctionCallExpressionType =
        Identifier *                                // name
        SingleExpression list                       // parameters

    and FunctionControlExpressionType =
        | Return