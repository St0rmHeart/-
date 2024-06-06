namespace Компилятор
{
    public class SyntacticalAnalyzer
    {
        private static int FindPairedClosingBracket(List<Terminal> terminals)
        {
            var openingBracket = terminals[0].TerminalType;
            var closingBracket = openingBracket switch
            {
                ETerminalType.LeftParen => ETerminalType.RightParen,
                ETerminalType.LeftBrace => ETerminalType.RightBrace,
                ETerminalType.LeftBracket => ETerminalType.RightBracket,
                _ => throw new ArgumentException(),
            };
            int counter = 0;
            for (int i = 0; i < terminals.Count; i++)
            {
                if (terminals[i].TerminalType == openingBracket)
                {
                    counter++;
                }
                if (terminals[i].TerminalType == closingBracket)
                {
                    counter--;
                    if (counter == 0)
                    {
                        return i;
                    }
                }
            }
            throw new ArgumentException("Не найдена закрывающая строка");
        }

        /// <summary>
        /// 1. Блок инструкций
        /// </summary>
        /*public static bool ParseInstructionBlock(List<Terminal> terminals)
        {
            if (terminals.Count != 0)
            {
                // 1.1 while ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция>
                // если начинается с while
                if (terminals[0].TerminalType == ETerminalType.While)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;
                    // если по индексу действительно (
                    if (terminals[leftParenIndex].TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = leftParenIndex + FindPairedClosingBracket(terminals[leftParenIndex..]);

                        // предполагаемый индекс {
                        int leftBraceIndex = rightParenIndex + 1;
                        // если по индексу действительно {
                        if (terminals[leftBraceIndex].TerminalType == ETerminalType.LeftBrace)
                        {
                            // находим индекс парной }
                            int rightBraceIndex = leftBraceIndex + FindPairedClosingBracket(terminals[leftBraceIndex..]);
                            // выделяем подпоследовательности для парсинга
                            var partForLogicalOR = terminals[(leftParenIndex + 1)..rightParenIndex];
                            var partForInstructionBlock = terminals[(leftBraceIndex + 1)..rightBraceIndex];
                            var partForFollowingInstruction = terminals[(rightBraceIndex + 1)..];
                            // Если подподпоследоватльности терминалов прошли парсинг
                            if (ParseLogicalOR(partForLogicalOR) &&
                                ParseInstructionBlock(partForInstructionBlock) &&
                                ParseFollowingInstruction(partForFollowingInstruction))
                            {
                                return true;
                            }
                        }
                    }
                }

                // 1.2 if ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция>
                // если начинается с if
                if (terminals[0].TerminalType == ETerminalType.If)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;

                    // если по индексу действительно (
                    if (terminals[leftParenIndex].TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = leftParenIndex + FindPairedClosingBracket(terminals[leftParenIndex..]);

                        // предполагаемый индекс {
                        int leftBraceIndex = rightParenIndex + 1;

                        // если по индексу действительно {
                        if (terminals[leftBraceIndex].TerminalType == ETerminalType.LeftBrace)
                        {
                            // находим индекс парной }
                            int rightBraceIndex = leftBraceIndex + FindPairedClosingBracket(terminals[leftBraceIndex..]);
                            // выделяем подпоследовательности для парсинга
                            var partForLogicalOR = terminals[(leftParenIndex + 1)..rightParenIndex];
                            var partForInstructionBlock = terminals[(leftBraceIndex + 1)..rightBraceIndex];
                            var partForFollowingInstruction = terminals[(rightBraceIndex + 1)..];
                            // Если подподпоследоватльности терминалов прошли парсинг
                            if (ParseLogicalOR(partForLogicalOR) &&
                                ParseInstructionBlock(partForInstructionBlock) &&
                                ParseFollowingInstruction(partForFollowingInstruction))
                            {
                                return true;
                            }
                        }
                    }
                }

                // 1.3 if ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция> else { <Блок инструкций> } <Последующая инструкция>
                // если начинается с if
                if (terminals[0].TerminalType == ETerminalType.If)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;

                    // если по индексу действительно (
                    if (terminals[leftParenIndex].TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = leftParenIndex + FindPairedClosingBracket(terminals[leftParenIndex..]);

                        // предполагаемый индекс {
                        int firstLeftBraceIndex = rightParenIndex + 1;

                        // если по индексу действительно {
                        if (terminals[firstLeftBraceIndex].TerminalType == ETerminalType.LeftBrace)
                        {
                            // находим индекс парной }
                            int firstRightBraceIndex = firstLeftBraceIndex + FindPairedClosingBracket(terminals[firstLeftBraceIndex..]);

                            // предполагаемый индекс else
                            int elseIndex = firstRightBraceIndex + 1;

                            // если по индексу действительно else
                            if (terminals[elseIndex].TerminalType == ETerminalType.Else)
                            {
                                // предполагаемый индекс {
                                int secondLeftBraceIndex = elseIndex + 1;

                                // если по индексу действительно {
                                if (terminals[secondLeftBraceIndex].TerminalType == ETerminalType.LeftBrace)
                                {
                                    // находим индекс парной }
                                    int secondRightBraceIndex = secondLeftBraceIndex + FindPairedClosingBracket(terminals[secondLeftBraceIndex..]);
                                    
                                    // выделяем подпоследовательности для парсинга
                                    var partForLogicalOR = terminals[(leftParenIndex + 1)..rightParenIndex];
                                    var partForFirstInstructionBlock = terminals[(firstLeftBraceIndex + 1)..firstRightBraceIndex];
                                    var partForSecondInstructionBlock = terminals[(secondLeftBraceIndex + 1)..secondRightBraceIndex];
                                    var partForFollowingInstruction = terminals[(firstRightBraceIndex + 1)..];
                                    // Если подподпоследоватльности терминалов прошли парсинг
                                    if (ParseLogicalOR(partForLogicalOR) &&
                                        ParseInstructionBlock(partForFirstInstructionBlock) &&
                                        ParseInstructionBlock(partForSecondInstructionBlock) &&
                                        ParseFollowingInstruction(partForFollowingInstruction))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

                // 1.4 Input(<Идентификатор>) ; <Последующая инструкция>
                // если начинается с Input
                if (terminals[0].TerminalType == ETerminalType.Input)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;

                    // если по индексу действительно (
                    if (terminals[leftParenIndex].TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = leftParenIndex + FindPairedClosingBracket(terminals[leftParenIndex..]);

                        // предполагаемый индекс ;
                        int semicolonIndex = rightParenIndex + 1;

                        // если по индексу действительно ;
                        if (terminals[leftParenIndex].TerminalType == ETerminalType.Semicolon)
                        {
                            // выделяем подпоследовательности для парсинга
                            var partForIdentifier = terminals[(leftParenIndex + 1)..rightParenIndex];
                            var partForFollowingInstruction = terminals[(semicolonIndex + 1)..];
                            // Если подподпоследоватльности терминалов прошли парсинг
                            if (ParseIdentifier(partForIdentifier) &&
                                ParseFollowingInstruction(partForFollowingInstruction))
                            {
                                return true;
                            }
                        }
                    }
                }

                // 1.5 Output(<Идентификатор>) ; <Последующая инструкция>
                // если начинается с Input
                if (terminals[0].TerminalType == ETerminalType.Output)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;

                    // если по индексу действительно (
                    if (terminals[leftParenIndex].TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = leftParenIndex + FindPairedClosingBracket(terminals[leftParenIndex..]);

                        // предполагаемый индекс ;
                        int semicolonIndex = rightParenIndex + 1;

                        // если по индексу действительно ;
                        if (terminals[leftParenIndex].TerminalType == ETerminalType.Semicolon)
                        {
                            // выделяем подпоследовательности для парсинга
                            var partForIdentifier = terminals[(leftParenIndex + 1)..rightParenIndex];
                            var partForFollowingInstruction = terminals[(semicolonIndex + 1)..];
                            // Если подподпоследоватльности терминалов прошли парсинг
                            if (ParseIdentifier(partForIdentifier) &&
                                ParseFollowingInstruction(partForFollowingInstruction))
                            {
                                return true;
                            }
                        }
                    }
                }

                // находим индекс первой точки с запятой
                int firstSemicolon = terminals.FindIndex(t => t.TerminalType == ETerminalType.Semicolon);

                // 1.6 <Инициализация переменной> ; <Последующая инструкция>
                if (firstSemicolon != -1)
                {
                    // выделяем подпоследовательности для парсинга
                    var partForVariableInitialization = terminals[..firstSemicolon];
                    var partForFollowingInstruction = terminals[(firstSemicolon +1 )..];
                    // проверяем подпоследовательности
                    if (ParseVariableInitialization(partForVariableInitialization) &&
                        ParseFollowingInstruction(partForFollowingInstruction))
                    {
                        return true;
                    }
                }

                // 1.7 <Присваивание> ; <Последующая инструкция>
                if (firstSemicolon != -1)
                {
                    // выделяем подпоследовательности для парсинга
                    var partForAssignment = terminals[..firstSemicolon];
                    var partForFollowingInstruction = terminals[(firstSemicolon + 1)..];
                    // проверяем подпоследовательности
                    if (ParseAssignment(partForAssignment) &&
                        ParseFollowingInstruction(partForFollowingInstruction))
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }*/



        /// <summary>
        /// 2. Последующая инструкция
        /// </summary>
        private static bool ParseFollowingInstruction(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 3. Инициализация переменной
        /// </summary>
        private static bool ParseVariableInitialization(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 4. Присваивание
        /// </summary>
        private static bool ParseAssignment(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 5. Аргумент присваивания
        /// </summary>
        private static bool ParseAssignmentArgument(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 6. Логическое ИЛИ
        /// </summary>
        private static bool ParseLogicalOR(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 7. Логическое И
        /// </summary>
        private static bool ParseLogicalAND(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 8. Аргумент логического И
        /// </summary>
        private static bool ParseLogicalANDArgument(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 9. Отрицание
        /// </summary>
        private static bool ParseNegation(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 10. Аргумент отрицания
        /// </summary>
        private static bool ParseNegationArgument(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 11. Строковое сравнение
        /// </summary>
        private static bool ParseStringComparison(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 12. Конкатенация
        /// </summary>
        private static bool ParseConcatenation(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 13. Аргумент конкатенации
        /// </summary>
        private static bool ParseConcatenationArgument(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 14. Числовое сравнение
        /// </summary>
        private static bool ParseNumericalComparison(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 15. Оператор сравнения
        /// </summary>
        private static bool ParseComparisonOperator(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 16. Сложение и вычитание
        /// </summary>
        private static bool ParseAdditionAndSubtraction(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 17. Умножение и деление
        /// </summary>
        private static bool ParseMultiplicationAndDivision(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 18. Унарный минус
        /// </summary>
        private static bool ParseUnaryMinus(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 19. Аргумент унарного минуса
        /// </summary>
        private static bool ParseUnaryMinusArgument(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 20. Идентификатор
        /// </summary>
        private static bool ParseIdentifier(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 21. Индексатор
        /// </summary>
        private static bool ParseIndexer(List<Terminal> terminals)
        {
            throw new NotImplementedException();
        }
    }
}
