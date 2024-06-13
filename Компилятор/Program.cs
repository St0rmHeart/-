using System.Security.AccessControl;

namespace Компилятор
{
    public static class Programm
    {
        public static void Main()
        {
            var code = FileReader.Read("data.txt");

            List<Terminal> terminals;
            if (LexicalAnalyzer.IsLexicalCorrect(code))
            {
                terminals = LexicalAnalyzer.GetTerminals();
            }
            else
            {
                throw new Exception();
            }

            List<Terminal> rpn;
            if (SyntacticalAnalyzer.IsSyntacticalCorrect(terminals))
            {
                rpn = RPNTranslator.ConvertToRPN(terminals);
            }
            else
            {
                throw new Exception();
            }
            RPNReader.ExecuteRPN(rpn);
            
        }
    }
}