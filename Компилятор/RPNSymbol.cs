using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Компилятор
{
    public class RPNSymbol
    {
        public ERPNType RPNType { get; set; }
        public RPNSymbol(ERPNType type)
        {
            RPNType = type;
        }
    }
}
