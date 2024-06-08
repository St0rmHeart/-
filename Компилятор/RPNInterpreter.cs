using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Компилятор
{
    public class RPNInterpreter
    {
        public static void ExecuteInstructions(List<RPNSymbol> rpn)
        {
            int currentLineNumber = 0;
            int markedLineNumber = -1;
            var variables = new Dictionary<string, List<string>>();
            var stack = new Stack<RPNSymbol>();


            foreach (var symbol in rpn)
            {
             
                if (currentLineNumber >= markedLineNumber)
                {
                    switch (symbol)
                    {
                        case RPNMark:
                            stack.Push(symbol);
                            break;
                        case RPNTextLine:
                            stack.Push(symbol);
                            break;
                        case RPNNumber:
                            stack.Push(symbol);
                            break;
                        case RPNBoolean:
                            stack.Push(symbol);
                            break;
                        case RPNIdentifier:
                            stack.Push(symbol);
                            break;
                        case RPNSymbol:
                            // Попытка создания переменной типа int
                            if (symbol.RPNType == ERPNType.F_Int) 
                            {
                                if (stack.Peek() is RPNIdentifier var)
                                {
                                    stack.Pop();
                                    if (variables.ContainsKey(var.Name))
                                    {
                                        throw new Exception("Переменная уже объявлена");
                                    }

                                    variables.Add(var.Name, new List<string> { $"{ERPNType.A_Number}", $"" });

                                    Console.WriteLine($"int {var.Name}");
                                }
                                else
                                {
                                    throw new Exception("После типа 'int' ожидался идентификатор переменной");
                                }                               
                            }

                            // Попытка создания переменной типа string
                            else if (symbol.RPNType == ERPNType.F_String)
                            {
                                if (stack.Peek() is RPNIdentifier var)
                                {
                                    stack.Pop();
                                    if (variables.ContainsKey(var.Name))
                                    {
                                        throw new Exception("Переменная уже объявлена");
                                    }

                                    variables.Add(var.Name, new List<string> { $"{ERPNType.A_TextLine}", $"" });

                                    Console.WriteLine($"string {var.Name}");
                                }
                                else
                                {
                                    throw new Exception("После типа 'string' ожидался идентификатор переменной");
                                }
                            }

                            // Попытка создания переменной типа bool
                            else if (symbol.RPNType == ERPNType.F_Bool)
                            {
                                if (stack.Peek() is RPNIdentifier var)
                                {
                                    stack.Pop();
                                    if (variables.ContainsKey(var.Name))
                                    {
                                        throw new Exception("Переменная уже объявлена");
                                    }

                                    variables.Add(var.Name, new List<string> { $"{ERPNType.A_Boolean}", $"" });

                                    Console.WriteLine($"bool {var.Name}");
                                }
                                else
                                {
                                    throw new Exception("После типа 'bool' ожидался идентификатор переменной");
                                }
                            }

                            // Попытка создания массива типа int
                            else if (symbol.RPNType == ERPNType.F_IntArray)
                            {
                                if (stack.Peek() is RPNIdentifier var)
                                {
                                    stack.Pop();
                                    if (variables.ContainsKey(var.Name))
                                    {
                                        throw new Exception("Переменная уже объявлена");
                                    }

                                    if (stack.Peek() is RPNNumber number)
                                    {
                                        stack.Pop();
                                        variables.Add(var.Name, new List<string> { $"{ERPNType.A_Number}", $"{number.Data}" });

                                        Console.WriteLine($"int[] {var.Name} = {number.Data}");
                                    }
                                    else
                                    {
                                        throw new Exception("После типа 'int[]' ожидалось число элементов массива");
                                    }
                                }
                                else
                                {
                                    throw new Exception("После типа 'int[]' ожидался идентификатор массива");
                                }
                            }
                            // Попытка присвоения значения переменной
                            else if (symbol.RPNType == ERPNType.F_Assignment)
                            {

                                if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;
                                    string var1 = (stack.Pop() as RPNIdentifier).Name;

                                    if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var1][0] != variables[var2][0])
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    variables[var1][1] = variables[var2][1];
                                    Console.WriteLine($"{variables[var1][0]} {var1} = {variables[var1][1]}");
                                }

                                else if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();
                                    string var = (stack.Pop() as RPNIdentifier).Name;
                                    if (!variables.ContainsKey(var))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }
                                    if (variables[var][0] != ERPNType.A_Number.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }
                                    variables[var][1] = number;
                                    Console.WriteLine($"{variables[var][0]} {var} = {variables[var][1]}");
                                }

                                else if (stack.Peek() is RPNBoolean)
                                {
                                    bool boolean = (stack.Pop() as RPNBoolean).Data;
                                    string var = (stack.Pop() as RPNIdentifier).Name;
                                    if (!variables.ContainsKey(var))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var][0] != ERPNType.A_Boolean.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    variables[var][1] = boolean.ToString();
                                    Console.WriteLine($"{variables[var][0]} {var} = {variables[var][1]}");
                                }

                                else if (stack.Peek() is RPNTextLine)
                                {
                                    string text = (stack.Pop() as RPNTextLine).Data;
                                    string var = (stack.Pop() as RPNIdentifier).Name;
                                    if (!variables.ContainsKey(var))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var][0] != ERPNType.A_TextLine.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    variables[var][1] = text;
                                    Console.WriteLine($"{variables[var][0]} {var} = {variables[var][1]}");
                                }
                            }
                            // Сравненение аргументов "больше"
                            else if (symbol.RPNType == ERPNType.F_Greater)
                            {
                                if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();
                                    string var = (stack.Pop() as RPNIdentifier).Name;

                                    if (!variables.ContainsKey(var))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var][0] != ERPNType.A_Number.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    RPNBoolean boolResult = new RPNBoolean(ERPNType.A_Boolean);

                                    boolResult.Data = int.Parse(variables[var][1]) > int.Parse(number);

                                    stack.Push(boolResult);
                                }

                                else if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;
                                    string var1 = (stack.Pop() as RPNIdentifier).Name;

                                    if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var1][0] != ERPNType.A_Number.ToString() || variables[var2][0] != ERPNType.A_Number.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    RPNBoolean boolResult = new RPNBoolean(ERPNType.A_Boolean);

                                    boolResult.Data = int.Parse(variables[var1][1]) > int.Parse(variables[var2][1]);

                                    stack.Push(boolResult);
                                }
                                else
                                {
                                    throw new Exception("Неверный тип аргументов");
                                }
                            }

                            // Сравненение аргументов "меньше"
                            else if (symbol.RPNType == ERPNType.F_Less)
                            {
                                if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();
                                    string var = (stack.Pop() as RPNIdentifier).Name;

                                    if (!variables.ContainsKey(var))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var][0] != ERPNType.A_Number.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    RPNBoolean boolResult = new RPNBoolean(ERPNType.A_Boolean);

                                    boolResult.Data = int.Parse(variables[var][1]) < int.Parse(number);

                                    stack.Push(boolResult);
                                }

                                else if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;
                                    string var1 = (stack.Pop() as RPNIdentifier).Name;

                                    if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                    {
                                        throw new Exception("Обращение к несуществующей переменной!");
                                    }

                                    if (variables[var1][0] != ERPNType.A_Number.ToString() || variables[var2][0] != ERPNType.A_Number.ToString())
                                    {
                                        throw new Exception("Несоответствие типов");
                                    }

                                    RPNBoolean boolResult = new RPNBoolean(ERPNType.A_Boolean);

                                    boolResult.Data = int.Parse(variables[var1][1]) < int.Parse(variables[var2][1]);

                                    stack.Push(boolResult);
                                }
                                else
                                {
                                    throw new Exception("Неверный тип аргументов");
                                }
                            }

                            // Метка условного оператора
                            else if (symbol.RPNType == ERPNType.F_ConditionalJumpToMark)
                            {
                                RPNMark mark = stack.Pop() as RPNMark;

                                if (stack.Pop() is RPNBoolean boolResult)
                                {
                                    if (!boolResult.Data)
                                    {
                                        markedLineNumber = mark.Position.Value;
                                    }
                                }
                                else { throw new Exception("Неверный тип аргументов условного оператора"); }

                            }

                            // Метка оператора
                            else if (symbol.RPNType == ERPNType.F_UnconditionalJumpToMark)
                            {
                                RPNMark mark = stack.Pop() as RPNMark;

                                markedLineNumber = mark.Position.Value;

                            }

                            // Операция сложения
                            else if (symbol.RPNType == ERPNType.F_Plus)
                            {
                                if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var1 = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var1][0] != ERPNType.A_Number.ToString() || variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        if (variables[var1][0] == ERPNType.A_Number.ToString())
                                        {
                                            RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                            rPNNumber.Data = int.Parse(variables[var1][1]) + int.Parse(variables[var2][1]);
                                            stack.Push(rPNNumber);
                                        }
                                        else if (variables[var1][0] == ERPNType.A_TextLine.ToString())
                                        {
                                            RPNTextLine rPNTextLine = new RPNTextLine(ERPNType.A_TextLine);
                                            rPNTextLine.Data = variables[var2][1] + variables[var1][1];
                                            stack.Push(rPNTextLine);
                                        }
                                        else
                                        {
                                            throw new Exception("Неверный тип аргументов");
                                        }
                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number = (stack.Pop() as RPNNumber).Data.ToString();

                                        if (!variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var2][1]) + int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else if (stack.Peek() is RPNTextLine)
                                    {
                                        string text = (stack.Pop() as RPNTextLine).Data;

                                        if (!variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var2][0] != ERPNType.A_TextLine.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNTextLine rPNTextLine = new RPNTextLine(ERPNType.A_TextLine);
                                        rPNTextLine.Data = variables[var2][1] + text;
                                        stack.Push(rPNTextLine);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }                                 
                                }
                                else if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var][1]) + int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number2 = (stack.Pop() as RPNNumber).Data.ToString();

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(number) + int.Parse(number2);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }

                                }
                                else if (stack.Peek() is RPNTextLine)
                                {
                                    string text = (stack.Pop() as RPNTextLine).Data;

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var][0] != ERPNType.A_TextLine.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNTextLine rPNTextLine = new RPNTextLine(ERPNType.A_TextLine);
                                        rPNTextLine.Data = variables[var][1] + text;
                                        stack.Push(rPNTextLine);
                                    }
                                    else if (stack.Peek() is RPNTextLine)
                                    {
                                        string text2 = (stack.Pop() as RPNTextLine).Data;

                                        RPNTextLine rPNTextLine = new RPNTextLine(ERPNType.A_TextLine);
                                        rPNTextLine.Data = text2 + text;
                                        stack.Push(rPNTextLine);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Неверный тип аргументов");
                                }
                            }

                            // Операция вычитания
                            else if (symbol.RPNType == ERPNType.F_Minus)
                            {   
                                if (stack.Count == 2)
                                {   
                                    if (stack.Peek() is RPNNumber)
                                    {
                                        string number = (stack.Pop() as RPNNumber).Data.ToString();
                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = - int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = -int.Parse(variables[var][1]);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }

                                }

                                else if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var1 = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var1][0] != ERPNType.A_Number.ToString() || variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        if (variables[var1][0] == ERPNType.A_Number.ToString())
                                        {
                                            RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                            rPNNumber.Data = int.Parse(variables[var1][1]) - int.Parse(variables[var2][1]);
                                            stack.Push(rPNNumber);
                                        }
                                        else
                                        {
                                            throw new Exception("Неверный тип аргументов");
                                        }
                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number = (stack.Pop() as RPNNumber).Data.ToString();

                                        if (!variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var2][1]) - int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                                else if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var][1]) - int.Parse(number);
                                        stack.Push(rPNNumber);

                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number2 = (stack.Pop() as RPNNumber).Data.ToString();

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(number) - int.Parse(number2);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                            }

                            //Операция умножения
                            else if (symbol.RPNType == ERPNType.F_Multiply)
                            {
                                if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var1 = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var1][0] != ERPNType.A_Number.ToString() || variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        if (variables[var1][0] == ERPNType.A_Number.ToString())
                                        {
                                            RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                            rPNNumber.Data = int.Parse(variables[var1][1]) * int.Parse(variables[var2][1]);
                                            stack.Push(rPNNumber);
                                        }
                                        else
                                        {
                                            throw new Exception("Неверный тип аргументов");
                                        }
                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number = (stack.Pop() as RPNNumber).Data.ToString();

                                        if (!variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var2][1]) * int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                                else if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var = (stack.Pop() as RPNIdentifier).Name;

                                       if (!variables.ContainsKey(var))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var][1]) * int.Parse(number);
                                        stack.Push(rPNNumber);

                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number2 = (stack.Pop() as RPNNumber).Data.ToString();

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(number) * int.Parse(number2);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                            }

                            // Операция деления
                            else if (symbol.RPNType == ERPNType.F_Divide)
                            {
                                if (stack.Peek() is RPNIdentifier)
                                {
                                    string var2 = (stack.Pop() as RPNIdentifier).Name;

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var1 = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var1) || !variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var1][0] != ERPNType.A_Number.ToString() || variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        if (variables[var1][0] == ERPNType.A_Number.ToString())
                                        {
                                            RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                            rPNNumber.Data = int.Parse(variables[var1][1]) / int.Parse(variables[var2][1]);
                                            stack.Push(rPNNumber);
                                        }
                                        else
                                        {
                                            throw new Exception("Неверный тип аргументов");
                                        }
                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number = (stack.Pop() as RPNNumber).Data.ToString();

                                        if (!variables.ContainsKey(var2))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var2][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(number) / int.Parse(variables[var2][1]);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                                else if (stack.Peek() is RPNNumber)
                                {
                                    string number = (stack.Pop() as RPNNumber).Data.ToString();

                                    if (stack.Peek() is RPNIdentifier)
                                    {
                                        string var = (stack.Pop() as RPNIdentifier).Name;

                                        if (!variables.ContainsKey(var))
                                        {
                                            throw new Exception("Обращение к несуществующей переменной!");
                                        }

                                        if (variables[var][0] != ERPNType.A_Number.ToString())
                                        {
                                            throw new Exception("Несоответствие типов");
                                        }

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(variables[var][1]) / int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else if (stack.Peek() is RPNNumber)
                                    {
                                        string number2 = (stack.Pop() as RPNNumber).Data.ToString();

                                        RPNNumber rPNNumber = new RPNNumber(ERPNType.A_Number);
                                        rPNNumber.Data = int.Parse(number2) / int.Parse(number);
                                        stack.Push(rPNNumber);
                                    }
                                    else
                                    {
                                        throw new Exception("Неверный тип аргументов");
                                    }
                                }
                            }
                            break;
                    }
                }
                currentLineNumber++;
                
            }
        }
    }
}
