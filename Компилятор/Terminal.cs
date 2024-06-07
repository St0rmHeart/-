namespace Компилятор
{
    public class Terminal
    {
        /// <summary>
        /// Тип терминала
        /// </summary>
        public ETerminalType TerminalType { get; }
        public Terminal(ETerminalType type)
        {
            TerminalType = type;
        }
        public class TextLine : Terminal
        {
            public string Data { get; private set; }
            public TextLine(ETerminalType type, string data) : base(type)
            {
                if (type != ETerminalType.TextLine) throw new ArgumentException("Неверно создан нетерминал");
                Data = data;
            }
        }
        public class Number : Terminal
        {
            public int Data { get; }
            public Number(ETerminalType type, string data) : base(type)
            {
                if (type != ETerminalType.Number) throw new ArgumentException("Неверно создан нетерминал");
                Data = Convert.ToInt32(data);
            }
        }
        public class Boolean : Terminal
        {
            public static bool Data { get; private set; }
            public Boolean(ETerminalType type, string data) : base(type)
            {
                if (type != ETerminalType.Boolean) throw new ArgumentException("Неверно создан нетерминал");
                Data = Convert.ToBoolean(data);
            }
        }
        public class Identifier : Terminal
        {
            public string Name { get; }
            public Identifier(ETerminalType type, string name) : base(type)
            {
                if (type != ETerminalType.VariableName) throw new ArgumentException("Неверно создан нетерминал");
                Name = name;
            }
        }
    }
    

}