﻿using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Messages;

namespace Salt.Business
{
    public interface ISaltApp
    {
        IEnumerable<IContactStoreItem> GetContacts();

        IEnumerable<SaltMessageHeader> GetDecryptedMessageHeadersByRecipientId(Guid contactId);

        IEnumerable<IMessageStoreItem> GetMessageStoreItemsByContactId(Guid contactId);

        MessageViewModel GetDecryptedMessage(Guid id);
    }
}
