using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestKeyStoreItem : IKeyStoreItem
    {
        public TestKeyStoreItem(string keyName, int startPos, int length, string key)
        {
            if (key.Length != length)
            {
                throw new ArgumentException("The key part must have the length of the length argument.");
            }

            KeyName = keyName;
            StartPos = startPos;
            Length = length;
            Key = key;
        }

        public string KeyName { get; private set; }

        public int StartPos { get; private set; }

        public int Length { get; private set; }

        public string Key { get; private set; }
    }
}
