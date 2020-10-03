using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text;


namespace lab1
{
    public  class Coin
    {
        private string _name;
        private float _price;
        public Coin(string name, float price)
        {
            _name = name;
            _price = price;
        }
        public Coin(){}
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public float Price
        {
            get { return _price; }
            set
            {
                _price = value;
            }
        }   
    }

    class Program
    {
        static string GetFileName()
        {

            while (true)
            {
                try
                {

                    Console.Write("Filename: ");
                    var filename = Console.ReadLine();
                    var ext = Path.GetExtension(filename);
                    if (ext != ".json" && ext !=".gz")
                    {
                        throw new Exception("File extensino must be .json or .gz");
                    }
                    return filename;
                }
                catch (Exception e)
                {

                    Console.WriteLine("Wrong input: \n{0}", e.Message);
                }
            }
        }
        static string GetAllFiles(string filename)
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), filename, SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                
                throw new Exception("Couldn't find any files");
            }
            string fileToOpen;
            if (files.Length == 1)
            {
                fileToOpen = files[0];
            }
            else
            {
                int i = 1;
                foreach (var file in files)
                {
                    Console.WriteLine(i.ToString() + ". " + file);
                    i++;
                }

                int option;
                while (true)
                {   
                    Console.Write("File number: ");
                    try
                    {
                        option = int.Parse(Console.ReadLine());
                        fileToOpen = files[option - 1];
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Wrong input, {0}", e.Message);
                    }
                }

            }
            return fileToOpen;
        }
        static string RenameFile(string oldName)
        {
            string newName;
            string[] badChars = new string[] { "/", "\\", ":", "*", "?", "\"", "<", ">", "|" };
            while (true)
            {
                try
                {
                    Console.Write("New file name: ");
                    newName = Console.ReadLine();
                    foreach (var character in badChars)
                    {
                        if (newName.Contains(character))
                        {
                            throw new Exception("usage of not-allowed symbol");
                        }
                    }
                    if (newName.Length > 260)
                    {
                        throw new Exception("Name should be < 260 characters");

                    }
                    if (Path.GetExtension(newName).ToLower() == ".json")
                    {
                        newName = Regex.Replace(oldName, @"[ \w-]+\.json", newName, RegexOptions.IgnoreCase);
                        File.Move(oldName, newName);
                        //[ \w-]+.json - json file regex
                        break;
                    }
                    else if (Path.GetExtension(newName).ToLower() == ".gz")
                    {
                        newName = Regex.Replace(oldName, @"[ \w-]+\.gz", newName, RegexOptions.IgnoreCase);
                        File.Move(oldName, newName);
                        //[ \w-]+.gz - gz file regex
                        break;
                    }
                    else
                    {
                        throw new Exception("File extension must be .json");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
              
            }
           
            return newName;
        }
        static string ReadDataCompressed(string filepath) //fix
        {
            using (FileStream src = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                using (GZipStream dcmpStream = new GZipStream(src, CompressionMode.Decompress))
                {
                    StreamReader reader = new StreamReader(dcmpStream);
                    string resString = reader.ReadToEnd();
                    return resString;
                }
            }
        }
        static string ReadData(string filepath)
        {
            var jsonString = File.ReadAllText(filepath);
            return jsonString;
        }
        static void WriteDataCompressed(string jsonData,string filepath)
        {
            using (FileStream source = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                using (GZipStream cmpStream = new GZipStream(source, CompressionMode.Compress))
                {
                    new MemoryStream(Encoding.ASCII.GetBytes(jsonData)).CopyTo(cmpStream);
                }
                
            }
           
        }
        static void WriteData(string path,string data)
        {
            
            File.WriteAllText(Path.Join(Directory.GetCurrentDirectory(),path),data);   
            
        }
        static void PrintMenu()
        {
            Console.WriteLine("1 - open file");
            Console.WriteLine("2 - save file");
            Console.WriteLine("3 - rename file");
            Console.WriteLine("4 - move file");
            Console.WriteLine("5 - view history");
            Console.WriteLine("6 - exit");
            Console.Write("Option: ");
        }
        static void MoveFile(string oldpath)
        {
            Console.WriteLine("New directory from WorkDir");
            var newPath = Console.ReadLine();
            if (Path.GetExtension(newPath) != ".json" && Path.GetExtension(newPath) != ".gz")
            {
                throw new Exception("File extension must be .json or .gz");
            }
            try
            {
                new FileInfo(Path.Join(Directory.GetCurrentDirectory(), newPath)).Directory.Create();
                File.Move(oldpath, Path.Join(Directory.GetCurrentDirectory(), newPath));
            }
            
            catch (Exception e)
            {
                throw e;
            }
        }
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "/../../../../" + "/WorkDir/");
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            List <Coin> coins = new List<Coin>();
            //coins.Add(new Coin("eth", (float)352.1));
            while (true)
            {
                Console.Clear();
                PrintMenu();
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    var filename = GetFileName();
                    var filepath = GetAllFiles(filename);
                    string jsonString;
                    if (Path.GetExtension(filepath) == ".gz")
                    {
                        jsonString = ReadDataCompressed(filepath);
                    }
                    else
                    {
                        jsonString = ReadData(filepath);
                    }
                    
                    coins = JsonSerializer.Deserialize<List<Coin>>(jsonString);
                    Console.ReadKey();

                }
                else if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    Console.WriteLine("1 - with comression");
                    Console.WriteLine("2 - without comression");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.D1)
                    {
                        string filepath = GetFileName();
                        var jsonData = JsonSerializer.Serialize(coins);
                        WriteDataCompressed(jsonData, filepath);
                    }
                    else if (key.Key == ConsoleKey.D2)
                    {
                        string filepath = GetFileName();
                        string jsonData = JsonSerializer.Serialize(coins);
                        WriteData(filepath, jsonData);
                        

                    }
                    Console.ReadKey();
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    string filepath = GetAllFiles(GetFileName());
                    RenameFile(filepath);
                    Console.ReadKey();

                }
                else if (key.Key == ConsoleKey.D4)
                {
                    try
                    {
                        Console.Clear();
                        var filepath = GetAllFiles(GetFileName());
                        MoveFile(filepath);
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        continue;
                    }
                 
                    
                }
                else if (key.Key == ConsoleKey.D6)
                {
                    return;
                }

            }
                
        }
    }
}
