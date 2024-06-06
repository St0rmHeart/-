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
        /// Пользовательский идентификатор
        /// </summary>
        Identifier,   // a...z, A...Z, _, а...я, А...Я
        
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
}
