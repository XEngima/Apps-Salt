using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Keys
{
    public class KeyStoreItem : IKeyStoreItem
    {
        public KeyStoreItem(string keyName, string key)
        {
            KeyName = keyName;
            Key = key;
        }

        public string KeyName { get; set; }

        public string Key { get; set; }
    }
}
