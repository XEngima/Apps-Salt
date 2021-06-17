using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface ISaltApp
    {
        IEnumerable<IContactItem> GetContacts();

        IEnumerable<IMessage> GetMessagesByKeyName(string contactId, string keyName);
    }
}
