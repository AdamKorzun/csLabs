using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace lab3
{
    public class TransferOptions
    {
        private bool encryption;
        private bool compress;
        private string encryptionKey;
        private string sourceFilePath;
        private string targetFilePath;
        public bool Encryption
        {
            get => encryption;
            set => encryption = value;

        }
        public bool Compress
        {
            get => compress;
            set => compress = value;

        }
        public string EncryptionKey
        {
            get => encryptionKey;
            set
            {
                if (value != null && value.Length != 8)
                {
                    throw new Exception("Encryption key should be 8 long");
                }
                encryptionKey = value;
            }
        }
        public string SourceFilePath
        {
            get => sourceFilePath;
            set
            {
                if (Directory.Exists(value))
                {
                    sourceFilePath = value;
                }
                else
                {
                    // Add error logger
                    throw new Exception("COuldn't find directory");
                }
            }

        }
        public string TargetFilePath
        {
            get => targetFilePath;
            set
            {
                if (Directory.Exists(value))
                {
                    targetFilePath = value;
                }
                else
                {
                    // Add error logger
                    throw new Exception("COuldn't find directory");
                }
            }

        }

        public TransferOptions(string sourceFilePath, string targetFilePath,
                                string encryptionKey = null, bool encryption = false, bool compress = false)
        {
            if (Directory.Exists(sourceFilePath) && Directory.Exists(targetFilePath))
            {
                this.sourceFilePath = sourceFilePath;
                this.targetFilePath = targetFilePath;
            }
            else
            {
                // Add error logger
                throw new Exception("Couldn't find directory");
            }
            this.compress = compress;
            if (encryptionKey != null)
            {
                this.encryptionKey = encryptionKey;
            }

            this.encryption = encryption;
        }
        public TransferOptions() { }
    }
}
