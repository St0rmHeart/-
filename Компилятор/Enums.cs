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
        Func_Output,

        /// <summary>
        /// Input(A)
        /// </summary>
        Func_Input,

        /// <summary>
        /// a=b
        /// </summary>
        Func_Assignment,        // =

        /// <summary>
        /// A&&B
        /// </summary>
        Func_And,          // &&

        /// <summary>
        /// A||B
        /// </summary>
        Func_Or,           // ||

        /// <summary>
        /// A==B
        /// </summary>
        Func_Equal,        // ==

        /// <summary>
        /// A<B
        /// </summary>
        Func_Less,         // <

        /// <summary>
        /// A>B
        /// </summary>
        Func_Greater,      // >

        /// <summary>
        /// A<=B
        /// </summary>
        Func_LessEqual,    // <=

        /// <summary>
        /// A>=B
        /// </summary>
        Func_GreaterEqual, // >=

        /// <summary>
        /// A+B
        /// </summary>
        Func_Plus,         // +

        /// <summary>
        /// A-B
        /// </summary>
        Func_Minus,        // -

        /// <summary>
        /// A*B
        /// </summary>
        Func_Multiply,     // *

        /// <summary>
        /// A/B
        /// </summary>
        Func_Divide,       // /

        /// <summary>
        /// A%B
        /// </summary>
        Func_Modulus,      // %

        /// <summary>
        /// !A
        /// </summary>
        Func_Not,          // !

        /// <summary>
        /// Взятие B-го элемента от массива A
        /// </summary>
        Func_Index,      //[]

        /// <summary>
        /// int
        /// </summary>
        Func_Int,

        /// <summary>
        /// string
        /// </summary>
        Func_String,

        /// <summary>
        /// Создание массива bool именем A числом элементов B
        /// </summary>
        Func_Bool,

        /// <summary>
        /// Создание массива int именем A числом элементов B
        /// </summary>
        Func_IntArray,

        /// <summary>
        /// Создание массива string именем A числом элементов B
        /// </summary>
        Func_StringArray,

        /// <summary>
        /// bool
        /// </summary>
        Func_BoolArray,

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
        ConditionalJumpToMark,

        /// <summary>
        /// Безусловный переход к MarkElse
        /// </summary>
        UnconditionalJumpToMark,

        /// <summary>
        /// Безусловный переход к MarkElse
        /// </summary>
        Mark,
    }
    public enum EMarkType
    {
        WhileBeginMark,
        WhileEndMark,
        IfMark,
        ElseMark,
    }
}
