using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Contacts
{
    public interface IContactStore
    {
        IEnumerable<IContactStoreItem> GetAllContacts();

        void Add(IContactStoreItem contactStoreItem);
    }
}
