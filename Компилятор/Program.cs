namespace Компилятор
{
    public static class Programm
    {
        public static void Main()
        {
            LexicalAnalyzer L_Analyzer = new();
            L_Analyzer.Data = "int a = 4;";
            Console.WriteLine(L_Analyzer.IsLexicalCorrect());
            List<int> list = [1, 2, 3, 4];
            var list1 = list[1..];
            var list2 = list[2..];
            var list3 = list[3..];
            var list4 = list[4..];


        }
    }
}