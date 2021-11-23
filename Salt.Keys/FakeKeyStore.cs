using System;
using System.Collections.Generic;
using System.Linq;

namespace Salt.Keys
{
    public class FakeKeyStore : IKeyStore
    {
        public FakeKeyStore()
        {
            Items = new List<IKeyStoreItem>
            {
                new KeyStoreItem("KEYNAME", "case")
            };
        }

        private List<IKeyStoreItem> Items { get; set; }

        public string GetKeyPart(string keyName, int pos, int length)
        {
            var keyStoreItem = Items.FirstOrDefault(x => x.KeyName == keyName);

            if (keyStoreItem != null)
            {
                return keyStoreItem.Key.Substring(pos, length);
            }

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
