namespace Компилятор
{
    public static class Programm
    {
        public static void Main()
        {
            var code = FileReader.Read("data.txt");
            var lexicalAnalizatorResult = LexicalAnalyzer.IsLexicalCorrect(code);
            List<Terminal> terminals = [];
            if (lexicalAnalizatorResult.IsCorrect)
            {
                terminals = lexicalAnalizatorResult.Terminals;
            }
            SyntacticalAnalyzer.ParseInstructionBlock(terminals);
            
            var log = SyntacticalAnalyzer.GetLog();
            // Записываем строку в файл
            File.WriteAllText("SAlog.txt", log);
        }
    }
}