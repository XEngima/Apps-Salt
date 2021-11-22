using Salt.Interfaces;
using Salt.Cypher;
using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;

namespace Salt.Business
{
    public class SaltApp : ISaltApp
    {
        public SaltApp(IContactStore contactStore = null, IMessageStore messageStore = null, IKeyStore keyStore = null, ICryptographer cryptographer = null)
        {
            ContactStore = contactStore;
            MessageStore = messageStore;
            Cryptographer = cryptographer;
            KeyStore = keyStore;

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

            if (keyStore == null)
            {
                KeyStore = new KeyStore();
            }
        }

        private IContactStore ContactStore { get; set; }

        private IMessageStore MessageStore { get; set; }

        private IKeyStore KeyStore { get; set; }

        private ICryptographer Cryptographer { get; set; }


        public IEnumerable<IContactItem> GetContacts()
        {
            return ContactStore.GetAllContacts();
        }

        public MessageViewModel GetDecryptedMessage(Guid id)
        {
            var messageStoreItem = MessageStore.GetMessageStoreItem(id);
            var key = KeyStore.GetKeyPart(messageStoreItem.KeyName, messageStoreItem.KeyStartPos, messageStoreItem.Message.Length);

            return messageStoreItem.Decrypt(Cryptographer, key);
        }

        public IEnumerable<IMessageStoreItem> GetMessageStoreItemsByContactId(Guid contactId)
        {
            // JAG ÄR HÄR. Måste hämta nyckeln associerad med kontakten och dekryptera messageheadern för att hitta vilka meddelanden som hör till kontakten.
            var messages = MessageStore.GetMessagesByKeyName("");

            return messages;
        }
    }
}
