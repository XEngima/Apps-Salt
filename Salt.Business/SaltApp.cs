using Salt.Interfaces;
using Salt.Cypher;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class SaltApp : ISaltApp
    {
        public SaltApp(IContactStore contactStore = null, IMessageStore messageStore = null, ICryptographer cryptographer = null)
        {
            ContactStore = contactStore;
            MessageStore = messageStore;
            Cryptographer = cryptographer;

            if (contactStore == null)
            {
                ContactStore = Factory.CreateContactStore();
            }

            if (messageStore == null)
            {
                MessageStore = Factory.CreateMessageStore();
            }

            if (cryptographer == null)
            {
                Cryptographer = new Cryptographer();
            }
        }

        private IContactStore ContactStore { get; set; }

        private IMessageStore MessageStore { get; set; }

        private ICryptographer Cryptographer { get; set; }


        public IEnumerable<IContactItem> GetContacts()
        {
            return ContactStore.GetAllContacts();
        }

        public IMessage GetDecryptedMessage(int id)
        {
            var message = MessageStore.GetMessage(id);

            return message.Decrypt(Cryptographer, "aaaaaaaaaa");
        }

        public IEnumerable<IMessage> GetMessages(Guid contactId)
        {
            return MessageStore.GetMessages(contactId);
        }
    }
}
