using System;
using System.Linq;

namespace ProcessingTextFile
{
    internal class IntelSys
    {
        public static void WriteAnswer()
        {

        }

        public static void ReadAnswer()
        {

        }

        public static void FindAnswer(Statement statement, string fileText)
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
                        }
                        break;

                    case "||":
                        if (fileText.Contains(statementAParts[i]) || fileText.Contains(statementAParts[i + 1]))
                        {
                            Console.WriteLine(statement.StatementB);
                        }
                        break;

                    default:
                        try
                        {
                            if (fileText.Contains(statementAParts[i]))
                            {
                                Console.WriteLine(statement.StatementB);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Была поймана ошибка: {ex}");
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
