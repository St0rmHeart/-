using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Компилятор
{
    public static class RPNTranslator
    {
        static List<Terminal> Input = new List<Terminal>();
        public static List<RPNSymbol> Output = new List<RPNSymbol>();
        static List<RPNSymbol> OperationStack = new List<RPNSymbol>();
        /// <summary>
        /// Возвращает список типа RPNTranslator в формате польской строки из входного списка типа Terminal
        /// </summary>
        public static List<RPNSymbol> Translate(List<Terminal> input)
        {
            Input = input;
            while (Input.Count > 0)
            {
                //Если операция или скобка - кладём в стек
                if (IsOperationOrParenthesis(Input[0]))
                {
                    ToStack(TranslateToRPNSymbol(Input[0]));
                    Input.Remove(Input.First());
                }
                //Если операнд - кладём в Output
                else if (IsOperand(Input[0]))
                {
                    Output.Add(TranslateToRPNSymbol(Input[0]));
                    Input.Remove(Input.First());
                }
                //while обрабатывается особым образом
                else if (Input[0].TerminalType == ETerminalType.While)
                {
                    Output.Add(new RPNSymbol(ERPNType.MarkWhileBegin));
                    OperationStack.Add(new RPNSymbol(ERPNType.ConditionalJumpToWhileEnd));
                    Input.Remove(Input.First());
                }
                //if обрабатывается особым образом
                else if (Input[0].TerminalType == ETerminalType.If)
                {
                    OperationStack.Add(new RPNSymbol(ERPNType.ConditionalJumpToMarkIf));
                    Input.Remove(Input.First());
                }
            }
            //После завершения считывания строки все оставшиеся OperationStack в стеке записываются в Output
            while (OperationStack.Count > 0)
            {
                if (IsWritable(OperationStack.Last()))
                {
                    Output.Add(OperationStack.Last());
                }
                OperationStack.Remove(OperationStack.Last());
            }
            return Output;
        }
        /// <summary>
        /// Возвращает true если input можно записать в Output
        /// </summary>
        public static bool IsWritable(RPNSymbol input)
        {
            if ((input.RPNType == ERPNType.Semicolon) || (input.RPNType == ERPNType.RightParen) || (input.RPNType == ERPNType.RightBracket) || (input.RPNType == ERPNType.RightBrace) || (input.RPNType == ERPNType.LeftBrace) || (input.RPNType == ERPNType.LeftParen))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Возвращает true если input - операнд
        /// </summary>
        public static bool IsOperand(Terminal input)
        {
            if ((input.TerminalType == ETerminalType.Number) || (input.TerminalType == ETerminalType.TextLine) || (input.TerminalType == ETerminalType.Boolean) || (input.TerminalType == ETerminalType.Identifier))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Возвращает true если input - операция или скобка
        /// </summary>
        public static bool IsOperationOrParenthesis(Terminal input)
        {
            if ((input.TerminalType == ETerminalType.Int) || (input.TerminalType == ETerminalType.String) || (input.TerminalType == ETerminalType.Bool) || (input.TerminalType == ETerminalType.Else) || (input.TerminalType == ETerminalType.Semicolon) || (input.TerminalType == ETerminalType.Output) || (input.TerminalType == ETerminalType.Input) || (input.TerminalType == ETerminalType.Assignment) || (input.TerminalType == ETerminalType.And) || (input.TerminalType == ETerminalType.Or) || (input.TerminalType == ETerminalType.Equal) || (input.TerminalType == ETerminalType.Less) || (input.TerminalType == ETerminalType.Greater) || (input.TerminalType == ETerminalType.GreaterEqual) || (input.TerminalType == ETerminalType.LessEqual) || (input.TerminalType == ETerminalType.Plus) || (input.TerminalType == ETerminalType.Minus) || (input.TerminalType == ETerminalType.Divide) || (input.TerminalType == ETerminalType.Multiply) || (input.TerminalType == ETerminalType.Modulus) || (input.TerminalType == ETerminalType.Not) ||  (input.TerminalType == ETerminalType.LeftParen) || (input.TerminalType == ETerminalType.RightParen) || (input.TerminalType == ETerminalType.RightBracket) || (input.TerminalType == ETerminalType.LeftBracket) || (input.TerminalType == ETerminalType.RightBrace) || (input.TerminalType == ETerminalType.LeftBrace))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Возвращает true если input - функция инициализации переменной
        /// </summary>
        public static bool IsVariableInitialization(RPNSymbol input)
        {
            if ((input.RPNType == ERPNType.FuncInt) || (input.RPNType == ERPNType.FuncString) || (input.RPNType == ERPNType.FuncBool))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Переносит оператор input из списка Input в OperatorStack
        /// </summary>
        public static void ToStack(RPNSymbol input)
        {
            if (OperationStack.Count > 0)
            {
                //Если входная лексема - правая круглая скобка, то в Output записываются все операции из OperationStack пока там не найдётся левая круглая скобка
                if (input.RPNType == ERPNType.RightParen)
                {
                    while ((OperationStack.Count > 0) && (OperationStack.Last().RPNType != ERPNType.LeftParen))
                    {
                        if (IsWritable(OperationStack.Last()))
                        {
                            Output.Add(OperationStack.Last());
                        }
                        OperationStack.Remove(OperationStack.Last());
                    }
                    //Если левая круглая скобка - условие If или While, то в Output записываются соответствующие символы
                    if (OperationStack.Count > 1)
                    {
                        OperationStack.Remove(OperationStack.Last());
                        if ((OperationStack.Last().RPNType == ERPNType.ConditionalJumpToMarkIf) || (OperationStack.Last().RPNType == ERPNType.ConditionalJumpToWhileEnd))
                        {
                            Output.Add(OperationStack.Last());
                            OperationStack.Remove(OperationStack.Last());
                        }
                    }
                }

                else if ((OperationStack.Count > 0) && (input.RPNType == ERPNType.RightBracket))
                {
                    //Если входная лексема - правая квадратная скобка, то в Output записываются все операции из OperationStack пока там не найдётся левая квадратная скобка
                    while ((OperationStack.Count > 0) && (OperationStack.Last().RPNType != ERPNType.LeftBracket))
                    {
                        if (IsWritable(OperationStack.Last()))
                        {
                            Output.Add(OperationStack.Last());
                        }
                        OperationStack.Remove(OperationStack.Last());
                    }
                    //Если перед левой квадратной скобкой стоит операция инициализации переменной - в Output записывается операция инициализации массива переменных такого типа
                    if (OperationStack.Count > 1)
                    {
                        if (OperationStack.Last().RPNType == ERPNType.LeftBracket)
                        {
                            OperationStack.Remove(OperationStack.Last());
                        }
                        if (IsVariableInitialization(OperationStack.Last()))
                        {
                            Output.Add(TranslateToRPNSymbol(Input[1]));
                            Input.Remove(Input[1]);
                            Output.Add(new RPNSymbol(ToArrayInit(OperationStack.Last())));
                        }
                        OperationStack.Remove(OperationStack.Last());
                    }
                    //Иначе - в Output записывается операция индексации
                    else if (OperationStack.Count > 0)
                    {
                        Output.Add(new RPNSymbol(ERPNType.FuncIndex));
                        OperationStack.Remove(OperationStack.Last());
                    }
                }
                //Если входная лексема - правая фигурная скобка, то в Output записываются все операции из OperationStack пока там не найдётся левая фигурная скобка
                else if ((OperationStack.Count > 0) && (input.RPNType == ERPNType.RightBrace))
                {
                    while ((OperationStack.Count > 0) && (OperationStack.Last().RPNType != ERPNType.LeftBrace))
                    {
                        if (IsWritable(OperationStack.Last()))
                        {
                            Output.Add(OperationStack.Last());
                        }
                        OperationStack.Remove(OperationStack.Last());
                    }
                    //Если после закрытия правой фигурной скобки в исходной строке стоит Else - записываем в ОПС операцию безусловного перехода к метке Else
                    if ((Input.Count > 1) && (Input[1].TerminalType == ETerminalType.Else))
                    {
                        Output.Add(new RPNSymbol(ERPNType.JumpToMarkElse));
                        Input.Remove(Input.First());
                    }
                    for (int i = Output.Count-1; i >= 0; i--)
                    {
                        //Если в Output встретится метка перехода к If - записываем в Output метку перехода к Else
                        if (Output[i].RPNType == ERPNType.MarkIf)
                        {
                            Output.Add(new RPNSymbol(ERPNType.MarkElse));
                            break;
                        }
                        //Иначе если в Output встретится операция условного перехода к метке If - записываем в Output метку If
                        else if (Output[i].RPNType == ERPNType.ConditionalJumpToMarkIf)
                        {
                            Output.Add(new RPNSymbol(ERPNType.MarkIf));
                            break;
                        }
                        //Иначе если В в Output встретится операция условного перехода к метке конца цикла While - записываем в Output операции перехода к метке начала цикла и метку конца цикла
                        else if (Output[i].RPNType == ERPNType.ConditionalJumpToWhileEnd)
                        {
                            Output.Add(new RPNSymbol(ERPNType.JumpToWhileBegin));
                            Output.Add(new RPNSymbol(ERPNType.MarkWhileEnd));
                            break;
                        }
                    }
                }

                else if ((input.RPNType == ERPNType.LeftParen) || (input.RPNType == ERPNType.LeftBracket))
                {

                }
                else if (input.RPNType == ERPNType.LeftBrace)
                {
                    if (OperationStack.Last().RPNType == ERPNType.ConditionalJumpToMarkIf)
                    {
                        Output.Add(OperationStack.Last());
                        OperationStack.Remove(OperationStack.Last());
                    }
                }
                else
                {
                    while ((OperationStack.Count > 0) && (GetRPNSymbolPriority(OperationStack.Last()) > GetRPNSymbolPriority(input)))
                    {
                        if (IsWritable(OperationStack.Last()))
                        {
                            Output.Add(OperationStack.Last());
                        }
                        OperationStack.Remove(OperationStack.Last());
                    }
                }
            }
            if (IsWritable(input))
            {
                OperationStack.Add(input);
            }
        }
        /// <summary>
        /// Перевод терминала в символ ОПС
        /// </summary>
        public static RPNSymbol TranslateToRPNSymbol(Terminal input) => input.TerminalType switch
        {
            ETerminalType.Assignment => new RPNSymbol(ERPNType.FuncAssignment),
            ETerminalType.And => new RPNSymbol(ERPNType.FuncAnd),
            ETerminalType.Or => new RPNSymbol(ERPNType.FuncOr),
            ETerminalType.Equal => new RPNSymbol(ERPNType.FuncOr),
            ETerminalType.Less => new RPNSymbol(ERPNType.FuncLess),
            ETerminalType.Greater => new RPNSymbol(ERPNType.FuncGreater),
            ETerminalType.LessEqual => new RPNSymbol(ERPNType.FuncLessEqual),
            ETerminalType.GreaterEqual => new RPNSymbol(ERPNType.FuncGreaterEqual),
            ETerminalType.Plus => new RPNSymbol(ERPNType.FuncPlus),
            ETerminalType.Minus => new RPNSymbol(ERPNType.FuncMinus),
            ETerminalType.Multiply => new RPNSymbol(ERPNType.FuncMultiply),
            ETerminalType.Divide => new RPNSymbol(ERPNType.FuncDivide),
            ETerminalType.Modulus => new RPNSymbol(ERPNType.FuncModulus),
            ETerminalType.Not => new RPNSymbol(ERPNType.FuncNot),
            ETerminalType.Int => new RPNSymbol(ERPNType.FuncInt),
            ETerminalType.String => new RPNSymbol(ERPNType.FuncString),
            ETerminalType.Bool => new RPNSymbol(ERPNType.FuncBool),
            ETerminalType.Input => new RPNSymbol(ERPNType.FuncInput),
            ETerminalType.Output => new RPNSymbol(ERPNType.FuncOutput),
            ETerminalType.LeftBracket => new RPNSymbol(ERPNType.LeftBracket),
            ETerminalType.RightBracket => new RPNSymbol(ERPNType.RightBracket),
            ETerminalType.LeftParen => new RPNSymbol(ERPNType.LeftParen),
            ETerminalType.RightParen => new RPNSymbol(ERPNType.RightParen),
            ETerminalType.LeftBrace => new RPNSymbol(ERPNType.LeftBrace),
            ETerminalType.RightBrace => new RPNSymbol(ERPNType.RightBrace),
            ETerminalType.If => new RPNSymbol(ERPNType.ConditionalJumpToMarkIf),
            ETerminalType.Else => new RPNSymbol(ERPNType.JumpToMarkElse),
            ETerminalType.Identifier => new RPNSymbol(ERPNType.Identifier),
            ETerminalType.Number => new RPNSymbol(ERPNType.Number),
            ETerminalType.TextLine => new RPNSymbol(ERPNType.TextLine),
            ETerminalType.Boolean => new RPNSymbol(ERPNType.Boolean),
            ETerminalType.Semicolon => new RPNSymbol(ERPNType.Semicolon),
            ETerminalType.While => new RPNSymbol(ERPNType.MarkWhileBegin),
            _ => throw new NotImplementedException("КРАШНУТЬСЯ НАФИГ")
        };
        /// <summary>
        /// Возвращает приоритет символа ОПС
        /// </summary>
        public static int GetRPNSymbolPriority(RPNSymbol input) => input.RPNType switch
        {
            ERPNType.ConditionalJumpToMarkIf => -1,
            ERPNType.ConditionalJumpToWhileEnd => -1,
            ERPNType.JumpToMarkElse => 10,
            ERPNType.MarkWhileBegin => 10,
            ERPNType.Semicolon => -1,
            ERPNType.LeftParen => -1,
            ERPNType.LeftBrace => -1,

            ERPNType.FuncAssignment => 0,
            ERPNType.FuncAnd => 1,
            ERPNType.FuncOr => 1,
            ERPNType.FuncEqual => 2,
            ERPNType.FuncLess => 2,
            ERPNType.FuncGreater => 2,
            ERPNType.FuncLessEqual => 2,
            ERPNType.FuncGreaterEqual => 2,
            ERPNType.FuncPlus => 3,
            ERPNType.FuncMinus => 3,
            ERPNType.FuncMultiply => 4,
            ERPNType.FuncDivide => 4,
            ERPNType.FuncModulus => 4,
            ERPNType.FuncNot => 5,
            ERPNType.FuncInt => 6,
            ERPNType.FuncString => 6,
            ERPNType.FuncBool => 6,
            ERPNType.FuncIntArray => 6,
            ERPNType.FuncStringArray => 6,
            ERPNType.FuncBoolArray => 6,
            ERPNType.FuncInput => 7,
            ERPNType.FuncOutput => 7,
            ERPNType.FuncIndex => 8,
            _ => throw new NotImplementedException("КРАШНУТЬСЯ НАФИГ НО ПОНИЖЕ")
        };
        /// <summary>
        /// Перевод функции инициализации одной переменной в функцию инициализации массива этого же типа
        /// </summary>
        public static ERPNType ToArrayInit(RPNSymbol input) => input.RPNType switch
        {
            ERPNType.FuncInt => ERPNType.FuncIntArray,
            ERPNType.FuncString => ERPNType.FuncStringArray,
            ERPNType.FuncBool => ERPNType.FuncBoolArray,
            _ => throw new NotImplementedException("КРАШНУТЬСЯ НАФИГ НО ЕЩЁ НИЖЕ")
        };
    }
}
