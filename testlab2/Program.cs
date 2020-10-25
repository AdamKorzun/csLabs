using System;
using System.Threading;
using lab2lib;
using System.IO;
namespace testlab2
{
    class Program
    {
        public static void OnCreated(object source, FileSystemEventArgs e)
        {
            // fix 

            var ext = Path.GetExtension(e.FullPath);

            if (ext == ".txt")
            {

                var newName = Path.ChangeExtension(e.FullPath, ".gz");
                newName = newName.Replace("SourceDirectory", "TargetDirectory");
                Console.WriteLine(newName);
                FW.SendFile(e.FullPath, newName, "aaaaaaaa");
            }
            else if (ext == ".gz")
            {
                FW.ReceiveFile(e.FullPath, Path.ChangeExtension(e.FullPath, ".txt"), "aaaaaaaa");
            }
        }
        
        static void Main(string[] args)
        {
            FW.OnCreate CrFunc = OnCreated;
            FW.WatchDir(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\SourceDirectory", "*.txt", CrFunc);
            FW.WatchDir(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\TargetDirectory", "*.gz", CrFunc);
            while (true) ;  
            

        }
    }
}
