using ProcessingTextFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj1
{
    class CLIHandler
    {

        private List<string> commands;
        private IntelSys intelSys;
        public Dictionary<string, string> parsedArgs;
        public CLIHandler(IntelSys intelSys)
        {
            this.intelSys = intelSys;
            this.commands = new List<string>();
            commands.Add("-h"); commands.Add("--help");
            commands.Add("-a"); commands.Add("-answer");
            commands.Add("-c"); commands.Add("--change-db");
            commands.Add("-q"); commands.Add("--question");
            commands.Add("-p"); commands.Add("--path");
            commands.Add("-f"); commands.Add("--file");
            commands.Add("-o"); commands.Add("--output");
        }


        public string GetHelp()
        {
            string help = "HELP:\n" +
                "-c --csv <path to csv> - get expert data from csv\n" +
                "-a --answer              - get answer\n" +
                "-q --question <question> - string question\n" +
                "-p --path <path>         - path to file with question\n" +
                "-f --f <path>            - path to file with text database\n" +
                "-o --output <path>       - path to output file with csv database\n" +
                "-h --help                - to get this message";
            return help;
        }

        private string GetError()
        {
            return "Неверные аргументы!\n";
        }

        private void debugArgs()
        {
            foreach (var d in parsedArgs)
            {
                Console.WriteLine(d.Key + " " + d.Value);
            }
        }

        private void ImplementArgs()
        {

            foreach (var pair in parsedArgs)
            {
                var key = pair.Key;
                var val = pair.Value;
                if (!(this.commands.Contains(key)))
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

        public void ParseArgs(string[] args)
        {
            this.parsedArgs = new Dictionary<string, string>();
            string prevKey = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0].Equals('-'))
                {
                    prevKey = args[i];
                    parsedArgs.Add(args[i], null);
                }
                else
                {
                    parsedArgs[prevKey] = args[i];
                }

            }

            //debugArgs();
            ImplementArgs();
        }
    }
}
