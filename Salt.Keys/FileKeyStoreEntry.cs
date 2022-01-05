using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Keys
{
    public class FileKeyStoreEntry
    {
        public FileKeyStoreEntry(string keyName, string filePath)
        {
            KeyName = keyName;
            FilePath = filePath;
        }

        public string KeyName { get; private set; }

        public string FilePath { get; private set; }
    }
}
