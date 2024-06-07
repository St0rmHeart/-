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

            List<RPNSymbol> rpn;
            if (SyntacticalAnalyzer.IsSyntacticalCorrect(terminals))
            {
                rpn = RPNTranslator.ConvertToRPN(terminals);
            }
            else
            {
                throw new Exception();
            }


            foreach (var rpnsymvol in rpn)
            {
                Console.WriteLine(rpnsymvol.RPNType);
            }
        }
    }
}