using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestKeyStore : IKeyStore
    {
        public TestKeyStore()
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
            foreach (var item in Items)
            {
                if (item.KeyName == keyName)
                {
                    return item.Key.Substring(pos, length);
                }
            }

            return string.Empty;
        }

        public void Add(IKeyStoreItem keyStoreItem)
        {
            Items.Add(keyStoreItem);
        }

        public bool KeyExists(string keyName)
        {
            return Items.FirstOrDefault(e => e.KeyName == keyName) != null;
        }
    }
}
