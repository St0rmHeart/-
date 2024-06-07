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
    public class RPNTranslator
    {
        List<Terminal> Input = new List<Terminal>();
        public List<RPNSymbol> Output = new List<RPNSymbol>();
        List<RPNSymbol> OperationStack = new List<RPNSymbol>();
        public List<RPNSymbol> Translate(List<Terminal> input)
        {
            Input = input;
            while (Input.Count > 0)
            {
                if (IsOperationOrParenthesis(Input[0]))
                {
                    ToStack(TranslateToRPNSymbol(Input[0]));
                    Input.Remove(Input.First());
                }
                else if (IsTerminal(Input[0]))
                {
                    Output.Add(TranslateToRPNSymbol(Input[0]));
                    Input.Remove(Input.First());
                }
                else if (Input[0].TerminalType == ETerminalType.While)
                {
                    Output.Add(new RPNSymbol(ERPNType.MarkWhileBegin));
                    OperationStack.Add(new RPNSymbol(ERPNType.ConditionalJumpToWhileEnd));
                    Input.Remove(Input.First());
                }
            }
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
        public static bool IsWritable(RPNSymbol input)
        {
            if ((input.RPNType == ERPNType.Semicolon) || (input.RPNType == ERPNType.RightParen) || (input.RPNType == ERPNType.RightBracket) || (input.RPNType == ERPNType.RightBrace) || (input.RPNType == ERPNType.LeftBrace) || (input.RPNType == ERPNType.LeftParen))
            {
                return false;
            }
            return true;
        }
        public static bool IsTerminal(Terminal input)
        {
            if ((input.TerminalType == ETerminalType.Number) || (input.TerminalType == ETerminalType.TextLine) || (input.TerminalType == ETerminalType.Boolean) || (input.TerminalType == ETerminalType.VariableName))
            {
                return true;
            }
            return false;
        }
        public static bool IsOperationOrParenthesis(Terminal input)
        {
            if ((input.TerminalType == ETerminalType.Int) || (input.TerminalType == ETerminalType.String) || (input.TerminalType == ETerminalType.Bool) || (input.TerminalType == ETerminalType.Else) || (input.TerminalType == ETerminalType.If) || (input.TerminalType == ETerminalType.Semicolon) || (input.TerminalType == ETerminalType.Output) || (input.TerminalType == ETerminalType.Input) || (input.TerminalType == ETerminalType.Assignment) || (input.TerminalType == ETerminalType.And) || (input.TerminalType == ETerminalType.Or) || (input.TerminalType == ETerminalType.Equal) || (input.TerminalType == ETerminalType.Less) || (input.TerminalType == ETerminalType.Greater) || (input.TerminalType == ETerminalType.GreaterEqual) || (input.TerminalType == ETerminalType.LessEqual) || (input.TerminalType == ETerminalType.Plus) || (input.TerminalType == ETerminalType.Minus) || (input.TerminalType == ETerminalType.Divide) || (input.TerminalType == ETerminalType.Multiply) || (input.TerminalType == ETerminalType.Modulus) || (input.TerminalType == ETerminalType.Not) ||  (input.TerminalType == ETerminalType.LeftParen) || (input.TerminalType == ETerminalType.RightParen) || (input.TerminalType == ETerminalType.RightBracket) || (input.TerminalType == ETerminalType.LeftBracket) || (input.TerminalType == ETerminalType.RightBrace) || (input.TerminalType == ETerminalType.LeftBrace))
            {
                return true;
            }
            return false;
        }
        public static bool IsVariableInitialization(RPNSymbol input)
        {
            if ((input.RPNType == ERPNType.Func_Int) || (input.RPNType == ERPNType.Func_String) || (input.RPNType == ERPNType.Func_Bool))
            {
                return true;
            }
            return false;
        }
        public void ToStack(RPNSymbol input)
        {
            if (OperationStack.Count > 0)
            {
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
                    else if (OperationStack.Count > 0)
                    {
                        Output.Add(new RPNSymbol(ERPNType.Func_Index));
                        OperationStack.Remove(OperationStack.Last());
                    }
                }

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
                    if ((Input.Count > 1) && (Input[1].TerminalType == ETerminalType.Else))
                    {
                        Output.Add(new RPNSymbol(ERPNType.JumpToMarkElse));
                        Input.Remove(Input.First());
                    }
                    for (int i = Output.Count-1; i >= 0; i--)
                    {
                        if (Output[i].RPNType == ERPNType.MarkIf)
                        {
                            Output.Add(new RPNSymbol(ERPNType.MarkElse));
                            break;
                        }
                        else if (Output[i].RPNType == ERPNType.ConditionalJumpToMarkIf)
                        {
                            Output.Add(new RPNSymbol(ERPNType.MarkIf));
                            break;
                        }
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
        public static RPNSymbol TranslateToRPNSymbol(Terminal input) => input.TerminalType switch
        {
            ETerminalType.Assignment => new RPNSymbol(ERPNType.Func_Assignment),
            ETerminalType.And => new RPNSymbol(ERPNType.Func_And),
            ETerminalType.Or => new RPNSymbol(ERPNType.Func_Or),
            ETerminalType.Equal => new RPNSymbol(ERPNType.Func_Or),
            ETerminalType.Less => new RPNSymbol(ERPNType.Func_Less),
            ETerminalType.Greater => new RPNSymbol(ERPNType.Func_Greater),
            ETerminalType.LessEqual => new RPNSymbol(ERPNType.Func_LessEqual),
            ETerminalType.GreaterEqual => new RPNSymbol(ERPNType.Func_GreaterEqual),
            ETerminalType.Plus => new RPNSymbol(ERPNType.Func_Plus),
            ETerminalType.Minus => new RPNSymbol(ERPNType.Func_Minus),
            ETerminalType.Multiply => new RPNSymbol(ERPNType.Func_Multiply),
            ETerminalType.Divide => new RPNSymbol(ERPNType.Func_Divide),
            ETerminalType.Modulus => new RPNSymbol(ERPNType.Func_Modulus),
            ETerminalType.Not => new RPNSymbol(ERPNType.Func_Not),
            ETerminalType.Int => new RPNSymbol(ERPNType.Func_Int),
            ETerminalType.String => new RPNSymbol(ERPNType.Func_String),
            ETerminalType.Bool => new RPNSymbol(ERPNType.Func_Bool),
            ETerminalType.Input => new RPNSymbol(ERPNType.Func_Input),
            ETerminalType.Output => new RPNSymbol(ERPNType.Func_Output),
            ETerminalType.LeftBracket => new RPNSymbol(ERPNType.LeftBracket),
            ETerminalType.RightBracket => new RPNSymbol(ERPNType.RightBracket),
            ETerminalType.LeftParen => new RPNSymbol(ERPNType.LeftParen),
            ETerminalType.RightParen => new RPNSymbol(ERPNType.RightParen),
            ETerminalType.LeftBrace => new RPNSymbol(ERPNType.LeftBrace),
            ETerminalType.RightBrace => new RPNSymbol(ERPNType.RightBrace),
            ETerminalType.If => new RPNSymbol(ERPNType.ConditionalJumpToMarkIf),
            ETerminalType.Else => new RPNSymbol(ERPNType.JumpToMarkElse),
            ETerminalType.VariableName => new RPNSymbol(ERPNType.Identifier),
            ETerminalType.Number => new RPNSymbol(ERPNType.Number),
            ETerminalType.TextLine => new RPNSymbol(ERPNType.TextLine),
            ETerminalType.Boolean => new RPNSymbol(ERPNType.Boolean),
            ETerminalType.Semicolon => new RPNSymbol(ERPNType.Semicolon),
            ETerminalType.While => new RPNSymbol(ERPNType.MarkWhileBegin),
            _ => throw new NotImplementedException("КРАШНУТЬСЯ НАФИГ")
        };
        public static int GetRPNSymbolPriority(RPNSymbol input) => input.RPNType switch
        {
            ERPNType.ConditionalJumpToMarkIf => 10,
            ERPNType.ConditionalJumpToWhileEnd => -1,
            ERPNType.JumpToMarkElse => 10,
            ERPNType.MarkWhileBegin => 10,
            ERPNType.Semicolon => -1,
            ERPNType.LeftParen => -1,
            ERPNType.LeftBrace => -1,

            ERPNType.Func_Assignment => 0,
            ERPNType.Func_And => 1,
            ERPNType.Func_Or => 1,
            ERPNType.Func_Equal => 2,
            ERPNType.Func_Less => 2,
            ERPNType.Func_Greater => 2,
            ERPNType.Func_LessEqual => 2,
            ERPNType.Func_GreaterEqual => 2,
            ERPNType.Func_Plus => 3,
            ERPNType.Func_Minus => 3,
            ERPNType.Func_Multiply => 4,
            ERPNType.Func_Divide => 4,
            ERPNType.Func_Modulus => 4,
            ERPNType.Func_Not => 5,
            ERPNType.Func_Int => 6,
            ERPNType.Func_String => 6,
            ERPNType.Func_Bool => 6,
            ERPNType.Func_IntArray => 6,
            ERPNType.Func_StringArray => 6,
            ERPNType.Func_BoolArray => 6,
            ERPNType.Func_Input => 7,
            ERPNType.Func_Output => 7,
            ERPNType.Func_Index => 8,
            _ => throw new NotImplementedException("КРАШНУТЬСЯ НАФИГ НО ПОНИЖЕ")
        };
        public static ERPNType ToArrayInit(RPNSymbol input) => input.RPNType switch
        {
            ERPNType.Func_Int => ERPNType.Func_IntArray,
            ERPNType.Func_String => ERPNType.Func_StringArray,
            ERPNType.Func_Bool => ERPNType.Func_BoolArray,
            _ => throw new NotImplementedException("КРАШНУТЬСЯ НАФИГ НО ЕЩЁ НИЖЕ")
        };
    }
}
