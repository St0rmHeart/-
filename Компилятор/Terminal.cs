using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Компилятор
{
    public class Terminal
    {
        /// <summary>
        /// Тип терминала
        /// </summary>
        public ETerminal TerminalType { get; }
        public int CharPointer { get; set; }
        public int LinePointer { get; set; }
        public Terminal(ETerminal type, int linePointer, int charPointer)
        {
            TerminalType = type;
            LinePointer = linePointer;
            CharPointer = charPointer;
        }
        public Terminal(ETerminal type)
        {
            TerminalType = type;
        }


        public class TextLine : Terminal
        {
            public string Data { get; private set; }
            public TextLine(ETerminal type, int linePointer, int charPointer, string data) : base(type, linePointer, charPointer)
            {
                if (type != ETerminal.TextLine) throw new ArgumentException("Неверно создан нетерминал");
                Data = data;
            }
            public TextLine(ETerminal type, string data) : base(type)
            {
                if (type != ETerminal.TextLine) throw new ArgumentException("Неверно создан нетерминал");
                Data = data;
            }
        }
        public class Number : Terminal
        {
            public int Data { get; }
            public Number(ETerminal type, int linePointer, int charPointer, string data) : base(type, linePointer, charPointer)
            {
                if (type != ETerminal.Number) throw new ArgumentException("Неверно создан нетерминал");
                Data = Convert.ToInt32(data);
            }
            public Number(ETerminal type, int data) : base(type)
            {
                if (type != ETerminal.Number) throw new ArgumentException("Неверно создан нетерминал");
                Data = data;
            }
        }
        public class Boolean : Terminal
        {
            public bool Data { get; private set; }
            public Boolean(ETerminal type, int linePointer, int charPointer, string data) : base(type, linePointer, charPointer)
            {
                if (type != ETerminal.Boolean) throw new ArgumentException("Неверно создан нетерминал");
                Data = Convert.ToBoolean(data);
            }
            public Boolean(ETerminal type, bool data) : base(type)
            {
                if (type != ETerminal.Boolean) throw new ArgumentException("Неверно создан нетерминал");
                Data = data;
            }

        }
        public class VariableName : Terminal
        {
            public string Name { get; }
            public VariableName(ETerminal type, int linePointer, int charPointer, string data) : base(type, linePointer, charPointer)
            {
                if (type != ETerminal.VariableName) throw new ArgumentException("Неверно создан нетерминал");
                Name = data;
            }
            public VariableName(ETerminal type, string name) : base(type)
            {
                if (type != ETerminal.VariableName) throw new ArgumentException("Неверно создан нетерминал");
                Name = name;
            }
        }
        public class MarkDestination : Terminal
        {
            private static int _id = 1;
            public int DMID { get; set; } 
            public EMarkType MarkType { get; set; }
            public MarkDestination(ETerminal type, EMarkType markType = EMarkType.None) : base(type)
            {
                MarkType = markType;
                DMID = _id;
                _id++;
            }
        }
        public class MarkPointer : Terminal
        {
            public MarkDestination DestinationMark { get; }
            public int Destination {  get; set; }
            public MarkPointer(ETerminal type, MarkDestination destination) : base(type)
            {
                DestinationMark = destination;
            }
        }
    }
    

}