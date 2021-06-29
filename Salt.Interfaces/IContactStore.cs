using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IContactStore
    {
        IEnumerable<IContactItem> GetAllContacts();
    }
}
