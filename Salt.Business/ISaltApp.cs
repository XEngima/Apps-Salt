using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Messages;

namespace Salt.Business
{
    public interface ISaltApp
    {
        IEnumerable<IContactStoreItem> GetContacts();

        IEnumerable<IMessageHeader> GetMessageHeadersByContactId(Guid contactId);

        IEnumerable<IMessageStoreItem> GetMessageStoreItemsByContactId(Guid contactId);

        MessageViewModel GetDecryptedMessage(Guid id);
    }
}
