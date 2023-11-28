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
        private static string fileText = "";

        static void Main(string[] args)
        {
            var intelSys = new IntelSys();
            var cliHandler = new CLIHandler(intelSys);
            cliHandler.ParseArgs(args);


            if (cliHandler.parsedArgs.ContainsKey("-q"))
            {
                var inputQuestion = cliHandler.parsedArgs["-q"];
                fileText = intelSys.GetAnalisysData(inputQuestion);
            }
            else if (cliHandler.parsedArgs.ContainsKey("--question"))
            {
                var inputQuestion = cliHandler.parsedArgs["--question"];
                fileText = intelSys.GetAnalisysData(inputQuestion);
            }

            else if (cliHandler.parsedArgs.ContainsKey("-p"))
            {
                var inputFilePath = cliHandler.parsedArgs["-p"];
                StreamReader streamReader = new StreamReader(inputFilePath, Encoding.UTF8);
                fileText = intelSys.GetAnalisysData(streamReader);
            }
            else if (cliHandler.parsedArgs.ContainsKey("--path"))
            {
                var inputFilePath = cliHandler.parsedArgs["--path"];
                StreamReader streamReader = new StreamReader(inputFilePath, Encoding.UTF8);
                fileText = intelSys.GetAnalisysData(streamReader);
            }

            else
            {
                StreamReader streamReader = new StreamReader(FILE_FOR_ANALYSIS_PATH, Encoding.UTF8);
                fileText = intelSys.GetAnalisysData(streamReader);
            }

            //ВВОДИМЫЕ УСЛОВИЯ ДЛЯ ДВУХМЕРНОГО МАССИВА.txt
            if (cliHandler.parsedArgs.ContainsKey("-f"))
            {
                var textDBPath = cliHandler.parsedArgs["-f"];
                StreamReader streamReader = new StreamReader(textDBPath, Encoding.UTF8);
                conditions = intelSys.GetExpertData(streamReader);
            }
            else if (cliHandler.parsedArgs.ContainsKey("--file"))
            {
                var textDBPath = cliHandler.parsedArgs["--file"];
                StreamReader streamReader = new StreamReader(textDBPath, Encoding.UTF8);
                conditions = intelSys.GetExpertData(streamReader);
            }
            else if (cliHandler.parsedArgs.ContainsKey("-c"))
            {
                var textDBPath = cliHandler.parsedArgs["-c"];
                StreamReader streamReader = new StreamReader(textDBPath, Encoding.UTF8);
                conditions = intelSys.GetExpertData(streamReader, true);
            }
            else if (cliHandler.parsedArgs.ContainsKey("--csv"))
            {
                var textDBPath = cliHandler.parsedArgs["--csv"];
                StreamReader streamReader = new StreamReader(textDBPath, Encoding.UTF8);
                conditions = intelSys.GetExpertData(streamReader,true);
            }
            else
            {
                StreamReader streamReader = new StreamReader(CONDITIONS_FILE_PATH, Encoding.UTF8);
                conditions = intelSys.GetExpertData(streamReader);

            }

            intelSys.FindAnswer(fileText);

            Console.ReadLine();
        }
    }
}