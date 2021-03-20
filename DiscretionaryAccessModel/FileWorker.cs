using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace DiscretionaryAccessModel
{
    class FileWorker
    {

        private static string pathToFS = @"..\..\..\files.json";

        private string userName { get; set; }

        private Dictionary<string, File> files;

        public FileWorker(string userName)
        {
            FileInfo fileInfo = new(pathToFS);
            this.userName = userName;

            if (!fileInfo.Exists)
            {
                //throw new Exception("Invalid path to users file!");
                Console.WriteLine("Invalid path to users file!");
                return;
            }

            StreamReader read = new StreamReader(pathToFS);
            string filesJson = read.ReadToEnd();
            read.Close();

            files = JsonSerializer.Deserialize<Dictionary<string, File>>(filesJson);
        }

        public bool CanChangeFile(File file)
        {
            int accessLevel;
            file.access.TryGetValue(userName, out accessLevel);

            if (accessLevel < 2)
            {
                Console.WriteLine($"Access denied! Your level {accessLevel}, you need level 2 or higher to change file");
                return false;
            }

            return true;
        }

        public bool CanReadFile(File file)
        {
            int accessLevel;
            file.access.TryGetValue(userName, out accessLevel);

            if (accessLevel != 1 && accessLevel != 3)
            {
                Console.WriteLine($"Access denied! Your level {accessLevel}, you need level 1 or 3 to read file");
                return false;
            }

            return true;
        }
    
        public void ChageFile(string filename, string text)
        {
            
            File file;
            files.TryGetValue(filename, out file);

            if (!CanChangeFile(file)) return;

            file.text = text;
            string filesJson = JsonSerializer.Serialize(files);

            StreamWriter sr = new StreamWriter(pathToFS, false, System.Text.Encoding.Default);
            sr.WriteLine(filesJson);
            sr.Close();

            Console.WriteLine($"File {filename} was chenged");
        }

        public void ReadFile(string filename)
        {
            File file;
            files.TryGetValue(filename, out file);

            if (!CanReadFile(file)) return;
            Console.WriteLine(file.text);
        }

    }
}
