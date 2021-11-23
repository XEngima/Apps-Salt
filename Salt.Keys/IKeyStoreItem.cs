using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Keys
{
    public interface IKeyStoreItem
    {
        string KeyName { get; }

        string Key { get; }
    }
}
