namespace Компилятор
{
    public static class Programm
    {
        public static void Main()
        {
            LexicalAnalyzer L_Analyzer = new();
            L_Analyzer.Data = "int a; a = 5 * (4 + 3);";
            Console.WriteLine(L_Analyzer.IsLexicalCorrect());
            Console.WriteLine(SyntacticalAnalyzer.ParseInstructionBlock(L_Analyzer.Terminals));
        }
    }
}