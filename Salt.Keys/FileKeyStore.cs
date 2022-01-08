using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Salt.Keys
{
    public class FileKeyStore : IKeyStore
    {
        public FileKeyStore(string folderPath)
        {
            FolderPath = folderPath;
            KeyStoreEntries = new List<FileKeyStoreEntry>();

            // if the key store directory does not exist, then create it

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePaths = Directory.GetFiles(FolderPath, "*.key");

            foreach (var filePath in filePaths)
            {
                string keyName = Path.GetFileNameWithoutExtension(filePath);
                KeyStoreEntries.Add(new FileKeyStoreEntry(keyName, filePath));
            }
        }

        public string FolderPath { get; set; }

        public List<FileKeyStoreEntry> KeyStoreEntries { get; set; }

        public IEnumerable<string> GetAllKeyNames()
        {
            return KeyStoreEntries.Select(e => e.KeyName).ToList();
        }

        public string GetKeyPart(string keyName, int pos, int length)
        {
            var entry = KeyStoreEntries.First(e => e.KeyName == keyName);

            using (StreamReader streamReader = File.OpenText(entry.FilePath))
            {
                char[] buffer = new char[pos];

                if (pos > 0)
                {
                    streamReader.Read(buffer, 0, pos);
                }

                buffer = new char[length];
                streamReader.Read(buffer, 0, length);

                return new string(buffer);
            }
        }

        public bool KeyExists(string keyName)
        {
            return KeyStoreEntries.Exists(e => e.KeyName == keyName);
        }
    }
}
