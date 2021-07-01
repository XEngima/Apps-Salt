using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestKeyStore : IKeyStore
    {
        public TestKeyStore(IHashGenerator hashGenerator)
        {
            Items = new List<TestKeyStoreItem>();
            HashGenerator = hashGenerator;
        }

        private IHashGenerator HashGenerator { get; set; }

        public IEnumerable<TestKeyStoreItem> Items { get; set; }

        public string GetKeyPart(string keyNameHash, int pos, int length)
        {
            foreach (var item in Items)
            {
                if (HashGenerator.CreateHash(item.KeyName) == keyNameHash && pos == item.StartPos && item.Length == length)
                {
                    return item.KeyPart;
                }
            }

            return string.Empty;
        }

        public bool HasKey(string keyName)
        {
            return false;
        }

        public void SaveItem(string keyName, string fullPath)
        {
        }
    }
}
