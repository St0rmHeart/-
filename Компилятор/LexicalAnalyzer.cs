using System.Globalization;

namespace Компилятор
{
    public static class LexicalAnalyzer
    {
        private static string Data { get; set; }
        private static List<Terminal> Terminals { get; } = [];

        private static int _charPointer = 1;
        private static int _linePointer = 1;
        private static int _char = 1;

        private static int _pointer = 0;
        private static int Pointer
        {
            get
            {
                return _pointer;
            }
            set
            {
                _pointer++;
                _charPointer++;
                if(Data.ElementAtOrDefault(_pointer) == '\n')
                {
                    _linePointer++;
                    _charPointer = 0;
                    _char = 1;
                }
            }
        }
        private static char CurrentChar
        {
            get
            {
                return Data[Pointer];
            }
        }
        public static bool IsLexicalCorrect(string data)
        {
            Data = data;


          
            while (Pointer < data.Length)
            {
                Start_Analyse();
            }
            return true;
        }
        public static List<Terminal> GetTerminals()
        {
            return Terminals;
        }
        private static void ReadTerminal(ETerminal terminalType)
        {
            Terminals.Add(new Terminal(terminalType, _linePointer, _char));
        }
        private static void ReadTerminal(ETerminal terminalType, string value)
        {
            
            switch (terminalType)
            {
                case ETerminal.Number:
                    Terminals.Add(new Terminal.Number(terminalType, _linePointer, _char, value));
                    break;
                case ETerminal.TextLine:
                    Terminals.Add(new Terminal.TextLine(terminalType, _linePointer, _char, value));
                    break;
                case ETerminal.Boolean:
                    Terminals.Add(new Terminal.Boolean(terminalType, _linePointer, _char, value));
                    break;
                case ETerminal.VariableName:
                    Terminals.Add(new Terminal.VariableName(terminalType, _linePointer, _char, value));
                    break;
                default:
                    throw new NotImplementedException("Невозможный тип терминала");
            }
        }
        private static string CurentCharGroup()
        {
            if (CurrentChar >= '0' && CurrentChar <= '9')
                return "<ц>";

            else if (CurrentChar >= 'a' && CurrentChar <= 'z' ||
                     CurrentChar >= 'A' && CurrentChar <= 'Z' ||
                     CurrentChar == '_')
                return "<б>";

            else if (CurrentChar == '\"') return "<\">";
            else if (CurrentChar == ' ') return "< >";
            else if (CurrentChar == '\n') return "< >";
            else if (CurrentChar == ';') return "<;>";

            else if (CurrentChar == '+') return "<+>";
            else if (CurrentChar == '-') return "<->";
            else if (CurrentChar == '*') return "<*>";
            else if (CurrentChar == '/') return "</>";
            else if (CurrentChar == '%') return "<%>";

            else if (CurrentChar == '<') return "<<>";
            else if (CurrentChar == '>') return "<>>";
            else if (CurrentChar == '=') return "<=>";
            else if (CurrentChar == '&') return "<&>";
            else if (CurrentChar == '|') return "<|>";
            else if (CurrentChar == '!') return "<!>";
            else if (CurrentChar == '(') return "<(>";
            else if (CurrentChar == ')') return "<)>";
            else if (CurrentChar == '[') return "<[>";
            else if (CurrentChar == ']') return "<]>";
            else if (CurrentChar == '{') return "<{>";
            else if (CurrentChar == '}') return "<}>";

            else if (CurrentChar >= 'а' && CurrentChar <= 'я' ||
                     CurrentChar >= 'А' && CurrentChar <= 'Я' ||
                     CurrentChar == '?' ||
                     CurrentChar == ',' ||
                     CurrentChar == '.')
                return "<o>";

            else
            {
                return "Error";
            }
        }
        
