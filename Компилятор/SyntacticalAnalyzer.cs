namespace Компилятор
{
    public static class SyntacticalAnalyzer
    {
        /// <summary>
        /// Нахождение индекса парной закрывающейся скобки
        /// </summary>
        private static int FindPairedClosingBracket(int leftParenIndex, List<Terminal> terminals)
        {
            var openingBracket = terminals[leftParenIndex].TerminalType;
            var closingBracket = openingBracket switch
            {
                ETerminalType.LeftParen => ETerminalType.RightParen,
                ETerminalType.LeftBrace => ETerminalType.RightBrace,
                ETerminalType.LeftBracket => ETerminalType.RightBracket,
                _ => throw new ArgumentException(),
            };
            int counter = 0;
            for (int i = leftParenIndex; i < terminals.Count; i++)
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
            return -1;
        }

        /// <summary>
        /// 1. Блок инструкций
        /// </summary>
        public static bool ParseInstructionBlock(List<Terminal> terminals)
        {

            // 1.1 while ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция>
            // если начинается с while
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.While)
            {
                // предполагаемый индекс (
                int leftParenIndex = 1;
                // если по индексу действительно (
                if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                {
                    // находим индекс парной )
                    int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                    // если парная ) успешно нашлась
                    if (rightParenIndex != -1)
                    {
                        // предполагаемый индекс {
                        int leftBraceIndex = rightParenIndex + 1;
                        // если по индексу действительно {
                        if (terminals.ElementAtOrDefault(leftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                        {
                            // находим индекс парной }
                            int rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);
                            // если парная } успешно нашлась
                            if (rightBraceIndex != -1)
                            {
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
                }
            }

            // 1.2 if ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция>
            // если начинается с if
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.If)
            {
                // предполагаемый индекс (
                int leftParenIndex = 1;
                // если по индексу действительно (
                if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                {
                    // находим индекс парной )
                    int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                    // если парная ) успешно нашлась
                    if (rightParenIndex != -1)
                    {
                        // предполагаемый индекс {
                        int leftBraceIndex = rightParenIndex + 1;
                        // если по индексу действительно {
                        if (terminals.ElementAtOrDefault(leftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                        {
                            // находим индекс парной }
                            int rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);
                            // если парная } успешно нашлась
                            if (rightBraceIndex != -1)
                            {
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
                }
            }

            // 1.3 if ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция> else { <Блок инструкций> } <Последующая инструкция>
            // если начинается с if
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.If)
            {
                // предполагаемый индекс (
                int leftParenIndex = 1;
                // если по индексу действительно (
                if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                {
                    // находим индекс парной )
                    int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                    // если парная ) успешно нашлась
                    if (rightParenIndex != -1)
                    {
                        // предполагаемый индекс {
                        int firstLeftBraceIndex = rightParenIndex + 1;
                        // если по индексу действительно {
                        if (terminals.ElementAtOrDefault(firstLeftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                        {
                            // находим индекс парной }
                            int firstRightBraceIndex = FindPairedClosingBracket(firstLeftBraceIndex, terminals);
                            // если парная } успешно нашлась
                            if (firstRightBraceIndex != -1)
                            {
                                // предполагаемый индекс else
                                int elseIndex = firstRightBraceIndex + 1;

                                // если по индексу действительно else
                                if (terminals.ElementAtOrDefault(elseIndex)?.TerminalType == ETerminalType.Else)
                                {
                                    // предполагаемый индекс {
                                    int secondLeftBraceIndex = elseIndex + 1;

                                    // если по индексу действительно {
                                    if (terminals.ElementAtOrDefault(secondLeftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                                    {
                                        // находим индекс парной }
                                        int secondRightBraceIndex = FindPairedClosingBracket(secondLeftBraceIndex, terminals);
                                        // если парная } успешно нашлась
                                        if (secondRightBraceIndex != -1)
                                        {
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
                    }
                }
            }

            // 1.4 Input(<Идентификатор>) ; <Последующая инструкция>
            // если начинается с Input
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Input)
            {
                // предполагаемый индекс (
                int leftParenIndex = 1;

                // если по индексу действительно (
                if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                {
                    // находим индекс парной )
                    int rightParenIndex =  + FindPairedClosingBracket(leftParenIndex, terminals);
                    // если парная ) успешно нашлась
                    if (rightParenIndex != -1)
                    {
                        // предполагаемый индекс ;
                        int semicolonIndex = rightParenIndex + 1;

                        // если по индексу действительно ;
                        if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.Semicolon)
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
            }

            // 1.5 Output(<Идентификатор>) ; <Последующая инструкция>
            // если начинается с Input
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Output)
            {
                // предполагаемый индекс (
                int leftParenIndex = 1;

                // если по индексу действительно (
                if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                {
                    // находим индекс парной )
                    int rightParenIndex = + FindPairedClosingBracket(leftParenIndex, terminals);
                    // если парная ) успешно нашлась
                    if (rightParenIndex != -1)
                    {
                        // предполагаемый индекс ;
                        int semicolonIndex = rightParenIndex + 1;

                        // если по индексу действительно ;
                        if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.Semicolon)
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
            }

            // находим индекс первой точки с запятой
            int firstSemicolon = terminals.FindIndex(t => t.TerminalType == ETerminalType.Semicolon);

            // 1.6 <Инициализация переменной> ; <Последующая инструкция>
            // если в последовательности есть ;
            if (firstSemicolon != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForVariableInitialization = terminals[..firstSemicolon];
                var partForFollowingInstruction = terminals[(firstSemicolon + 1)..];
                // проверяем подпоследовательности
                if (ParseVariableInitialization(partForVariableInitialization) &&
                    ParseFollowingInstruction(partForFollowingInstruction))
                {
                    return true;
                }
            }

            // 1.7 <Присваивание> ; <Последующая инструкция>
            // если в последовательности есть ;
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

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 2. Последующая инструкция
        /// </summary>
        private static bool ParseFollowingInstruction(List<Terminal> terminals)
        {
            // если есть хотя бы один терминал
            if (terminals.Count != 0)
            {
                // 2.1 while ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция>
                // если начинается с while
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.While)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;
                    // если по индексу действительно (
                    if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                        // если парная ) успешно нашлась
                        if (rightParenIndex != -1)
                        {
                            // предполагаемый индекс {
                            int leftBraceIndex = rightParenIndex + 1;
                            // если по индексу действительно {
                            if (terminals.ElementAtOrDefault(leftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                            {
                                // находим индекс парной }
                                int rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);
                                // если парная } успешно нашлась
                                if (rightBraceIndex != -1)
                                {
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
                    }
                }

                // 2.2 if ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция>
                // если начинается с if
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.If)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;
                    // если по индексу действительно (
                    if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                        // если парная ) успешно нашлась
                        if (rightParenIndex != -1)
                        {
                            // предполагаемый индекс {
                            int leftBraceIndex = rightParenIndex + 1;
                            // если по индексу действительно {
                            if (terminals.ElementAtOrDefault(leftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                            {
                                // находим индекс парной }
                                int rightBraceIndex = FindPairedClosingBracket(leftBraceIndex, terminals);
                                // если парная } успешно нашлась
                                if (rightBraceIndex != -1)
                                {
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
                    }
                }

                // 2.3 if ( <Логическое или> ) { <Блок инструкций> } <Последующая инструкция> else { <Блок инструкций> } <Последующая инструкция>
                // если начинается с if
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.If)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;
                    // если по индексу действительно (
                    if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                        // если парная ) успешно нашлась
                        if (rightParenIndex != -1)
                        {
                            // предполагаемый индекс {
                            int firstLeftBraceIndex = rightParenIndex + 1;
                            // если по индексу действительно {
                            if (terminals.ElementAtOrDefault(firstLeftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                            {
                                // находим индекс парной }
                                int firstRightBraceIndex = FindPairedClosingBracket(firstLeftBraceIndex, terminals);
                                // если парная } успешно нашлась
                                if (firstRightBraceIndex != -1)
                                {
                                    // предполагаемый индекс else
                                    int elseIndex = firstRightBraceIndex + 1;

                                    // если по индексу действительно else
                                    if (terminals.ElementAtOrDefault(elseIndex)?.TerminalType == ETerminalType.Else)
                                    {
                                        // предполагаемый индекс {
                                        int secondLeftBraceIndex = elseIndex + 1;

                                        // если по индексу действительно {
                                        if (terminals.ElementAtOrDefault(secondLeftBraceIndex)?.TerminalType == ETerminalType.LeftBrace)
                                        {
                                            // находим индекс парной }
                                            int secondRightBraceIndex = FindPairedClosingBracket(secondLeftBraceIndex, terminals);
                                            // если парная } успешно нашлась
                                            if (secondRightBraceIndex != -1)
                                            {
                                                // выделяем подпоследовательности для парсинга
                                                var partForLogicalOR = terminals[(leftParenIndex + 1)..rightParenIndex];
                                                var partForFirstInstructionBlock = terminals[(firstLeftBraceIndex + 1)..firstRightBraceIndex];
                                                var partForSecondInstructionBlock = terminals[(secondLeftBraceIndex + 1)..secondRightBraceIndex];
                                                var partForFollowingInstruction = terminals[(secondRightBraceIndex + 1)..];
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
                        }
                    }
                }

                // 2.4 Input(<Идентификатор>) ; <Последующая инструкция>
                // если начинается с Input
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Input)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;

                    // если по индексу действительно (
                    if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                        // если парная ) успешно нашлась
                        if (rightParenIndex != -1)
                        {
                            // предполагаемый индекс ;
                            int semicolonIndex = rightParenIndex + 1;

                            // если по индексу действительно ;
                            if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.Semicolon)
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
                }

                // 2.5 Output(<Идентификатор>) ; <Последующая инструкция>
                // если начинается с Input
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Output)
                {
                    // предполагаемый индекс (
                    int leftParenIndex = 1;

                    // если по индексу действительно (
                    if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.LeftParen)
                    {
                        // находим индекс парной )
                        int rightParenIndex = FindPairedClosingBracket(leftParenIndex, terminals);
                        // если парная ) успешно нашлась
                        if (rightParenIndex != -1)
                        {
                            // предполагаемый индекс ;
                            int semicolonIndex = rightParenIndex + 1;

                            // если по индексу действительно ;
                            if (terminals.ElementAtOrDefault(leftParenIndex)?.TerminalType == ETerminalType.Semicolon)
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
                }

                // находим индекс первой точки с запятой
                int firstSemicolon = terminals.FindIndex(t => t.TerminalType == ETerminalType.Semicolon);

                // 2.6 <Инициализация переменной> ; <Последующая инструкция>
                // если в последовательности есть ;
                if (firstSemicolon != -1)
                {
                    // выделяем подпоследовательности для парсинга
                    var partForVariableInitialization = terminals[..firstSemicolon];
                    var partForFollowingInstruction = terminals[(firstSemicolon + 1)..];
                    // проверяем подпоследовательности
                    if (ParseVariableInitialization(partForVariableInitialization) &&
                        ParseFollowingInstruction(partForFollowingInstruction))
                    {
                        return true;
                    }
                }

                // 2.7 <Присваивание> ; <Последующая инструкция>
                // если в последовательности есть ;
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

                // последовательность не подпадает ни под один из шаблонов
                return false;
            }
            else
            {
                // последовательность - лямбда
                return true;
            }
        }



        /// <summary>
        /// 3. Инициализация переменной
        /// </summary>
        private static bool ParseVariableInitialization(List<Terminal> terminals)
        {
            
            //если в последовательности ровно 5 треминалов
            if (terminals.Count == 5)
            {
                // 3.1 int[ЧИСЛО] НАЗВАНИЕ ПЕРЕМЕННОЙ
                // если начинается с int
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Int)
                {
                    // если следующий терминал [
                    if (terminals.ElementAtOrDefault(1)?.TerminalType == ETerminalType.LeftBracket)
                    {
                        // если следующий терминал ЧИСЛО
                        if (terminals.ElementAtOrDefault(2)?.TerminalType == ETerminalType.Number)
                        {
                            // если следующий терминал ]
                            if (terminals.ElementAtOrDefault(3)?.TerminalType == ETerminalType.Number)
                            {
                                // если следующий терминал идентификатор
                                if (terminals.ElementAtOrDefault(4)?.TerminalType == ETerminalType.Number)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                // 3.2 bool[ЧИСЛО] НАЗВАНИЕ ПЕРЕМЕННОЙ
                // если начинается с bool
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Bool)
                {
                    // если следующий терминал [
                    if (terminals.ElementAtOrDefault(1)?.TerminalType == ETerminalType.LeftBracket)
                    {
                        // если следующий терминал ЧИСЛО
                        if (terminals.ElementAtOrDefault(2)?.TerminalType == ETerminalType.Number)
                        {
                            // если следующий терминал ]
                            if (terminals.ElementAtOrDefault(3)?.TerminalType == ETerminalType.Number)
                            {
                                // если следующий терминал идентификатор
                                if (terminals.ElementAtOrDefault(4)?.TerminalType == ETerminalType.Number)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                // 3.3 string[ЧИСЛО] НАЗВАНИЕ ПЕРЕМЕННОЙ
                // если начинается с string
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.String)
                {
                    // если следующий терминал [
                    if (terminals.ElementAtOrDefault(1)?.TerminalType == ETerminalType.LeftBracket)
                    {
                        // если следующий терминал ЧИСЛО
                        if (terminals.ElementAtOrDefault(2)?.TerminalType == ETerminalType.Number)
                        {
                            // если следующий терминал ]
                            if (terminals.ElementAtOrDefault(3)?.TerminalType == ETerminalType.Number)
                            {
                                // если следующий терминал идентификатор
                                if (terminals.ElementAtOrDefault(4)?.TerminalType == ETerminalType.Number)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            //если в последовательности ровно 2 терминала
            if (terminals.Count == 2)
            {
                // 3.4 int НАЗВАНИЕ ПЕРЕМЕННОЙ
                // если начинается с int
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Int)
                {
                    // если следующий терминал идентификатор
                    if (terminals.ElementAtOrDefault(1)?.TerminalType == ETerminalType.VariableName)
                    {
                        return true;
                    }
                }

                // 3.5 bool НАЗВАНИЕ ПЕРЕМЕННОЙ
                // если начинается с bool
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Bool)
                {
                    // если следующий терминал идентификатор
                    if (terminals.ElementAtOrDefault(1)?.TerminalType == ETerminalType.VariableName)
                    {
                        return true;
                    }
                }

                // 3.6 string НАЗВАНИЕ ПЕРЕМЕННОЙ
                // если начинается с string
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.String)
                {
                    // если следующий терминал идентификатор
                    if (terminals.ElementAtOrDefault(1)?.TerminalType == ETerminalType.VariableName)
                    {
                        return true;
                    }
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 4. Присваивание
        /// </summary>
        private static bool ParseAssignment(List<Terminal> terminals)
        {
            // 4.1 <Идентификатор> = <Аргумент присваивания>
            // находим индекс первого =
            int firstAssignment = terminals.FindIndex(t => t.TerminalType == ETerminalType.Assignment);
            
            // если = нашелся
            if (firstAssignment != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForIdentifier = terminals[..firstAssignment];
                var partForAssignmentArgument = terminals[(firstAssignment + 1)..];
                // проверяем подпоследовательности
                if (ParseIdentifier(partForIdentifier) &&
                    ParseAssignmentArgument(partForAssignmentArgument))
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 5. Аргумент присваивания
        /// </summary>
        private static bool ParseAssignmentArgument(List<Terminal> terminals)
        {
            // 5.1 <Логическое ИЛИ>
            // если посдевовательность удовлетворяет шаблону <Логическое ИЛИ>
            if (ParseLogicalOR(terminals))
            {
                return true;
            }

            // 5.2 <Конкатенация>
            // если посдевовательность удовлетворяет шаблону <Конкатенация>
            if (ParseConcatenation(terminals))
            {
                return true;
            }

            // 5.3 <Сложение и вычитание>
            // если посдевовательность удовлетворяет шаблону <Сложение и вычитание>
            if (ParseAdditionAndSubtraction(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 6. Логическое ИЛИ
        /// </summary>
        private static bool ParseLogicalOR(List<Terminal> terminals)
        {
            // 6.1 <Логическое И> || <Логическое ИЛИ>
            // находим индекс первого ||
            int firstLogicalOR = terminals.FindIndex(t => t.TerminalType == ETerminalType.Or);
            
            // если || нашелся
            if (firstLogicalOR != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForLogicalAND = terminals[..firstLogicalOR];
                var partForLogicalOR = terminals[(firstLogicalOR + 1)..];
                // проверяем подпоследовательности
                if (ParseLogicalAND(partForLogicalAND) &&
                    ParseLogicalOR(partForLogicalOR))
                {
                    return true;
                }
            }

            // 6.2 <Логическое И>
            // если посдевовательность удовлетворяет шаблону <Логическое И>
            if (ParseLogicalAND(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 7. Логическое И
        /// </summary>
        private static bool ParseLogicalAND(List<Terminal> terminals)
        {
            // 7.1 <Аргумент логического И> && <Логическое И>
            // находим индекс первого &&
            int firstLogicalAND = terminals.FindIndex(t => t.TerminalType == ETerminalType.And);
            // если && нашелся
            if (firstLogicalAND != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForLogicalANDArgument = terminals[..firstLogicalAND];
                var partForLogicalOR = terminals[(firstLogicalAND + 1)..];
                // проверяем подпоследовательности
                if (ParseLogicalANDArgument(partForLogicalANDArgument) &&
                    ParseLogicalOR(partForLogicalOR))
                {
                    return true;
                }
            }

            // 7.2 <Аргумент логического И>
            // если посдевовательность удовлетворяет шаблону <Аргумент логического И>
            if (ParseLogicalANDArgument(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 8. Аргумент логического И
        /// </summary>
        private static bool ParseLogicalANDArgument(List<Terminal> terminals)
        {
            // 8.1 <Отрицание>
            // если посдевовательность удовлетворяет шаблону <Отрицание>
            if (ParseNegation(terminals))
            {
                return true;
            }

            // 8.2 <Строковое сравнение>
            // если посдевовательность удовлетворяет шаблону <Строковое сравнение>
            if (ParseStringComparison(terminals))
            {
                return true;
            }

            // 8.3 <Числовое сравнение>
            // если посдевовательность удовлетворяет шаблону <Числовое сравнение>
            if (ParseNumericalComparison(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 9. Отрицание
        /// </summary>
        private static bool ParseNegation(List<Terminal> terminals)
        {
            // 9.1 !<Аргумент отрицания>
            // если первый терминал !
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Not)
            {
                // выделяем подпоследовательность для парсинга
                var partForNegationArgument = terminals[1..];
                // проверяем подпоследовательности
                if (ParseNegationArgument(partForNegationArgument))
                {
                    return true;
                }
            }

            // 9.2 <Аргумент отрицания>
            // если посдевовательность удовлетворяет шаблону <Аргумент отрицания>
            if (ParseNegationArgument(terminals))
            {
                return true;
            }
            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 10. Аргумент отрицания
        /// </summary>
        private static bool ParseNegationArgument(List<Terminal> terminals)
        {
            // 10.1 (<Логическое или>)
            // если первый терминал (
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.LeftParen)
            {
                // находим индекс парной )
                int rightParenIndex = FindPairedClosingBracket(0, terminals);
                // если парная ) успешно нашлась
                if (rightParenIndex != -1)
                {
                    // если парная ) это последний терминал
                    if (rightParenIndex == terminals.Count - 1)
                    {
                        // выделяем подпоследовательность для парсинга
                        var partForLogicalOr = terminals[1..rightParenIndex];
                        // проверяем подпоследовательности
                        if (ParseLogicalOR(partForLogicalOr))
                        {
                            return true;
                        }
                    }
                }
            }

            // 10.2 <Идентификатор>
            // если посдевовательность удовлетворяет шаблону <Идентификатор>
            if (ParseIdentifier(terminals))
            {
                return true;
            }

            // 10.2 БУЛЕАН
            // если в последователельности ровно один терминал
            if (terminals.Count == 1)
            {
                // если первый терминал булеан
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Boolean)
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 11. Строковое сравнение
        /// </summary>
        private static bool ParseStringComparison(List<Terminal> terminals)
        {
            // 11.1 <Конкатенация> == <Конкатенация>
            // находим индекс первого ==
            int firstEqual = terminals.FindIndex(t => t.TerminalType == ETerminalType.Equal);
            // если == нашелся
            if (firstEqual != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForLeftConcatenation = terminals[..firstEqual];
                var partForRightConcatenation = terminals[(firstEqual + 1)..];
                // проверяем подпоследовательности
                if (ParseConcatenation(partForLeftConcatenation) &&
                    ParseConcatenation(partForRightConcatenation))
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 12. Конкатенация
        /// </summary>
        private static bool ParseConcatenation(List<Terminal> terminals)
        {
            // 12.1 <Аргумент конкатенации> + <Конкатенация>
            // находим индекс первого +
            int firstPlus = terminals.FindIndex(t => t.TerminalType == ETerminalType.Plus);
            // если + нашелся
            if (firstPlus != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForConcatenationArgument = terminals[..firstPlus];
                var partForConcatenation = terminals[(firstPlus + 1)..];
                // проверяем подпоследовательности
                if (ParseConcatenationArgument(partForConcatenationArgument) &&
                    ParseConcatenation(partForConcatenation))
                {
                    return true;
                }
            }

            // 12.2 <Аргумент конкатенации>
            // если посдевовательность удовлетворяет шаблону <Аргумент конкатенации>
            if (ParseConcatenationArgument(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 13. Аргумент конкатенации
        /// </summary>
        private static bool ParseConcatenationArgument(List<Terminal> terminals)
        {
            // 13.1 <Идентификатор>
            // если посдевовательность удовлетворяет шаблону <Идентификатор>
            if (ParseIdentifier(terminals))
            {
                return true;
            }

            // 13.2 СТРОКА
            // если в последователельности ровно один терминал
            if (terminals.Count == 1)
            {
                // если первый терминал булеан
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.TextLine)
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 14. Числовое сравнение
        /// </summary>
        private static bool ParseNumericalComparison(List<Terminal> terminals)
        {
            // 14.1 <Сложение и вычитание> <Оператор сравнения> <Сложение и вычитание>
            // берём все операторы сравнения
            ETerminalType[] comparsions =
                [ETerminalType.Greater,
                ETerminalType.Less,
                ETerminalType.Equal,
                ETerminalType.GreaterEqual,
                ETerminalType.LessEqual];
            // для каждого оператора сравнения
            foreach (var comparsionType in comparsions)
            {
                // находим индекс первого вхождения этого опрератора в последовательность
                int firstcomparsion = terminals.FindIndex(t => t.TerminalType == comparsionType);
                // если опрератор нашелся
                if (firstcomparsion != -1)
                {
                    // выделяем подпоследовательности для парсинга
                    var partForLeftAdditionAndSubtraction = terminals[..firstcomparsion];
                    var partForRightAdditionAndSubtraction = terminals[(firstcomparsion + 1)..];
                    // проверяем подпоследовательности
                    if (ParseAdditionAndSubtraction(partForLeftAdditionAndSubtraction) &&
                        ParseAdditionAndSubtraction(partForRightAdditionAndSubtraction))
                    {
                        return true;
                    }
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
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
            // 16.1 <Умножение и деление> + <Сложение и вычитание>
            // находим индекс первого +
            int firstPlus = terminals.FindIndex(t => t.TerminalType == ETerminalType.Plus);
            // если + нашелся
            if (firstPlus != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForMultiplicationAndDivision = terminals[..firstPlus];
                var partForAdditionAndSubtraction = terminals[(firstPlus + 1)..];
                // проверяем подпоследовательности
                if (ParseMultiplicationAndDivision(partForMultiplicationAndDivision) &&
                    ParseAdditionAndSubtraction(partForAdditionAndSubtraction))
                {
                    return true;
                }
            }

            // 16.2 <Умножение и деление> - <Сложение и вычитание>
            // находим индекс первого -
            int firstMinus = terminals.FindIndex(t => t.TerminalType == ETerminalType.Minus);
            // если - нашелся
            if (firstMinus != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForMultiplicationAndDivision = terminals[..firstMinus];
                var partForAdditionAndSubtraction = terminals[(firstMinus + 1)..];
                // проверяем подпоследовательности
                if (ParseMultiplicationAndDivision(partForMultiplicationAndDivision) &&
                    ParseAdditionAndSubtraction(partForAdditionAndSubtraction))
                {
                    return true;
                }
            }

            // 16.3 <Умножение и деление>
            // если посдевовательность удовлетворяет шаблону <Умножение и деление>
            if (ParseMultiplicationAndDivision(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 17. Умножение и деление
        /// </summary>
        private static bool ParseMultiplicationAndDivision(List<Terminal> terminals)
        {
            // 17.1 <Унарный минус> * <Умножение и деление>
            // находим индекс первого *
            int firstMultiply = terminals.FindIndex(t => t.TerminalType == ETerminalType.Multiply);
            // если * нашелся
            if (firstMultiply != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForUnaryMinus = terminals[..firstMultiply];
                var partForMultiplicationAndDivision = terminals[(firstMultiply + 1)..];
                // проверяем подпоследовательности
                if (ParseUnaryMinus(partForUnaryMinus) &&
                    ParseMultiplicationAndDivision(partForMultiplicationAndDivision))
                {
                    return true;
                }
            }

            // 17.2 <Унарный минус> / <Умножение и деление> 
            // находим индекс первого /
            int firstDivide = terminals.FindIndex(t => t.TerminalType == ETerminalType.Divide);
            // если / нашелся
            if (firstDivide != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForUnaryMinus = terminals[..firstDivide];
                var partForMultiplicationAndDivision = terminals[(firstDivide + 1)..];
                // проверяем подпоследовательности
                if (ParseUnaryMinus(partForUnaryMinus) &&
                    ParseMultiplicationAndDivision(partForMultiplicationAndDivision))
                {
                    return true;
                }
            }

            // 17.3 <Унарный минус> % <Умножение и деление>
            // находим индекс первого %
            int firstModulus = terminals.FindIndex(t => t.TerminalType == ETerminalType.Modulus);
            // если % нашелся
            if (firstModulus != -1)
            {
                // выделяем подпоследовательности для парсинга
                var partForUnaryMinus = terminals[..firstModulus];
                var partForMultiplicationAndDivision = terminals[(firstModulus + 1)..];
                // проверяем подпоследовательности
                if (ParseUnaryMinus(partForUnaryMinus) &&
                    ParseMultiplicationAndDivision(partForMultiplicationAndDivision))
                {
                    return true;
                }
            }

            // 16.3 <Унарный минус>
            // если посдевовательность удовлетворяет шаблону <Унарный минус>
            if (ParseUnaryMinus(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 18. Унарный минус
        /// </summary>
        private static bool ParseUnaryMinus(List<Terminal> terminals)
        {
            // 18.1 - <Аргумент унарного минуса>
            // если первый терминал -
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Minus)
            {
                // выделяем подпоследовательность для парсинга
                var partForUnaryMinusArgument = terminals[1..];
                // проверяем подпоследовательность
                if (ParseUnaryMinusArgument(partForUnaryMinusArgument))
                {
                    return true;
                }
            }

            // 18.2 <Аргумент унарного минуса>
            // если посдевовательность удовлетворяет шаблону <Аргумент унарного минуса>
            if (ParseUnaryMinusArgument(terminals))
            {
                return true;
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 19. Аргумент унарного минуса
        /// </summary>
        private static bool ParseUnaryMinusArgument(List<Terminal> terminals)
        {
            // 19.1 (<Сложение и вычитание>)
            // если первый терминал (
            
            
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.LeftParen)
            {
                // находим индекс парной )
                int rightParenIndex = FindPairedClosingBracket(0, terminals);
                // если парная ) успешно нашлась
                if (rightParenIndex != -1)
                {
                    // если парная ) это последний терминал
                    if (rightParenIndex == terminals.Count - 1)
                    {
                        // выделяем подпоследовательность для парсинга
                        var partForAdditionAndSubtraction = terminals[1..rightParenIndex];
                        // проверяем подпоследовательности
                        if (ParseAdditionAndSubtraction(partForAdditionAndSubtraction))
                        {
                            return true;
                        }
                    }
                }
            }

            // 19.2 <Идентификатор>
            // если посдевовательность удовлетворяет шаблону <Идентификатор>
            if (ParseIdentifier(terminals))
            {
                return true;
            }

            // 10.2 ЧИСЛО
            // если в последователельности ровно один терминал
            if (terminals.Count == 1)
            {
                // если первый терминал число
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Number)
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 20. Идентификатор
        /// </summary>
        private static bool ParseIdentifier(List<Terminal> terminals)
        {
            // 20.1 НАЗВАНИЕ ПЕРЕМЕННОЙ[<Индексатор>]
            // если первый терминал имяПеременной
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.VariableName)
            {
                // предполагаемый индекс [
                int leftBracketIndex = 1;
                // если по индексу действительно [
                if (terminals.ElementAtOrDefault(leftBracketIndex)?.TerminalType == ETerminalType.LeftBracket)
                {
                    // находим индекс парной ]
                    int rightBracketIndex = FindPairedClosingBracket(leftBracketIndex, terminals);
                    // если ] была успешно найдена
                    if (rightBracketIndex != -1)
                    {
                        // если парная ] это последний терминал
                        if (rightBracketIndex == terminals.Count - 1)
                        {
                            // выделяем подпоследовательность для парсинга
                            var partForIndexer = terminals[(leftBracketIndex + 1)..rightBracketIndex];
                            // проверяем подпоследовательности
                            if (ParseIndexer(partForIndexer))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            // 20.2 НАЗВАНИЕ ПЕРЕМЕННОЙ
            // если в последователельности ровно один терминал
            if (terminals.Count == 1)
            {
                // если первый название переменной
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.VariableName)
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }



        /// <summary>
        /// 21. Индексатор
        /// </summary>
        private static bool ParseIndexer(List<Terminal> terminals)
        {
            // 21.1 НАЗВАНИЕ ПЕРЕМЕННОЙ[<Индексатор>]
            // если первый терминал имяПеременной
            if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.VariableName)
            {
                // предполагаемый индекс [
                int leftBracketIndex = 1;
                // если по индексу действительно [
                if (terminals.ElementAtOrDefault(leftBracketIndex)?.TerminalType == ETerminalType.LeftBracket)
                {
                    // находим индекс парной ]
                    int rightBracketIndex = FindPairedClosingBracket(leftBracketIndex, terminals);
                    // если ] была успешно найдена
                    if (rightBracketIndex != -1)
                    {
                        // если парная ] это последний терминал
                        if (rightBracketIndex == terminals.Count - 1)
                        {
                            // выделяем подпоследовательность для парсинга
                            var partForIndexer = terminals[(leftBracketIndex + 1)..rightBracketIndex];
                            // проверяем подпоследовательности
                            if (ParseIndexer(partForIndexer))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            // 21.2 НАЗВАНИЕ ПЕРЕМЕННОЙ
            // если в последователельности ровно один терминал
            if (terminals.Count == 1)
            {
                // если первый терминал число
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.VariableName)
                {
                    return true;
                }
            }

            // 21.3 ЧИСЛО
            // если в последователельности ровно один терминал
            if (terminals.Count == 1)
            {
                // если первый терминал число
                if (terminals.ElementAtOrDefault(0)?.TerminalType == ETerminalType.Number)
                {
                    return true;
                }
            }

            // последовательность не подпадает ни под один из шаблонов
            return false;
        }
    }
}