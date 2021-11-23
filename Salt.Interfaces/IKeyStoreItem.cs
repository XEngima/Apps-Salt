using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IKeyStoreItem
    {
        string KeyName { get; }

        string Key { get; }
    }
}
