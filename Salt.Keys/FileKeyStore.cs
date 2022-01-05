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

            //using (FileStream fileStream = new FileStream(entry.FilePath, FileMode.Open, FileAccess.Read))
            //{
            //    // Read the source file into a byte array.
            //    int numBytesToRead = pos;
            //    byte[] bytes = new byte[numBytesToRead];
            //    int numBytesRead = 0;

            //    // Read past the first bytes
            //    if (pos > 0)
            //    {
            //        fileStream.Read(bytes, numBytesRead, numBytesToRead);
            //    }

            //    // Read the current key part
            //    numBytesToRead = length;
            //    bytes = new byte[numBytesToRead];

            //    fileStream.Read(bytes, numBytesRead, numBytesToRead);

            //    string keyPart = System.Text.Encoding.Default.GetString(bytes);

            //    return keyPart;

            //    // ------------------------

                //// Read the source file into a byte array.
                //int numBytesToRead = // Your amount to read at a time
                //byte[] bytes = new byte[numBytesToRead];

                //int numBytesRead = 0;
                //while (numBytesToRead > 0)
                //{
                //    // Read may return anything from 0 to numBytesToRead.
                //    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                //    // Break when the end of the file is reached.
                //    if (n == 0)
                //        break;

                //    // Do here what you want to do with the bytes read (convert to string using Encoding.YourEncoding.GetString())
                //}
            //}
        }
    }
}
