using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscretionaryAccessModel
{
    class CommandInterpreter
    {
        private FileWorker fileWorker;
        public CommandInterpreter(string userName)
        {
            fileWorker = new FileWorker(userName);
            Console.WriteLine("Read file - read filename");
            Console.WriteLine("Change file - change filename newtext");
        }

        public static string[] SplitComand(string commandLine) 
            => commandLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        public static bool isExit(string command) => command == "exit" ? true : false;
   
        private bool isValidCommand(string commandLine)
        {
            string[] splitCommand = SplitComand(commandLine);

            if (splitCommand.Length == 2)
            {
                if (splitCommand[0] == "read" || splitCommand[0] == "delete")
                {
                    return true;
                }
            }
            else if (splitCommand.Length == 3)
            {
                if (splitCommand[0] == "create" || splitCommand[0] == "change")
                {
                    return true;
                }
            }

            Console.WriteLine("Invalid command");
            return false;
        }

        public void WriteCommand()
        {
            bool validateResult;
            string command;

            do
            {
                command = Console.ReadLine();
                command.Trim();
                if (isExit(command)) return;

                validateResult = isValidCommand(command);
            } while (!validateResult);

            string[] splitCommand = SplitComand(command);
            switch (splitCommand[0])
            {
                case "change":
                    fileWorker.ChageFile(splitCommand[1], splitCommand[2]);
                    break;
                case "read":
                    fileWorker.ReadFile(splitCommand[1]);
                    break;
            }

        }
    }
}
