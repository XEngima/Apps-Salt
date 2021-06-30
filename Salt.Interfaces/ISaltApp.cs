using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface ISaltApp
    {
        IEnumerable<IContactItem> GetContacts();

        IEnumerable<IMessage> GetMessages(Guid contactId);

        IMessage GetDecryptedMessage(int id);
    }
}
