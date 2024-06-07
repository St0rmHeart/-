namespace Компилятор
{
    public static class Programm
    {
        public static void Main()
        {
            var code = FileReader.Read("data.txt");
            var lexicalAnalizatorResult = LexicalAnalyzer.IsLexicalCorrect(code);
            List<Terminal> terminals = [];
            List<RPNSymbol> rpn = [];
            if (lexicalAnalizatorResult.IsCorrect)
            {
                terminals = lexicalAnalizatorResult.Terminals;
            }
            bool isSyntacticalCorrect = SyntacticalAnalyzer.ParseInstructionBlock(terminals);
            File.WriteAllText("SAlog.txt", SyntacticalAnalyzer.GetLog());
            if (isSyntacticalCorrect)
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