using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salt.Keys
{
    public class LetterKeyStore : IKeyStore
    {
        public LetterKeyStore()
        {
            Items = new List<IKeyStoreItem>();
        }

        private List<IKeyStoreItem> Items { get; set; }

        public string GetKeyPart(string keyName, int pos, int length)
        {
            var sbKeyPart = new StringBuilder();
            var keyStoreItem = Items.FirstOrDefault(x => x.KeyName == keyName);

            if (keyStoreItem != null)
            {
                for (int i = 0; i < length; i++)
                {
                    sbKeyPart.Append("a");
                }
            }

            return sbKeyPart.ToString();
        }

        public bool HasKey(string keyNameHash)
        {
            return false;
        }

        public void Add(string keyName, string fullPath)
        {
            Items.Add(new KeyStoreItem(keyName, ""));
        }

        public IEnumerable<string> GetAllKeyNames()
        {
            return Items.Select(x => x.KeyName);
        }
    }
}
