using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Interfaces;
using Salt.Messages;

namespace Salt.Business
{
    public interface ISaltApp
    {
        IEnumerable<IContactItem> GetContacts();

        IEnumerable<IMessageStoreItem> GetMessageStoreItemsByContactId(Guid contactId);

        MessageViewModel GetDecryptedMessage(Guid id);
    }
}
