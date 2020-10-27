using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using lab2lib;
namespace lab2ws
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            if (e.OldFullPath != null)
            {
                var oldFileName = e.OldFullPath;
                oldFileName = oldFileName.Replace("SourceDirectory", "TargetDirectory");
                File.Delete(oldFileName);
                oldFileName = oldFileName.Replace(".txt", ".gz");
                File.Delete(oldFileName);
            }
            try
            {
                var ext = Path.GetExtension(e.FullPath);

                if (ext == ".txt")
                {
                    var time = DateTime.Now;

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
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(@"C:\Users\admin\Desktop\c#sem3\labs\logs.txt"))
                {
                    sw.WriteLine(ex.Message);

                }
            }
        }
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            // TODO:
            // fix file change
            // Add file renamed event
            
        
            try
            {
                var ext = Path.GetExtension(e.FullPath);

                if (ext == ".txt")
                {
                    var time = File.GetCreationTime(e.FullPath);
                   
                    
                    var newFilePath = Path.Combine(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\SourceDirectory", time.Year.ToString());
                    newFilePath = Path.Combine(newFilePath, time.Month.ToString());
                    newFilePath = Path.Combine(newFilePath, time.Day.ToString());
                    newFilePath = Path.Combine(newFilePath, Path.GetFileName(e.FullPath));
                    var newName = Path.ChangeExtension(e.FullPath, ".gz");
                    using (StreamWriter outputFile = new StreamWriter(@"C:\Users\admin\Desktop\c#sem3\labs\logs.txt"))
                    {
                        outputFile.Write(newFilePath);
                    }
                    if (!Directory.Exists(Path.GetDirectoryName(newFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newFilePath));
                        File.Move(e.FullPath, newFilePath);
                        return;

                    }
                    File.Move(e.FullPath, newFilePath);
                    newName = newName.Replace("SourceDirectory", "TargetDirectory");
                    Console.WriteLine(newName);
                    FW.SendFile(e.FullPath, newName, "aaaaaaaa");
                }
                else if (ext == ".gz")
                {
                    FW.ReceiveFile(e.FullPath, Path.ChangeExtension(e.FullPath, ".txt"), "aaaaaaaa");
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(@"C:\Users\admin\Desktop\c#sem3\labs\logs.txt"))
                {
                    sw.WriteLine(ex.Message);
                    
                }
            }
        }

        FW.OnCreate CrFunc = OnCreated;
        FW.OnRename CrRename = OnRenamed;
        protected override void OnStart(string[] args)
        {
            
            FW.WatchDir(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\SourceDirectory", "*.txt", CrFunc,CrRename);
            FW.WatchDir(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\TargetDirectory", "*.gz",CrFunc,CrRename);
            
        }

        protected override void OnStop()
        {
        }
    }
}
