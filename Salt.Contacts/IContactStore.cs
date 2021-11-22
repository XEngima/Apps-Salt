using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Contacts
{
    public interface IContactStore
    {
        IEnumerable<IContactItem> GetAllContacts();
    }
}
