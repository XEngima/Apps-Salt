using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Salt.Business
{
    public class Settings : ISettings
    {
        public Settings()
        {
        }

        public Settings(string workingDir)
        {
            MyContactId = Guid.NewGuid();
            KeyStoreFolderPath = Path.Combine(workingDir, "KeyStore");
            ContactStoreFolderPath = Path.Combine(workingDir, "ContactStore");
            MessageStoreFolderPath = Path.Combine(workingDir, "MessageStore");
        }

        public Guid MyContactId { get; set; }

        public string MessageStoreFolderPath { get; set; }

        public string KeyStoreFolderPath { get; set; }

        public string ContactStoreFolderPath { get; set; }
    }
}
