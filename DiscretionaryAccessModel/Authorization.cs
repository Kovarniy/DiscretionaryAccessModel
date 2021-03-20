using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DiscretionaryAccessModel
{
    class Authorization
    {

        private static string pathToUsersFile = @"..\..\..\users.json";

        private static bool hasAccess = false;

        private static string getHashMd5(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);

            // формирование строки из бассива байт 
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x}", b);

            return hash;
        }

        private static bool HasAccess(string name, string pass)
        {
            // В C# есть такой способ
            FileInfo fileInfo = new(pathToUsersFile);

            if (!fileInfo.Exists)
            {
                Console.WriteLine("Invalid path to users file!");
                return false;
            }

            StreamReader read = new StreamReader(pathToUsersFile);
            string usersJson = read.ReadToEnd();
            read.Close();
            Dictionary<string, string> users = JsonSerializer.Deserialize<Dictionary<string, string>>(usersJson);
            
            string ans;
            users.TryGetValue(name, out ans);
            if (ans == pass) return true;

            Console.WriteLine("Invalid login or password!");
            return false;
        }

        public static void login()
        {
            Console.WriteLine("Command 'exit' for exit in programm");
            string name;
            bool exit;

            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                exit = CommandInterpreter.isExit(name);
                if (exit) return;

                Console.Write("Password: ");
                string pass = Console.ReadLine();
                exit = CommandInterpreter.isExit(pass);
                if (exit) return;

                string hashPass = getHashMd5(pass);
                hasAccess = HasAccess(name, hashPass);
            } while (!hasAccess);

            CommandInterpreter cI = new CommandInterpreter(name);
            cI.WriteCommand();
        }
    }
}
