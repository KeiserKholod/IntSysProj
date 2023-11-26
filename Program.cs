using System;
using System.IO;
using System.Text;

namespace ProcessingTextFile
{
    internal class Program
    {
        private const string FILE_FOR_ANALYSIS_PATH = "ЛегкийТест.txt";
        private const string CONDITIONS_FILE_PATH = "ВВОДИМЫЕ УСЛОВИЯ ДЛЯ ДВУХМЕРНОГО МАССИВА.txt";

        private static string[,] conditions = null;

        static void Main()
        {
            using (StreamReader streamReader = new StreamReader(CONDITIONS_FILE_PATH, Encoding.UTF8))
            {
                string conditionsFileText;
                conditionsFileText = streamReader.ReadToEnd().Replace(".", "");

                // Получаем предложения ЕСЛИ и ТО.
                string[] wordsA;
                wordsA = conditionsFileText.Split(new string[] { "Если ", ", то", }, StringSplitOptions.RemoveEmptyEntries);

                // Записываем по индексам ЕСЛИ и ТО.
                conditions = new string[wordsA.Length / 2, 2];
                for (int i = 0; i < conditions.GetLength(0); i++)
                {       
                    conditions[i, 0] = i + 1 == 1 ? wordsA[i] : wordsA[i + i];
                    conditions[i, 1] = wordsA[i + 1 == 2 ? i + 2 : i + i + 1];
                }
            }

            string fileText;
            using (StreamReader streamReader = new StreamReader(FILE_FOR_ANALYSIS_PATH, Encoding.UTF8))
            {
                fileText = streamReader.ReadToEnd();
            }

            Statement[] statements = new Statement[conditions.GetLength(0)];
            for (int i = 0; i < statements.Length; i++)
            {
                statements[i] = new Statement { StatementA = conditions[i, 0], StatementB = conditions[i, 1] };
            }

            for (int i = 0; i < statements.Length; i++)
            {
                IntelSys.FindAnswer(statements[i], fileText);
            }

            Console.ReadLine();
        }
    }
}