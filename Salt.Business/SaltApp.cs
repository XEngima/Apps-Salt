using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class SaltApp : ISaltApp
    {
        public SaltApp()
        {
            ContactStore = Factory.CreateContactStore();
            MessageStore = Factory.CreateMessageStore();
        }

        private IContactStore ContactStore { get; set; }

        private IMessageStore MessageStore { get; set; }

        public IEnumerable<IContactItem> GetContacts()
        {
            return ContactStore.GetAllContacts();
        }

        public IMessage GetMessage(int id)
        {
            return MessageStore.GetMessage(id);
        }

        public IEnumerable<IMessage> GetMessages(Guid contactId)
        {
            return MessageStore.GetMessages(contactId);
        }
    }
}
