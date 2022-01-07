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

        public IList<IKeyStoreItem> Items { get; private set; }

        public IEnumerable<string> GetAllKeyNames()
        {
            return Items.Select(k => k.KeyName).ToList();
        }

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

        public bool KeyExists(string keyName)
        {
            return Items.FirstOrDefault(e => e.KeyName == keyName) != null;
        }
    }
}
