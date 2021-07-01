using Salt.Interfaces;
using System;

namespace Salt.Keys
{
    public class KeyStore : IKeyStore
    {
        public string GetKeyPart(string keyNameHash, int pos, int length)
        {
            return "";
        }

        public bool HasKey(string keyNameHash)
        {
            return false;
        }

        public void SaveItem(string keyName, string fullPath)
        {
        }
    }
}