        private static void Start_Analyse()
        {
            _char = _charPointer;
            switch (CurentCharGroup())
            {
                case "<ц>":
                    NUM_Analyse();
                    break;

                case "<б>":
                    ID_Analyse();
                    break;

                case "< >":
                    Pointer++;
                    break;

                case "<\">":
                    STR_Analyse();
                    break;

                case "<;>":
                    ReadTerminal(ETerminal.Semicolon);
                    Pointer++;
                    break;

                case "<+>":
                    ReadTerminal(ETerminal.Plus);
                    Pointer++;
                    break;

                case "<->":
                    ReadTerminal(ETerminal.Minus);
                    Pointer++;
                    break;

                case "<*>":
                    ReadTerminal(ETerminal.Multiply);
                    Pointer++;
                    break;

                case "</>":
                    ReadTerminal(ETerminal.Divide);
                    Pointer++;
                    break;

                case "<%>":
                    ReadTerminal(ETerminal.Modulus);
                    Pointer++;
                    break;

                case "<<>":
                    LESS_Analyse();
                    break;

                case "<>>":
                    MORE_Analyse();
                    break;

                case "<=>":
                    EQUAL_Analyse();
                    break;

                case "<&>":
                    AND_Analyse();
                    break;

                case "<|>":
                    OR_Analyse();
                    break;

                case "<!>":
                    ReadTerminal(ETerminal.Not);
                    Pointer++;
                    break;

                case "<(>":
                    ReadTerminal(ETerminal.LeftParen);
                    Pointer++;
                    break;

                case "<)>":
                    ReadTerminal(ETerminal.RightParen);
                    Pointer++;
                    break;

                case "<[>":
                    ReadTerminal(ETerminal.LeftBracket);
                    Pointer++;
                    break;

                case "<]>":
                    ReadTerminal(ETerminal.RightBracket);
                    Pointer++;
                    break;

                case "<{>":
                    ReadTerminal(ETerminal.LeftBrace);
                    Pointer++;
                    break;

                case "<}>":
                    ReadTerminal(ETerminal.RightBrace);
                    Pointer++;
                    break;

                default:
                    Console.WriteLine($"Некорректный сомвол: {CurrentChar}" +
                    $"\tСтрока {_linePointer};" +
                    $"\tСимвол {_charPointer};");
                    throw new Exception($"\nНедопустимый символ\nСтрока {_linePointer};\nСимвол {_charPointer};\n");
            }
        }
        private static void NUM_Analyse()
        {
            string number = string.Empty;
            do
            {
                number += CurrentChar;
                Pointer++;
            }
            while (CurentCharGroup() == "<ц>");
            ReadTerminal(ETerminal.Number, number);
        }
        private static void ID_Analyse()
        {
            string identifier = string.Empty;
            do
            {
                identifier += CurrentChar;
                Pointer++;
            }
            while (Pointer < Data.Length && (CurentCharGroup() == "<ц>" || CurentCharGroup() == "<б>"));
            
            switch (identifier)
            {
                case "while":
                    ReadTerminal(ETerminal.While);
                    break;

                case "if":
                    ReadTerminal(ETerminal.If);
                    break;

                case "else":
                    ReadTerminal(ETerminal.Else);
                    break;

                case "int":
                    ReadTerminal(ETerminal.Int);
                    break;

                case "string":
                    ReadTerminal(ETerminal.String);
                    break;

                case "bool":
                    ReadTerminal(ETerminal.Bool);
                    break;

                case "input":
                    ReadTerminal(ETerminal.Input);
                    break;

                case "output":
                    ReadTerminal(ETerminal.Output);
                    break;

                default:
                    ReadTerminal(ETerminal.VariableName, identifier);
                    break;
            }

        }
        private static void STR_Analyse()
        {
            Pointer++;
            string textLine = string.Empty;
            do
            {
                textLine += CurrentChar;
                Pointer++;
            }
            while (CurentCharGroup() != "<\">");
            Pointer++;
            ReadTerminal(ETerminal.TextLine, textLine);
        }
        private static void LESS_Analyse()
        {
            if (Data[Pointer+1] == '=')
            {
                ReadTerminal(ETerminal.LessEqual);
                Pointer++;
            }
            else
            {
                ReadTerminal(ETerminal.Less);
            }
            Pointer++;
        }
        private static void MORE_Analyse()
        {
            if (Data[Pointer + 1] == '=')
            {
                ReadTerminal(ETerminal.GreaterEqual);
                Pointer++;
            }
            else
            {
                ReadTerminal(ETerminal.Greater);
            }
            Pointer++;
        }
        private static void EQUAL_Analyse()
        {
            if (Data[Pointer + 1] == '=')
            {
                ReadTerminal(ETerminal.Equal);
                Pointer++;
            }
            else
            {
                ReadTerminal(ETerminal.Assignment);
            }
            Pointer++;
        }
        private static void AND_Analyse()
        {
            if (Data[Pointer + 1] == '&')
            {
                ReadTerminal(ETerminal.And);
                Pointer++;
                Pointer++;
            }
            else
            {
                Console.WriteLine($"Некорректный символ: {CurrentChar}" +
                    $"\tСтрока {_linePointer};" +
                    $"\tСимвол {_charPointer};");
                throw new NotImplementedException();
            }
        }
        private static void OR_Analyse()
        {
            if (Data[Pointer + 1] == '&')
            {
                ReadTerminal(ETerminal.And);
                Pointer++;
                Pointer++;
            }
            else
            {
                Console.WriteLine($"Некорректный символ: {CurrentChar}" +
                    $"\tСтрока {_linePointer};" +
                    $"\tСимвол {_charPointer};");
                throw new NotImplementedException();
            }
        }
    }
}
