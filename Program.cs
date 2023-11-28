using prj1;
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

        static void Main(string[] args)
        {
            var cliHandler = new CLIHandler();
            cliHandler.ParseArgs(args);

            var intelSys = new IntelSys();
            conditions = intelSys.ReadInputData(CONDITIONS_FILE_PATH);
            var fileText = intelSys.GetAnalisysData(FILE_FOR_ANALYSIS_PATH);
            intelSys.FindAnswer(fileText);

            Console.ReadLine();
        }
    }
}