using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    public class EncryptionOptions
    {
        private string encryptionKey;
        private bool encrypt;
        public bool Encrypt
        {
            get => encrypt;
            set => encrypt = value;
        }
        public string EncryptionKey
        {
            get => encryptionKey;
            set
            {
                if (value != null && value.Length != 8)
                {
                    throw new Exception("Encryption key shoould be 8 long");
                }
                encryptionKey = value;
            }

        }
        public EncryptionOptions() { }
    }
}
