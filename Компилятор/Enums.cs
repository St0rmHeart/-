namespace Компилятор
{
   public enum ETerminalType
    {
        /// <summary>
        /// Целое число
        /// </summary>
        Number,
        
        /// <summary>
        /// Строка текста
        /// </summary>
        TextLine,
        
        /// <summary>
        /// True или False
        /// </summary>
        Boolean,
        // Operators
        
        /// <summary>
        /// +
        /// </summary>
        Plus,         // +
        
        /// <summary>
        /// -
        /// </summary>
        Minus,        // -
        
        /// <summary>
        /// *
        /// </summary>
        Multiply,     // *
        
        /// <summary>
        /// /
        /// </summary>
        Divide,       // /
        
        /// <summary>
        /// %
        /// </summary>
        Modulus,      // %
        
        /// <summary>
        /// &&
        /// </summary>
        And,          // &&
        
        /// <summary>
        /// ||
        /// </summary>
        Or,           // ||
        
        /// <summary>
        /// !
        /// </summary>
        Not,          // !

        // Parentheses and Brackets
        
        /// <summary>
        /// (
        /// </summary>
        LeftParen,    // (
        
        /// <summary>
        /// )
        /// </summary>
        RightParen,   // )
        
        /// <summary>
        /// [
        /// </summary>
        LeftBracket,  // [
        
        /// <summary>
        /// ]
        /// </summary>
        RightBracket, // ]
        
        /// <summary>
        /// {
        /// </summary>
        LeftBrace,    // {
        
        /// <summary>
        /// }
        /// </summary>
        RightBrace,   // }

        // Quotation mark
        /// <summary>
        /// "
        /// </summary>
        DoubleQuote,  // “

        // Assignment
        /// <summary>
        /// =
        /// </summary>
        Assignment,        // =

        // Identifiers and keywords
       
        /// <summary>
        /// Пользовательская переменная
        /// </summary>
        VariableName,   // a...z, A...Z, _, а...я, А...Я
        
        /// <summary>
        /// if
        /// </summary>
        If,
        
        /// <summary>
        /// else
        /// </summary>
        Else,
        
        /// <summary>
        /// ==
        /// </summary>
        Equal,        // ==
        
        /// <summary>
        /// строго меньше 
        /// </summary>
        Less,         // <
        
        /// <summary>
        /// >
        /// </summary>
        Greater,      // >

        /// <summary>
        /// меньше или равно 
        /// </summary>
        LessEqual,    // <=
        
        /// <summary>
        /// >=
        /// </summary>
        GreaterEqual, // >=
        
        /// <summary>
        /// while
        /// </summary>
        While,
        
        /// <summary>
        /// int
        /// </summary>
        Int,
        
        /// <summary>
        /// string
        /// </summary>
        String,
        
        /// <summary>
        /// bool
        /// </summary>
        Bool,
        
        /// <summary>
        /// Функия вывода данных
        /// </summary>
        Output,
        
        /// <summary>
        /// Функция ввода данных в переменную
        /// </summary>
        Input,
        
        /// <summary>
        /// ;
        /// </summary>
        Semicolon 

    }

    //RPN = Reverse Polish Notation = Обратная польская нотация = ОПН = ОПС
    public enum ERPNType
    {
        //ОПЕРАЦИИ
        // При описании операций буквами A и B обозначены, соответственно, первый и второй аргументы, используемые в этой операции
        // AB+ == A+B и т.д.

        /// <summary>
        /// Output(A)
        /// </summary>
        FuncOutput,

        /// <summary>
        /// Input(A)
        /// </summary>
        FuncInput,

        /// <summary>
        /// a=b
        /// </summary>
        FuncAssignment,        // =

        /// <summary>
        /// A&&B
        /// </summary>
        FuncAnd,          // &&

        /// <summary>
        /// A||B
        /// </summary>
        FuncOr,           // ||

        /// <summary>
        /// A==B
        /// </summary>
        FuncEqual,        // ==

        /// <summary>
        /// A<B
        /// </summary>
        FuncLess,         // <

        /// <summary>
        /// A>B
        /// </summary>
        FuncGreater,      // >

        /// <summary>
        /// A<=B
        /// </summary>
        FuncLessEqual,    // <=

        /// <summary>
        /// A>=B
        /// </summary>
        FuncGreaterEqual, // >=

        /// <summary>
        /// A+B
        /// </summary>
        FuncPlus,         // +

        /// <summary>
        /// A-B
        /// </summary>
        FuncMinus,        // -

        /// <summary>
        /// A*B
        /// </summary>
        FuncMultiply,     // *

        /// <summary>
        /// A/B
        /// </summary>
        FuncDivide,       // /

        /// <summary>
        /// A%B
        /// </summary>
        FuncModulus,      // %

        /// <summary>
        /// !A
        /// </summary>
        FuncNot,          // !

        /// <summary>
        /// Взятие B-го элемента от массива A
        /// </summary>
        FuncIndex,      //[]

        /// <summary>
        /// int
        /// </summary>
        FuncInt,

        /// <summary>
        /// string
        /// </summary>
        FuncString,

        /// <summary>
        /// Создание массива bool именем A числом элементов B
        /// </summary>
        FuncBool,

        /// <summary>
        /// Создание массива int именем A числом элементов B
        /// </summary>
        FuncIntArray,

        /// <summary>
        /// Создание массива string именем A числом элементов B
        /// </summary>
        FuncStringArray,

        /// <summary>
        /// bool
        /// </summary>
        FuncBoolArray,

        //АРГУМЕНТЫ

        /// <summary>
        /// Целое число
        /// </summary>
        Number,

        /// <summary>
        /// Строка текста
        /// </summary>
        TextLine,

        /// <summary>
        /// True или False
        /// </summary>
        Boolean,

        /// <summary>
        /// Пользовательский идентификатор
        /// </summary>
        Identifier,   // a...z, A...Z, _, а...я, А...Я

        // Identifiers and keywords

        /// <summary>
        /// if
        /// </summary>
        If,

        /// <summary>
        /// else
        /// </summary>
        Else,

        /// <summary>
        /// while
        /// </summary>
        While,

        /// <summary>
        /// ;
        /// </summary>
        Semicolon,

        /// <summary>
        /// (
        /// </summary>
        LeftParen,    // (

        /// <summary>
        /// )
        /// </summary>
        RightParen,   // )

        /// <summary>
        /// [
        /// </summary>
        LeftBracket,  // [

        /// <summary>
        /// ]
        /// </summary>
        RightBracket, // ]

        /// <summary>
        /// {
        /// </summary>
        LeftBrace,    // {

        /// <summary>
        /// }
        /// </summary>
        RightBrace,   // }

        /// <summary>
        /// Если TRUE - выполняется идущий дальше код, иначе - переход к MarkIf
        /// </summary>
        ConditionalJumpToMarkIf,

        /// <summary>
        /// Безусловный переход к MarkElse
        /// </summary>
        JumpToMarkElse, 

        /// <summary>
        /// Сюда переходит ConditionalJumpToMarkIf если условие не выполняется
        /// </summary>
        MarkIf,

        /// <summary>
        /// Сюда переходит прыгает JumpToMarkElse
        /// </summary>
        MarkElse,

        /// <summary>
        /// Начало цикла While, сюда безусловно переходит ConditionalJumpToWhileEnd 
        /// </summary>
        MarkWhileBegin,

        /// <summary>
        /// Безусловный переход к MarkWhileBegin
        /// </summary>
        JumpToWhileBegin,

        /// <summary>
        /// Конец цикла While, сюда переходит ConditionalJumpToWhileEnd если его условие выполняется
        /// </summary>
        MarkWhileEnd,

        /// <summary>
        /// Если TRUE - выполняется идущий дальше код, иначе - переход к MarkWhileEnd
        /// </summary>
        ConditionalJumpToWhileEnd,
    }
}
