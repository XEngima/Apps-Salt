using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Contacts
{
    public interface IContactStoreItem
    {
        Guid Id { get; }

        string Name { get; }

        string KeyName { get; }
    }
}
