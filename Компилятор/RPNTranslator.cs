namespace Компилятор
{

    public static class RPNTranslator
    {
        private static Dictionary<ETerminal, int> OperatorsPriority { get; } = new Dictionary<ETerminal, int>
        {
            // - (unary minus)
            { ETerminal.UnaryMinus, 7 },
            // * / % (multiplicative operators)
            { ETerminal.Multiply, 6 },
            { ETerminal.Divide, 6 },
            { ETerminal.Modulus, 6 },
            // + - (additive operators)
            { ETerminal.Plus, 5 },
            { ETerminal.Minus, 5 },
            // < > <= >= == (relational operators)
            { ETerminal.Less, 4 },
            { ETerminal.Greater, 4 },
            { ETerminal.LessEqual, 4 },
            { ETerminal.GreaterEqual, 4 },
            { ETerminal.Equal, 4 },
            // ! (unary negation)
            { ETerminal.Not, 3 },
            // && (logical AND)
            { ETerminal.And, 2 },
            // || (logical OR)
            { ETerminal.Or, 1 },
            //
            { ETerminal.GetByIndex, 0 },
            // = (assignment operator)
            { ETerminal.Assignment, -1 },
            // other
            { ETerminal.Int, -2 },
            { ETerminal.Bool, -2 },
            { ETerminal.String, -2 },
            { ETerminal.IntArray, -2 },
            { ETerminal.BoolArray, -2 },
            { ETerminal.StringArray, -2 },
            
            // groups
            { ETerminal.CondMark, -3 },
            { ETerminal.LeftBrace, -3 },
            { ETerminal.LeftBracket, -3 },
            { ETerminal.LeftParen, -3 },


        };
        private static Dictionary<ETerminal, ETerminalGroup> GroupOfTerminal { get; } = new()
        {
            { ETerminal.Number,         ETerminalGroup.Data },
            { ETerminal.TextLine,       ETerminalGroup.Data },
            { ETerminal.Boolean,        ETerminalGroup.Data },
            { ETerminal.VariableName,   ETerminalGroup.Data },
            { ETerminal.GetByIndex,     ETerminalGroup.Data },
            { ETerminal.MarkDestination,ETerminalGroup.Data },
            { ETerminal.MarkPointer,    ETerminalGroup.Data },
            { ETerminal.CondMark,       ETerminalGroup.Data },

            { ETerminal.Plus,           ETerminalGroup.Operator },
            { ETerminal.Minus,          ETerminalGroup.Operator },
            { ETerminal.Multiply,       ETerminalGroup.Operator },
            { ETerminal.Divide,         ETerminalGroup.Operator },
            { ETerminal.Modulus,        ETerminalGroup.Operator },
            { ETerminal.And,            ETerminalGroup.Operator },
            { ETerminal.Or,             ETerminalGroup.Operator },
            { ETerminal.Not,            ETerminalGroup.Operator },
            { ETerminal.Equal,          ETerminalGroup.Operator },
            { ETerminal.Less,           ETerminalGroup.Operator },
            { ETerminal.Greater,        ETerminalGroup.Operator },
            { ETerminal.LessEqual,      ETerminalGroup.Operator },
            { ETerminal.GreaterEqual,   ETerminalGroup.Operator },
            { ETerminal.Assignment,     ETerminalGroup.Operator },
            

            { ETerminal.LeftParen,      ETerminalGroup.GroupSymbol },
            { ETerminal.RightParen,     ETerminalGroup.GroupSymbol },
            { ETerminal.LeftBracket,    ETerminalGroup.GroupSymbol },
            { ETerminal.RightBracket,   ETerminalGroup.GroupSymbol },
            { ETerminal.LeftBrace,      ETerminalGroup.GroupSymbol },
            { ETerminal.RightBrace,     ETerminalGroup.GroupSymbol },
            { ETerminal.Semicolon,      ETerminalGroup.GroupSymbol },

            { ETerminal.If,             ETerminalGroup.Reserved },
            { ETerminal.While,          ETerminalGroup.Reserved },
            { ETerminal.Int,            ETerminalGroup.Reserved },
            { ETerminal.String,         ETerminalGroup.Reserved },
            { ETerminal.Bool,           ETerminalGroup.Reserved },
            { ETerminal.Output,         ETerminalGroup.Reserved },
            { ETerminal.Input,          ETerminalGroup.Reserved },

            
        };
        private static List<Terminal> RPN { get; } = [];
        private static Stack<Terminal> OperationsStack { get; } = [];
        

        /// <summary>
        /// Нахождение индекса парной закрывающейся скобки
        /// </summary>
        private static int FindPairedClosingBracket(int leftParenIndex, List<Terminal> terminals)
        {
            var openingBracket = terminals[leftParenIndex].TerminalType;
            var closingBracket = openingBracket switch
            {
                ETerminal.LeftParen => ETerminal.RightParen,
                ETerminal.LeftBrace => ETerminal.RightBrace,
                ETerminal.LeftBracket => ETerminal.RightBracket,
                _ => throw new ArgumentException(),
            };
            int counter = 0;
            for (int i = leftParenIndex; i < terminals.Count; i++)
            {
                if (terminals[i].TerminalType == openingBracket)
                {
                    counter++;
                }
                if (terminals[i].TerminalType == closingBracket)
                {
                    counter--;
                    if (counter == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static List<Terminal> ConvertToRPN(List<Terminal> terminals)
        {
            for (int i = 0; i < terminals.Count; i++)
            {
                // берём терминал
                var terminal = terminals[i];
                // определяем его тип
                var terminalType = terminal.TerminalType;
                // в зависимотсти от типа терминала
                switch (GroupOfTerminal[terminalType])
                {
                    // если это данные
                    case ETerminalGroup.Data:
                        // добавляем в ОПС
                        RPN.Add(terminal);
                        break;

                    // если это оператор
                    case ETerminalGroup.Operator:

                        // если это минус и предшествующий оператор не data
                        if (terminal.TerminalType == ETerminal.Minus &&
                            terminals.ElementAtOrDefault(i - 1) != null &&
                            GroupOfTerminal[terminals[i - 1].TerminalType] != ETerminalGroup.Data)
                        {
                            // значит это унарный минус и его надо добавить в стек
                            OperationsStack.Push(terminal);
                        }
                        //Если это не унарный минус
                        else
                        {
                            // если стек операторов пустой
                            if (OperationsStack.Count == 0)
                            {
                                // добавляем оператор в стек
                                OperationsStack.Push(terminal);
                            }
                            // если в стеке что-то есть
                            else
                            {
                                // пока стек не пустой
                                // и приоритет верхнего оператора в стеке выше или равен приоритету текущего оператора
                                while (OperationsStack.Count > 0 &&
                                       OperatorsPriority[OperationsStack.Peek().TerminalType] >= OperatorsPriority[terminal.TerminalType])
                                {
                                    //вытаскиваем оператор из стека и записываем его в ОПС
                                    RPN.Add(OperationsStack.Pop());
                                }
                                // добавляем оператор в стэк
                                OperationsStack.Push(terminal);
                            }
                        }
                        break;

                    // если это зарезервированное слово
                    case ETerminalGroup.Reserved:
                        switch (terminal.TerminalType)
                        {
                            // если это while
                            case ETerminal.While:
                                InsertWhile(terminals, i);
                                i--;
                                break;
                            case ETerminal.If:
                                InsertIf(terminals, i);
                                i--;
                                break;

                            case ETerminal.Int:
                                if (terminals[i+1].TerminalType == ETerminal.LeftBracket)
                                {
                                    OperationsStack.Push(new Terminal(ETerminal.IntArray));
                                }
                                else
                                {
                                    OperationsStack.Push(new Terminal(ETerminal.Int));
                                }
                                break;
                            case ETerminal.String:
                                if (terminals[i + 1].TerminalType == ETerminal.LeftBracket)
                                {
                                    OperationsStack.Push(new Terminal(ETerminal.StringArray));
                                }
                                else
                                {
                                    OperationsStack.Push(new Terminal(ETerminal.String));
                                }
                                break;
                            case ETerminal.Bool:
                                if (terminals[i + 1].TerminalType == ETerminal.LeftBracket)
                                {
                                    OperationsStack.Push(new Terminal(ETerminal.BoolArray));
                                }
                                else
                                {
                                    OperationsStack.Push(new Terminal(ETerminal.Bool));
                                }
                                break;

                            case ETerminal.Output:
                                OperationsStack.Push(terminal);
                                break;
                            case ETerminal.Input:
                                OperationsStack.Push(terminal);
                                break;
                        }
                        break;

                    // если это символл группировки
                    case ETerminalGroup.GroupSymbol:
                        switch (terminal.TerminalType)
                        {
                            // если это открывающаяся скобка
                            case ETerminal.LeftParen:
                                OperationsStack.Push(terminal);
                                break;
                            case ETerminal.LeftBracket:
                                InsertLeftBracket(terminals, i);
                                break;
                            case ETerminal.LeftBrace:
                                OperationsStack.Push(terminal);
                                break;

                            // если это закрывающаяся скобка
                            case ETerminal.RightParen:
                                while (OperationsStack.Peek().TerminalType != ETerminal.LeftParen)
                                {
                                    RPN.Add(OperationsStack.Pop());
                                }
                                OperationsStack.Pop();
                                break;
                            case ETerminal.RightBracket:
                                while (OperationsStack.Peek().TerminalType != ETerminal.LeftBracket)
                                {
                                    RPN.Add(OperationsStack.Pop());
                                }
                                OperationsStack.Pop();
                                break;
                            case ETerminal.RightBrace:
                                while (OperationsStack.Peek().TerminalType != ETerminal.LeftBrace)
                                {
                                    RPN.Add(OperationsStack.Pop());
                                }
                                OperationsStack.Pop();
                                break;

                            // если это ;
                            case ETerminal.Semicolon:
                                while (OperationsStack.Count > 0 && OperationsStack.Peek().TerminalType != ETerminal.LeftBrace)
                                {
                                    RPN.Add(OperationsStack.Pop());
                                }
                                break;
                        }
                        break;
                }
            }

            foreach (var terminal in RPN)
            {
                if (terminal is Terminal.MarkPointer markPointer)
                {
                    // Заданное значение DMID для поиска
                    int targetDMID = markPointer.DestinationMark.DMID;

                    // Поиск индекса объекта с заданным DMID
                    int index = RPN.FindIndex(t => t is Terminal.MarkDestination md && md.DMID == targetDMID);
                }
            }
            File.WriteAllText("RPN.txt", RPNToString());
            return RPN;
        }

        /// <summary>
        /// Редактирование последовательности терминалов для работы while
        /// </summary>
        /// <param name="terminals"></param>
        public static void InsertWhile(List<Terminal> terminals, int whilePos)
        {
            // имеем
            // <Обработанная часть> while ( <Логическое или> ) { <Блок инструкций> } <Не Обработанняа часть>
            // хотим получить
            // <Обработанная часть> md2 ( <Логическое или> ) cond mp1 { <Блок инструкций> } mp2 md1 <Не Обработанняа часть>

            Terminal.MarkDestination md1 = new(ETerminal.MarkDestination);
            Terminal.MarkDestination md2 = new(ETerminal.MarkDestination);
            Terminal.MarkPointer mp1 = new(ETerminal.MarkPointer, md1);
            Terminal.MarkPointer mp2 = new(ETerminal.MarkPointer, md2);
            Terminal cond = new(ETerminal.CondMark);

            // на место while ставим метку-назначение1
            terminals[whilePos] = md2;

            // находим индекс (
            int leftParenIndex = terminals.FindIndex(whilePos, t => t.TerminalType == ETerminal.LeftParen);

            // находим индекс )
            int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);

            // вставляем метку-указатель1 после )
            terminals.Insert(rightParenIndex + 1, mp1);

            // вставляем оператор условного перехода на метку-указатель2 после )
            terminals.Insert(rightParenIndex + 1, cond);

            // находим индекс {
            int leftBraceIndex = terminals.FindIndex(rightParenIndex, t => t.TerminalType == ETerminal.LeftBrace);

            // находим индекс }
            int rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);

            // вставляем метку-назначение1 после }
            terminals.Insert(rightBraceIndex + 1, md1);

            // вставляем метку-указатель2 после }
            terminals.Insert(rightBraceIndex + 1, mp2);
        }

        /// <summary>
        /// Редактирование последовательности терминалов для работы if
        /// </summary>
        /// <param name="terminals"></param>
        public static void InsertIf(List<Terminal> terminals, int ifPos)
        {
            // имеем
            //
            // варинат 1
            // <Обработанная часть> if ( <Логическое или> ) { <Блок инструкций> } <Не Обработанняа часть>
            // хотим получить
            // <Обработанная часть> ( <Логическое или> ) cond mp1 { <Блок инструкций> } md1 <Не Обработанняа часть>
            //
            // варинат 2
            // <Обработанная часть> if ( <Логическое или> ) { <Блок инструкций> } else { <Блок инструкций> } <Не Обработанняа часть>
            // хотим получить
            // <Обработанная часть> ( <Логическое или> ) cond mp1 { <Блок инструкций> } mp2 md1 { <Блок инструкций> } md2 <Не Обработанняа часть>

            Terminal.MarkDestination md1 = new(ETerminal.MarkDestination);
            Terminal.MarkDestination md2 = new(ETerminal.MarkDestination);
            Terminal.MarkPointer mp1 = new(ETerminal.MarkPointer, md1);
            Terminal.MarkPointer mp2 = new(ETerminal.MarkPointer, md2);
            Terminal cond = new(ETerminal.CondMark);

            // удаляем if
            terminals.RemoveAt(ifPos);

            // находим индекс (
            int leftParenIndex = terminals.FindIndex(ifPos, t => t.TerminalType == ETerminal.LeftParen);

            // находим индекс )
            int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);

            // вставляем метку-указатель1 после )
            terminals.Insert(rightParenIndex + 1, mp1);

            // вставляем оператор условного перехода на метку-указатель2 после )
            terminals.Insert(rightParenIndex + 1, cond);

            // находим индекс {
            int leftBraceIndex = terminals.FindIndex(rightParenIndex, t => t.TerminalType == ETerminal.LeftBrace);

            // находим индекс }
            int rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);

            if (rightBraceIndex + 1 < terminals.Count && terminals[rightBraceIndex + 1].TerminalType == ETerminal.Else)
            {
                // удаляем else
                terminals.RemoveAt(rightBraceIndex + 1);

                // вставляем метку-назначение1 после }
                terminals.Insert(rightBraceIndex + 1, md1);

                // вставляем метку-указатель2 после }
                terminals.Insert(rightBraceIndex + 1, mp2);


                // находим индекс {
                leftBraceIndex = terminals.FindIndex(rightBraceIndex, t => t.TerminalType == ETerminal.LeftBrace);

                // находим индекс }
                rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);

                // вставляем метку-назначение2 после }
                terminals.Insert(rightBraceIndex + 1, md2);
            }
            else
            {
                // вставляем метку-назначение1 после }
                terminals.Insert(rightBraceIndex + 1, md1);
            }
        }

        /// <summary>
        /// Редактирование последовательности терминалов для работы объявления массивов и индексации
        /// </summary>
        /// <param name="terminals"></param>
        public static void InsertLeftBracket(List<Terminal> terminals, int leftBracketPos)
        {
            // если перед [ было название переменной
            if (terminals[leftBracketPos - 1].TerminalType == ETerminal.VariableName)
            {
                // занчит это индексация по массиву

                // находим парную ]
                int rightBracketPos = FindPairedClosingBracket(leftBracketPos, terminals);
                // вставляем GetByIndex
                terminals.Insert(rightBracketPos + 1, new Terminal(ETerminal.GetByIndex));
            }
            OperationsStack.Push(terminals[leftBracketPos]);
        }

        /// <summary>
        /// Генерация строкового представления ОПС
        /// </summary>
        /// <returns></returns>
        private static string RPNToString()
        {
            string strRPN = string.Empty;

            foreach (var terminal in RPN)
            {
                ETerminal terminalType = terminal.TerminalType;
                switch (terminalType)
                {
                    case ETerminal.Number:
                        strRPN += (terminal as Terminal.Number).Data.ToString();
                        break;
                    case ETerminal.TextLine:
                        strRPN += $"{(terminal as Terminal.TextLine).Data}";
                        break;
                    case ETerminal.Boolean:
                        strRPN += (terminal as Terminal.Boolean).Data.ToString();
                        break;
                    case ETerminal.VariableName:
                        strRPN += (terminal as Terminal.VariableName).Name;
                        break;
                    case ETerminal.UnaryMinus:
                        strRPN += '-';
                        break;
                    case ETerminal.Multiply:
                        strRPN += '*';
                        break;
                    case ETerminal.Divide:
                        strRPN += '/';
                        break;
                    case ETerminal.Modulus:
                        strRPN += '%';
                        break;
                    case ETerminal.Plus:
                        strRPN += '+';
                        break;
                    case ETerminal.Minus:
                        strRPN += '-';
                        break;
                    case ETerminal.Less:
                        strRPN += '<';
                        break;
                    case ETerminal.Greater:
                        strRPN += '>';
                        break;
                    case ETerminal.LessEqual:
                        strRPN += "<=";
                        break;
                    case ETerminal.GreaterEqual:
                        strRPN += ">=";
                        break;
                    case ETerminal.Equal:
                        strRPN += "==";
                        break;
                    case ETerminal.Not:
                        strRPN += '!';
                        break;
                    case ETerminal.And:
                        strRPN += "&&";
                        break;
                    case ETerminal.Or:
                        strRPN += "||";
                        break;
                    case ETerminal.Assignment:
                        strRPN += '=';
                        break;
                    case ETerminal.GetByIndex:
                        strRPN += "GetByIndex";
                        break;
                    case ETerminal.CondMark:
                        strRPN += "CondMark";
                        break;
                    case ETerminal.Int:
                        strRPN += "Int";
                        break;
                    case ETerminal.IntArray:
                        strRPN += "IntArray";
                        break;
                    case ETerminal.String:
                        strRPN += "String";
                        break;
                    case ETerminal.StringArray:
                        strRPN += "StringArray";
                        break;
                    case ETerminal.Bool:
                        strRPN += "Bool";
                        break;
                    case ETerminal.BoolArray:
                        strRPN += "BoolArray";
                        break;
                    case ETerminal.Output:
                        strRPN += "Output";
                        break;
                    case ETerminal.Input:
                        strRPN += "Input";
                        break;
                    case ETerminal.MarkPointer:
                        strRPN += $"MP{(terminal as Terminal.MarkPointer).DestinationMark.DMID}";
                        break;
                    case ETerminal.MarkDestination:
                        strRPN += $"MD{(terminal as Terminal.MarkDestination).DMID}";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                strRPN += ' ';
            }
            return strRPN;
        }
    }
}
