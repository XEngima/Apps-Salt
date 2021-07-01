using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestKeyStoreItem
    {
        public TestKeyStoreItem(string keyName, int startPos, int length, string keyPart)
        {
            if (keyPart.Length != length)
            {
                throw new ArgumentException("The key part must have the length of the length argument.");
            }

            KeyName = keyName;
            StartPos = startPos;
            Length = length;
            KeyPart = keyPart;
        }

        public string KeyName { get; private set; }

        public int StartPos { get; private set; }

        public int Length { get; private set; }

        public string KeyPart { get; private set; }
    }
}
