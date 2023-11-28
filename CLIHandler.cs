using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj1
{
    class CLIHandler
    {
        private List<string> commandsArr;
        public CLIHandler()
        {
            this.commandsArr = new List<string>();
            commandsArr.Add("-h"); commandsArr.Add("--help");
            commandsArr.Add("-a"); commandsArr.Add("-answer");
            commandsArr.Add("-c"); commandsArr.Add("--change-db");
            commandsArr.Add("-q"); commandsArr.Add("--question");
            commandsArr.Add("-p"); commandsArr.Add("--path");
        }

        private List<Dictionary<string, string>> parsedArgs;
        public string GetHelp()
        {
            string help = "HELP:\n" +
                "-c --change-db - change DB to connect\n" +
                "-a --answer - get answer\n" +
                "-q --question - string question\n" +
                "-p --path - path to file with question\n" +
                "-h --help - to get this message";
            return help;
        }

        private string GetError()
        {
            return "Неверные аргументы!\n";
        }

        private void debugArgs()
        {
            foreach (var item in parsedArgs)
            {
                foreach (var d in item)
                {
                    Console.WriteLine(d.Key + " " + d.Value);
                }
            }
        }

        private void ImplementArgs()
        {
            foreach (var item in parsedArgs)
            {
                foreach (var pair in item)
                {
                    var key = pair.Key;
                    var val = pair.Value;
                    if (!(this.commandsArr.Contains(key)))
                    {
                        Console.WriteLine(GetError());
                        Console.WriteLine(GetHelp());
                        System.Environment.Exit(1);
                    }
                    if (key.Equals("-h") || key.Equals("--help"))
                    {
                        Console.WriteLine(GetHelp());
                        System.Environment.Exit(1);
                    }

                }
            }
        }

        public void ParseArgs(string[] args)
        {
            this.parsedArgs = new List<Dictionary<string, string>>();
            Dictionary<string, string> prevDict = null;
            string prevKey = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0].Equals('-'))
                {
                    var keyValue = new Dictionary<string, string>();
                    prevDict = keyValue;
                    prevKey = args[i];
                    keyValue.Add(args[i], null);
                    parsedArgs.Add(keyValue);
                }
                else
                {
                    prevDict[prevKey] = args[i];
                }

            }

            debugArgs();
            ImplementArgs();
        }
    }
}
