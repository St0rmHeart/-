namespace Компилятор
{
    public enum ETerminal
    {
        #region __________ДАННЫЕ__________
        /// <summary>
        /// Целое число - int
        /// </summary>
        Number,

        /// <summary>
        /// Строка текста - string
        /// </summary>
        TextLine,

        /// <summary>
        /// True или False - bool
        /// </summary>
        Boolean,

        /// <summary>
        /// Пользовательская переменная
        /// </summary>
        VariableName,
        #endregion

        #region __________ОПЕРАТОРЫ__________
        /// <summary>
        /// <br /> -A
        /// <br /> унарная
        /// <br /> 1. int
        /// </summary>
        UnaryMinus,

        /// <summary>
        /// <br /> A * B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        Multiply,

        /// <summary>
        /// /
        /// </summary>
        Divide,

        /// <summary>
        /// <br /> A / B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        Modulus,

        /// <summary>
        /// <br /> A + B
        /// <br /> бинарная
        /// <br /> 1. int, string
        /// <br /> 2. int, string
        /// </summary>
        Plus,

        /// <summary>
        /// <br /> A - B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        Minus,

        /// <summary>
        /// <br /> A &lt; B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        Less,

        /// /// <summary>
        /// <br /> A &gt; B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        Greater,

        /// <summary>
        /// <br /> A &lt;= B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        LessEqual,
        
        /// <summary>
        /// <br /> A &gt;= B
        /// <br /> бинарная
        /// <br /> 1. int
        /// <br /> 2. int
        /// </summary>
        GreaterEqual,

        /// <summary>
        /// <br /> A == B
        /// <br /> бинарная
        /// <br /> 1. bool
        /// <br /> 2. bool
        /// </summary>
        Equal,

        /// <summary>
        /// <br /> !A
        /// <br /> унарная
        /// <br /> 1. bool
        /// </summary>
        Not,

        /// <summary>
        /// <br /> A && B
        /// <br /> бинарная
        /// <br /> 1. bool
        /// <br /> 2. bool
        /// </summary>
        And,

        /// <summary>
        /// <br /> A || B
        /// <br /> бинарная
        /// <br /> 1. bool
        /// <br /> 2. bool
        /// </summary>
        Or,

        /// <summary>
        /// <br /> a = b
        /// <br /> бинарная
        /// <br /> 1. variableName;
        /// <br /> 2. string, int, bool;
        /// <br /> переменная и данные должны быть одного типа
        /// </summary>
        Assignment,

        /// <summary>
        /// <br /> Взятие B-го элемента от массива A
        /// <br /> бинарная
        /// <br /> 1. variableName
        /// <br /> 2. int
        /// </summary>
        GetByIndex,

        /// <summary>
        /// <br /> Если условие верное - перепрыгивает последующую ему метку
        /// <br /> унарная
        /// <br /> 1. bool
        /// </summary>
        CondMark,
        #endregion

        #region __________СИМВОЛЫ ГРУППИРОВКИ__________
        /// <summary>
        /// (
        /// </summary>
        LeftParen,

        /// <summary>
        /// )
        /// </summary>
        RightParen,

        /// <summary>
        /// [
        /// </summary>
        LeftBracket,

        /// <summary>
        /// ]
        /// </summary>
        RightBracket,

        /// <summary>
        /// {
        /// </summary>
        LeftBrace,

        /// <summary>
        /// }
        /// </summary>
        RightBrace,

        /// <summary>
        /// "
        /// </summary>
        DoubleQuote,

        /// <summary>
        /// ;
        /// </summary>
        Semicolon,
        #endregion

        #region __________КЛЮЧЕВЫЕ СЛОВА__________
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
        /// <br /> Создание перменной типа int
        /// <br /> унарная
        /// <br /> 1. variableName
        /// </summary>
        Int,

        /// <summary>
        /// <br /> Создание массива int именем B числом элементов A
        /// <br /> бинарная
        /// <br /> 1. variableName
        /// <br /> 2. int
        /// </summary>
        IntArray = 60,

        /// <summary>
        /// <br /> Создание перменной типа string
        /// <br /> унарная
        /// <br /> 1. variableName
        /// </summary>
        String,

        /// <summary>
        /// <br /> Создание массива string именем B числом элементов A
        /// <br /> бинарная
        /// <br /> 1. variableName
        /// <br /> 2. int
        /// </summary>
        StringArray,

        /// <summary>
        /// <br /> Создание перменной типа bool
        /// <br /> унарная
        /// <br /> 1. variableName
        /// </summary>
        Bool,

        /// <summary>
        /// <br /> Создание массива bool именем B числом элементов A
        /// <br /> бинарная
        /// <br /> 1. variableName
        /// <br /> 2. int
        /// </summary>
        BoolArray,

        /// <summary>
        /// <br /> Output(A)
        /// <br /> Унарная
        /// <br /> 1. bool, int, string
        /// </summary>
        Output,

        /// <summary>
        /// <br /> Input(A)
        /// <br /> Унарная
        /// <br /> 1. bool, int, string
        /// </summary>
        Input,
        #endregion

        /// <summary>
        /// Метка-указатель
        /// </summary>
        MarkPointer,

        /// <summary>
        /// Метка-назначение
        /// </summary>
        MarkDestination
    }

    public enum EMarkType
    {
        None,
        WhileBeginMark,
        WhileEndMark,
        IfMark,
        ElseMark,
    }
    public enum ETerminalGroup
    {
        Operator,
        Data,
        GroupSymbol,
        Reserved,
    }
    #region __________Данные__________
    #endregion
}
