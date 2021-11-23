using Salt.Interfaces;
using System;
using System.Collections.Generic;

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

        public void AddKey(string keyName, string fullPath)
        {
        }

        public IEnumerable<string> GetAllKeyNames()
        {
            return new List<string>();
        }
    }
}
