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
using System.Xml.Schema;
using System.Xml;
using lab3;
using System.Xml.Linq;
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
                var manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.xml");
                var xmlConfig = manager.GetConfig<TransferOptions>();
                manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.json");
                var jsonConfig = manager.GetConfig<TransferOptions>();
                var key = jsonConfig.EncryptionKey;
                var encrypt = xmlConfig.Encryption;
                var compress = jsonConfig.Compress;
                var ext = Path.GetExtension(e.FullPath);

                if (ext == ".txt")
                {
                    var time = DateTime.Now;

                    var newName = Path.ChangeExtension(e.FullPath, ".gz");
                    newName = newName.Replace("SourceDirectory", "TargetDirectory");
                    Console.WriteLine(newName);
                    FW.SendFile(e.FullPath, newName, encrypt, key, compress);
                }
                else if (ext == ".gz")
                {
                    FW.ReceiveFile(e.FullPath, Path.ChangeExtension(e.FullPath, ".txt"), encrypt, key, compress);
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
            try
            {
                var manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.xml");
                var xmlConfig = manager.GetConfig<TransferOptions>();
                manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.json");
                var jsonConfig = manager.GetConfig<TransferOptions>();
                var key = jsonConfig.EncryptionKey;
                var encrypt = xmlConfig.Encryption;
                var compress = jsonConfig.Compress;
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
                    FW.SendFile(e.FullPath, newName, encrypt, key, compress);
                }
                else if (ext == ".gz")
                {
                    FW.ReceiveFile(e.FullPath, Path.ChangeExtension(e.FullPath, ".txt"), encrypt, key, compress);
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

        public readonly FW.OnCreate CrFunc = OnCreated;
        public readonly FW.OnRename CrRename = OnRenamed;
        protected override void OnStart(string[] args)
        {
           
            ValidateXml(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.xsd",
                        @"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.xml");
            var manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.xml");
            var xmlConfig = manager.GetConfig<TransferOptions>();

            manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.json");
            var jsonConfig = manager.GetConfig<TransferOptions>();
            var source = xmlConfig.SourceFilePath;
            var target = jsonConfig.TargetFilePath;
            FW.WatchDir(source, "*.txt", CrFunc,CrRename);
            FW.WatchDir(target, "*.gz",CrFunc,CrRename);
            
        }
        private void ValidateXml(string xsdFilePath, string xmlFilePath)
        {
            var schema = new XmlSchemaSet();
            schema.Add(string.Empty, xsdFilePath);
            XDocument doc = XDocument.Load(xmlFilePath);

            doc.Validate(schema, ValidationCallBack);
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);
        }
        protected override void OnStop()
        {
        }
    }
}
