using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Компилятор
{
    /// <summary>
    /// Статический класс для считывания текста из файла
    /// </summary>
    public static class FileReader
    {
        /// <summary>
        /// Считывает текст из .txt файла с указанным именем в папке с программой
        /// </summary>
        public static string Read(string filePath)
        {
            string text;
            // Считываем текст из файла
            text = File.ReadAllText(filePath);
            
            // Удаляем все пробелы, переносы строк и символы табуляции
            text = RemoveWhitespace(text);
            return text;
        }

        static string RemoveWhitespace(string input)
        {
            // Используем метод Replace для удаления нежелательных символов
            return input.Replace(" ", "")
                        .Replace("\t", "")
                        .Replace("\n", "")
                        .Replace("\r", "");
        }
    }
}
