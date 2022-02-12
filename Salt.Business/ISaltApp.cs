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

        IEnumerable<SaltMessageHeader> GetMessageHeadersByRecipientId(Guid contactId);

        IEnumerable<SaltMessageHeader> GetMessageHeadersByAnyContactId(Guid contactId);

        SaltMessage GetMessage(Guid id);

        void SendMessage(Guid recipient, string subject, string message, string keyName);

        void SaveContact(string name, Guid id, string keyName);

        Guid GenerateKey(int size);
    }
}
