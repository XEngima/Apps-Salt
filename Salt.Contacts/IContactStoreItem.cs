using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Contacts
{
    public interface IContactStoreItem
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string KeyName { get; set; }
    }
}
