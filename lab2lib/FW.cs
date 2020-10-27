using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace lab2lib
{
    public static class FW
    {
        public static void SendFile(string path, string targetPath,string key)
        {
            if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            }
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);
            using (CryptoStream fs = new CryptoStream(new FileStream(path, FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite), cryptic.CreateEncryptor(),CryptoStreamMode.Read))
            {
                using (FileStream ts =  new FileStream(targetPath,FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite))
                {
                    using (GZipStream compressionStream = new GZipStream(ts, CompressionMode.Compress))
                    {
                        fs.CopyTo(compressionStream);

                    }
                }
                
            }
        }
        public static void ReceiveFile(string path, string targetFile, string key)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);
            using (FileStream sourceStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                
                using (FileStream targetStream = File.Create(targetFile))
                {
                    
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        using (CryptoStream cs = new CryptoStream(decompressionStream, cryptic.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            cs.CopyTo(targetStream);
                        }

                    }
                }
            }
        }

      
        public delegate void OnCreate(object source, FileSystemEventArgs e);
        public static OnCreate onCreated;
        public delegate void OnRename(object source, RenamedEventArgs  e);
        public static OnRename OnRenamed;
        public static void WatchDir(string inputDir, string fileRegEx, OnCreate OnCreated, OnRename OnRename = null)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = inputDir;
            //watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = fileRegEx;
            var handler = new FileSystemEventHandler(OnCreated);
            watcher.IncludeSubdirectories = true;
            //watcher.Changed += handler;
            watcher.Created += handler;
            if (OnRename != null)
            {
                watcher.Renamed += new RenamedEventHandler(OnRename)    ;
            }
            
            watcher.EnableRaisingEvents = true;
            

        }
    }
}
