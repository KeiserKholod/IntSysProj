using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProcessingTextFile
{
    internal class IntelSys
    {
        private static string[,] conditions = null;
        public string CSVOut = "outDB1.csv";
        private string result;
        private void WriteCSV(string path, string if_txt, string then_txt)
        {
            string readText = "";
            try
            {
                readText = File.ReadAllText(path);
            }
            catch
            {
                readText = "IF_Statement, THEN_Statement";
            }
            string result = readText + "\n" + if_txt + "; " + then_txt;
            File.WriteAllText(path, result);

        }

        public string[,] GetExpertData(StreamReader streamReader)
        {
            using (streamReader)
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
            return conditions;
        }

        public string[,] GetExpertData(StreamReader streamReader, bool csv)
        {
            using (streamReader)
            {
                List<string> listIF = new List<string>();
                List<string> listTHEN = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var values = line.Split(';');

                    listIF.Add(values[0]);
                    listTHEN.Add(values[1]);
                }

                conditions = new string[listIF.Count, 2];
                for (int i = 0; i < conditions.GetLength(0); i++)
                {
                    conditions[i, 0] = listIF[i];
                    conditions[i, 1] = listTHEN[i];
                }
                return conditions;
            }
        }

        public string GetAnalisysData(StreamReader streamReader)
        {
            string fileText = streamReader.ReadToEnd();
            return fileText;
        }

        public string GetAnalisysData(String fileText)
        {
            return fileText;
        }

        private void FindAnswerIteration(Statement statement, string fileText)
        {
            string[] splittersSymbolsA = new string[statement.StatementA.Length];
            //string[] splittersSymbolsB = new string[statement.StatementB.Length];

            GetSeparatingSymbols(statement, ref splittersSymbolsA/*, /*ref splittersSymbolsB*/);

            string[] statementAParts = statement.StatementA.Split(new string[] { " и ", "или" }, StringSplitOptions.RemoveEmptyEntries);
            //string[] statementBParts = statement.StatementA.Split(new string[] { " и ", "или" }, StringSplitOptions.RemoveEmptyEntries);


            // Нет обратного вывода, вывода без условий "И" и "ИЛИ", для вывода можно только одно условие.
            for (int i = 0; i < splittersSymbolsA.Length; i++)
            {
                switch (splittersSymbolsA[i])
                {
                    case "&&":
                        if (fileText.Contains(statementAParts[i]) && fileText.Contains(statementAParts[i + 1]))
                        {
                            Console.WriteLine(statement.StatementB);
                            result = statement.StatementB;
                        }
                        break;

                    case "||":
                        if (fileText.Contains(statementAParts[i]) || fileText.Contains(statementAParts[i + 1]))
                        {
                            Console.WriteLine(statement.StatementB);
                            result = statement.StatementB;
                        }
                        break;

                    default:
                        try
                        {
                            if (fileText.Contains(statementAParts[i]))
                            {
                                Console.WriteLine(statement.StatementB);
                                result = statement.StatementB;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Была поймана ошибка: {ex}");
                            result = "Ответ не найден";
                        }
                        break;

                }
            }

            //for (int i = 0; i < splittersSymbolsB.Length; i++)
            //{
            //    switch (splittersSymbolsB[i])
            //    {
            //        case "&&":
            //            if (fileText.Contains(statementBParts[i]) && fileText.Contains(statementBParts[i + 1]))
            //            {
            //                Console.WriteLine($"{statement.StatementA}");
            //            }
            //            break;

            //        case "||":
            //            if (fileText.Contains(statementBParts[i]) || fileText.Contains(statementBParts[i + 1]))
            //            {
            //                Console.WriteLine($"{statement.StatementA}");
            //            }
            //            break;
            //    }
            //}
        }

        public void FindAnswer(string fileText)
        {
            Statement[] statements = new Statement[conditions.GetLength(0)];
            for (int i = 0; i < statements.Length; i++)
            {
                statements[i] = new Statement { StatementA = conditions[i, 0], StatementB = conditions[i, 1] };
            }

            for (int i = 0; i < statements.Length; i++)
            {
                this.FindAnswerIteration(statements[i], fileText);
            }
            this.WriteCSV(CSVOut, fileText, result);
        }

        internal static void GetSeparatingSymbols(Statement statement, ref string[] splittersSymbolsA/*, ref string[] splittersSymbolsB*/)
        {
            for (int i = 0; i < statement.StatementA.Length; i++)
            {
                if (i + 3 <= statement.StatementA.Length && statement.StatementA.Substring(i, 3) == " и ")
                {
                    splittersSymbolsA[i] += "&&";
                }
                else if (statement.StatementA == " или ")
                {
                    splittersSymbolsA[i] += "||";
                }
            }
            splittersSymbolsA = splittersSymbolsA.Where(x => x != null).ToArray();

            //for (int i = 0; i < statement.StatementB.Length; i++)
            //{
            //    if (i + 3 <= statement.StatementA.Length && statement.StatementA.Substring(i, 3) == " и ")
            //    {
            //        splittersSymbolsB[i] += "&&";
            //    }
            //    else if (statement.StatementB == " или ")
            //    {
            //        splittersSymbolsB[i] += "||";
            //    }
            //}
            //splittersSymbolsB = splittersSymbolsB.Where(x => x != null).ToArray();
        }
    }
}
