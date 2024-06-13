namespace Компилятор
{
    public static class RPNReader
    {
        private static Stack<Terminal> OperandStack { get; set; } = new();
        private static Dictionary<string, Terminal> VariablesDictionary { get; set; } = [];

        public static void ExecuteRPN(List<Terminal> terminals)
        {
            int counter = 0;
            string RPNlog = string.Empty;
            for (int i = 0; i < terminals.Count; i++)
            {
                counter++;
                var terminal = terminals[i];
                var terminalType = terminal.TerminalType;
                switch (terminalType)
                {
                    case ETerminal.Number:
                    case ETerminal.TextLine:
                    case ETerminal.Boolean:
                    case ETerminal.VariableName:
                        OperandStack.Push(terminal);
                        break;

                    case ETerminal.UnaryMinus:
                        ExecuteUnaryMinus();
                        break;

                    case ETerminal.Multiply:
                    case ETerminal.Divide:
                    case ETerminal.Modulus:
                    case ETerminal.Plus:
                    case ETerminal.Minus:
                        ExecuteMath(terminalType);
                        break;

                    case ETerminal.Less:
                    case ETerminal.Greater:
                    case ETerminal.LessEqual:
                    case ETerminal.GreaterEqual:
                    case ETerminal.Equal:
                        ExecuteIntComparsion(terminalType);
                        break;

                    case ETerminal.Not:
                        ExecuteNegation();
                        break;

                    case ETerminal.And:
                    case ETerminal.Or:
                        ExecuteAndOr(terminalType);
                        break;

                    case ETerminal.Assignment:
                        ExecuteAssignment();
                        break;

                    case ETerminal.GetByIndex:
                        ExecuteGetByIndex();
                        break;

                    case ETerminal.CondMark:
                        if (ExecuteCondMark()) i++;
                        break;

                    case ETerminal.Int:
                    case ETerminal.String:
                    case ETerminal.Bool:
                        ExecuteVariableInitialization(terminalType);
                        break;

                    case ETerminal.IntArray:
                    case ETerminal.StringArray:
                    case ETerminal.BoolArray:
                        ExecuteArrayInitialization(terminalType);
                        break;

                    case ETerminal.Output:
                        ExecuteOutput();
                        break;

                    case ETerminal.Input:
                        ExecuteInput();
                        break;

                    case ETerminal.MarkPointer:
                        i = (terminal as Terminal.MarkPointer).Destination;
                        break;

                    case ETerminal.MarkDestination:
                        break;
                }
                RPNlog += $"\n{counter.ToString() + '.',4}{(i + 1).ToString() + '.',4} {terminalType}";
            }
            File.WriteAllText("RPNlog.txt", RPNlog);
        }
        private static int TryGetIntData(Terminal terminal)
        {
            if (terminal is Terminal.VariableName variable)
            {
                if (VariablesDictionary.TryGetValue(variable.Name, out terminal))
                {
                    if (terminal is not Terminal.Number)
                    {
                        throw new Exception($"\nПеременная {variable.Name} имеет не корректный тип данных" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                    }
                }
                else
                {
                    throw new Exception($"\nПеременная {variable.Name} не была инициализирована" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                }
            }
            return (terminal as Terminal.Number).Data;
        }
        private static bool TryGetBoolData(Terminal terminal)
        {
            if (terminal is Terminal.VariableName variable)
            {
                if (VariablesDictionary.TryGetValue(variable.Name, out terminal))
                {
                    if (terminal is not Terminal.Boolean)
                    {
                        throw new Exception($"\nПеременная {variable.Name} имеет не корректный тип данных" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                    }
                }
                else
                {
                    throw new Exception($"\nПеременная {variable.Name} не была инициализирована" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                }
            }
            return (terminal as Terminal.Boolean).Data;
        }
        private static string TryGetStringData(Terminal terminal)
        {
            if (terminal is Terminal.VariableName variable)
            {
                if (VariablesDictionary.TryGetValue(variable.Name, out terminal))
                {
                    if (terminal is not Terminal.TextLine)
                    {
                        throw new Exception($"\nПеременная {variable.Name} имеет не корректный тип данных" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                    }
                }
                else
                {
                    throw new Exception($"\nПеременная {variable.Name} не была инициализирована" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                }
            }
            return (terminal as Terminal.TextLine).Data;
        }
        private static void ExecuteUnaryMinus()
        {
            // 
            var operand1 = OperandStack.Pop();
            // 
            int argument1 = TryGetIntData(operand1);
            // 
            int overationResult = -argument1;
            //
            OperandStack.Push(new Terminal.Number(ETerminal.Number, overationResult));
        }
        private static void ExecuteMath(ETerminal terminal)
        {
            // 
            var operand2 = OperandStack.Pop();
            // 
            var operand1 = OperandStack.Pop();
            // 
            int argument1 = TryGetIntData(operand1);
            // 
            int argument2 = TryGetIntData(operand2);
            // 
            var overationResult = terminal switch
            {
                ETerminal.Multiply => argument1 * argument2,
                ETerminal.Divide => argument1 / argument2,
                ETerminal.Modulus => argument1 % argument2,
                ETerminal.Plus => argument1 + argument2,
                ETerminal.Minus => argument1 - argument2,
                _ => throw new Exception("Некорректный тип оператора"),
            };
            //
            OperandStack.Push(new Terminal.Number(ETerminal.Number, overationResult));
        }
        private static void ExecuteIntComparsion(ETerminal terminal)
        {
            // 
            var operand2 = OperandStack.Pop();
            // 
            var operand1 = OperandStack.Pop();
            // 
            int argument1 = TryGetIntData(operand1);
            // 
            int argument2 = TryGetIntData(operand2);
            // 
            var overationResult = terminal switch
            {
                ETerminal.Less => argument1 < argument2,
                ETerminal.Greater => argument1 > argument2,
                ETerminal.LessEqual => argument1 <= argument2,
                ETerminal.GreaterEqual => argument1 >= argument2,
                ETerminal.Equal => argument1 == argument2,
                _ => throw new Exception("Некорректный тип оператора"),
            };
            //
            OperandStack.Push(new Terminal.Boolean(ETerminal.Boolean, overationResult));
        }
        private static void ExecuteNegation()
        {
            // 
            var operand1 = OperandStack.Pop();
            // 
            bool argument1 = TryGetBoolData(operand1);
            // 
            bool overationResult = !argument1;
            //
            OperandStack.Push(new Terminal.Boolean(ETerminal.Number, overationResult));
        }
        private static void ExecuteAndOr(ETerminal terminal)
        {
            // 
            var operand2 = OperandStack.Pop();
            // 
            var operand1 = OperandStack.Pop();
            // 
            bool argument1 = TryGetBoolData(operand1);
            // 
            bool argument2 = TryGetBoolData(operand2);
            // 
            var overationResult = terminal switch
            {
                ETerminal.Or => argument1 || argument2,
                ETerminal.And => argument1 && argument2,
                _ => throw new Exception("Некорректный тип оператора"),
            };
            //
            OperandStack.Push(new Terminal.Boolean(ETerminal.Number, overationResult));
        }
        private static void ExecuteAssignment()
        {
            // 
            var operand2 = OperandStack.Pop();
            if (operand2 is Terminal.VariableName variable)
            {
                operand2 = VariablesDictionary[variable.Name];
            }

            // 
            var operand1 = OperandStack.Pop() as Terminal.VariableName;
            // 
            if (VariablesDictionary.TryGetValue(operand1.Name, out Terminal previousValue))
            {
                if (operand2 is Terminal.Number && previousValue is Terminal.Number ||
                    operand2 is Terminal.Boolean && previousValue is Terminal.Boolean ||
                    operand2 is Terminal.TextLine && previousValue is Terminal.TextLine)
                {
                    VariablesDictionary[operand1.Name] = operand2;
                }
                else
                {
                    throw new Exception($"\n Попытка присвоения {operand2.GetType()} данных в пременую типа {previousValue.GetType()}" +
                                        $"\n\t строка {operand1.LinePointer};" +
                                        $"\n\t символ {operand1.CharPointer};");
                }
            }
            else
            {
                throw new Exception($"\nПеременная {operand1.Name} не была инициализирована" +
                                    $"\n\t строка {operand1.LinePointer};" +
                                    $"\n\t символ {operand1.CharPointer};");
            }
        }
        private static void ExecuteGetByIndex()
        {
            // 
            var operand2 = TryGetIntData(OperandStack.Pop());
            // 
            var operand1 = OperandStack.Pop() as Terminal.VariableName;
            // 
            string variableName = operand1.Name + '[' + operand2 + ']';
            string zeroIndex = operand1.Name + "[0]";

            if (VariablesDictionary.ContainsKey(variableName))
            {
                //
                OperandStack.Push(new Terminal.VariableName(ETerminal.VariableName, variableName));
            }
            else if (VariablesDictionary.ContainsKey(zeroIndex))
            {
                throw new Exception($"Выход за границы массива {operand1}");
            }
            else
            {
                throw new Exception($"Массив {operand1} не был инициализирован");
            }

        }
        private static bool ExecuteCondMark()
        {
            var operand1 = OperandStack.Pop() as Terminal.Boolean;
            return operand1.Data;
        }
        private static void ExecuteVariableInitialization(ETerminal terminal)
        {
            var operand1 = OperandStack.Pop() as Terminal.VariableName;
            if (VariablesDictionary.ContainsKey(operand1.Name))
            {
                throw new Exception($"\nПеременная {operand1.Name} уже была инициализирована" +
                                        $"\n\t строка {operand1.LinePointer};" +
                                        $"\n\t символ {operand1.CharPointer};");
            }
            else
            {
                switch (terminal)
                {
                    case ETerminal.Int:
                        VariablesDictionary.Add(operand1.Name, new Terminal.Number(ETerminal.Number, 0));
                        break;
                    case ETerminal.String:
                        VariablesDictionary.Add(operand1.Name, new Terminal.TextLine(ETerminal.TextLine, ""));
                        break;
                    case ETerminal.Bool:
                        VariablesDictionary.Add(operand1.Name, new Terminal.Boolean(ETerminal.Boolean, false));
                        break;
                    default:
                        throw new Exception($"\nТип данных указан неверно");
                }
            }
        }
        private static void ExecuteArrayInitialization(ETerminal terminal)
        {
            var operand1 = OperandStack.Pop() as Terminal.VariableName;
            var operand2 = TryGetIntData(OperandStack.Pop());

            if (VariablesDictionary.ContainsKey(operand1.Name + "[0]"))
            {
                throw new Exception($"\nМассив {operand1.Name} уже был инициализирован" +
                                        $"\n\t строка {operand1.LinePointer};" +
                                        $"\n\t символ {operand1.CharPointer};");
            }
            if (operand2 < 1)
            {
                throw new Exception($"\nЗадан невозможный размера массива" +
                                        $"\n\t строка {operand1.LinePointer};" +
                                        $"\n\t символ {operand1.CharPointer};");
            }
            for (int i = 0; i < operand2; i++)
            {
                switch (terminal)
                {
                    case ETerminal.IntArray:
                        VariablesDictionary.Add(operand1.Name + '[' + i + ']', new Terminal.Number(ETerminal.Number, 0));
                        break;
                    case ETerminal.StringArray:
                        VariablesDictionary.Add(operand1.Name + '[' + i + ']', new Terminal.TextLine(ETerminal.TextLine, ""));
                        break;
                    case ETerminal.BoolArray:
                        VariablesDictionary.Add(operand1.Name + '[' + i + ']', new Terminal.Boolean(ETerminal.Boolean, false));
                        break;
                    default:
                        throw new Exception($"\nТип данных указан неверно");
                }
            }
        }
        private static void ExecuteOutput()
        {
            var operand1 = OperandStack.Pop();
            if (operand1 is Terminal.VariableName variable)
            {
                if (!VariablesDictionary.TryGetValue(variable.Name, out operand1))
                {
                    throw new Exception($"\nПеременная {variable.Name} не была инициализирована" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                }
            }
            if (operand1 is Terminal.Number)
            {
                int data = TryGetIntData(operand1);
                Console.WriteLine(data);
            }
            if (operand1 is Terminal.TextLine)
            {
                string data = TryGetStringData(operand1);
                Console.WriteLine(data);
            }
            if (operand1 is Terminal.Boolean)
            {
                bool data = TryGetBoolData(operand1);
                Console.WriteLine(data);
            }
        }
        private static void ExecuteInput()
        {
            var operand1 = OperandStack.Pop();
            string stringData = Console.ReadLine();
            if (operand1 is Terminal.VariableName variable)
            {
                if (VariablesDictionary.TryGetValue(variable.Name, out Terminal value))
                {
                    if (value is Terminal.Number)
                    {
                        VariablesDictionary[variable.Name] = new Terminal.Number(ETerminal.Number, Convert.ToInt32(stringData));
                    }
                    if (value is Terminal.TextLine)
                    {
                        VariablesDictionary[variable.Name] = new Terminal.TextLine(ETerminal.Number,stringData);
                    }
                    if (value is Terminal.Boolean)
                    {
                        VariablesDictionary[variable.Name] = new Terminal.Boolean(ETerminal.Number, Convert.ToBoolean(stringData));
                    }
                    throw new Exception($"Некорректно создана переменная");
                }
                else
                {
                    throw new Exception($"\nПеременная {variable.Name} не была инициализирована" +
                                        $"\n\t строка {variable.LinePointer};" +
                                        $"\n\t символ {variable.CharPointer};");
                }
            }
        }
    }
}
