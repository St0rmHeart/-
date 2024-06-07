namespace Компилятор
{
    public static class Programm
    {
        public static void Main()
        {
            #region тесты
            /*
            LexicalAnalyzer L_Analyzer = new();
            L_Analyzer.Data = "int a = 4;";
            Console.WriteLine(L_Analyzer.IsLexicalCorrect());
            List<int> list = [1, 2, 3, 4];
            var list1 = list[1..];
            var list2 = list[2..];
            var list3 = list[3..];
            var list4 = list[4..];
            */
            List<Terminal> RPNInput = new List<Terminal>();
            //int a; a = 5;
            /*
            RPNInput.Add(new Terminal(ETerminalType.Int));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            */
            //(A[5]+6)*(2-1)
            /*
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Plus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.Multiply));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Minus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            */
            // A[5] = "A"
            /*
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.String));
            */
            //if (a >= 5) { a = a - 2 }
            /*
            RPNInput.Add(new Terminal(ETerminalType.If));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.GreaterEqual));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Minus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            */
            //if (a >= 5) { a = a - 2 } else { a = 2 % 2 }
            /*
            RPNInput.Add(new Terminal(ETerminalType.If));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.GreaterEqual));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Minus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            RPNInput.Add(new Terminal(ETerminalType.Else));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Modulus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            */
            // while (a>b) { a = a - 1 }
            /*
            RPNInput.Add(new Terminal(ETerminalType.While));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.GreaterEqual));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Minus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            */
            // input(a); a = a + 2; output(a);
            
            RPNInput.Add(new Terminal(ETerminalType.Input));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Plus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            RPNInput.Add(new Terminal(ETerminalType.Output));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            
            // int[5] A; A[3] = -2;
            /*
            RPNInput.Add(new Terminal(ETerminalType.Int));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Minus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            */
            //((a+2)-(b+3))*6
            /*
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Plus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.Minus));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Plus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.Multiply));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            */
            //if (a>=2) {if (b==3) { a = 5; } } else { while (b<=2) { 2 + 2 ; } }
            /*
            RPNInput.Add(new Terminal(ETerminalType.If));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.GreaterEqual));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.If));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Equal));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            RPNInput.Add(new Terminal(ETerminalType.Else));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.While));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LessEqual));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Plus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            */
            ///int[5] A;
            ///A[0] = 5;
            ///A[1] = 4;
            ///A[2] = 3;
            ///A[3] = 2;
            ///A[4] = 1;
            ///int i;
            ///i = 0;
            ///int temp;
            ///while (i < 4)
            ///{
            ///   if (i>A[i+1])
            ///   {
            ///      temp = A[i];
            ///      A[i] = A[i+1];
            ///      A[i+1] = temp;
            ///      if (i > 0)
            ///      {
            ///         i = i - 1;
            ///      }
            ///   }
            ///   else
            ///   {
            ///      i = i + 1;
            ///   }
            ///}
            ///
            /*
            RPNInput.Add(new Terminal(ETerminalType.Int));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Int));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Assignment));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));

            RPNInput.Add(new Terminal(ETerminalType.Int));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Semicolon));
            
            
            RPNInput.Add(new Terminal(ETerminalType.While));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Less));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));

            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.If));
            RPNInput.Add(new Terminal(ETerminalType.LeftParen));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.Greater));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.LeftBracket));
            RPNInput.Add(new Terminal(ETerminalType.Identifier));
            RPNInput.Add(new Terminal(ETerminalType.Plus));
            RPNInput.Add(new Terminal(ETerminalType.Number));
            RPNInput.Add(new Terminal(ETerminalType.RightBracket));
            RPNInput.Add(new Terminal(ETerminalType.RightParen));
            RPNInput.Add(new Terminal(ETerminalType.LeftBrace));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            RPNInput.Add(new Terminal(ETerminalType.RightBrace));
            */
            var RPNOutput = RPNTranslator.Translate(RPNInput);
            for (int i = 0; i < RPNOutput.Count; i++)
            {
                Console.WriteLine(i.ToString() + " " + RPNOutput[i].RPNType.ToString());
            }
            for (int i = 0; i < RPNTranslator.ConstMarks.Count; i++)
            {
                Console.WriteLine(RPNTranslator.ConstMarks[i].Position.ToString());
            }
            
            /*
            int[] A = { 5, 4, 3, 2, 1, };
            int temp;
            int i = 0;
            while (i<4)
            {
                if (A[i] > A[i + 1])
               {
                  temp = A[i];
                  A[i] = A[i+1];
                  A[i+1] = temp;
                  if (i > 0)
                  {
                        i = i - 1;
                  }
               }
               else
               {
                  i = i + 1;
               }
            }
            for (int j = 0; j < 5; j++)
            {
                Console.WriteLine(A[j]);
            }
            */
            #endregion
            /*
            var code = FileReader.Read("data.txt");
            var lexicalAnalizatorResult = LexicalAnalyzer.IsLexicalCorrect(code);
            List<Terminal> terminals = [];
            if (lexicalAnalizatorResult.IsCorrect)
            {
                terminals = lexicalAnalizatorResult.Terminals;
            }
            
            List<RPNSymbol> rpn = [];
            rpn = RPNTranslator.Translate(RPNInput);
            for (int i = 0; i < rpn.Count; i++)
            {
                Console.WriteLine(rpn[i].RPNType.ToString());
            }
            
            */
        }
    }
}