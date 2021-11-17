using Salt.Interfaces;
using Salt.Cypher;
using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class SaltApp : ISaltApp
    {
        public SaltApp(IContactStore contactStore = null, IMessageStore messageStore = null, IKeyStore keyStore = null, IHashGenerator hashGenerator = null, ICryptographer cryptographer = null)
        {
            ContactStore = contactStore;
            MessageStore = messageStore;
            Cryptographer = cryptographer;
            HashGenerator = hashGenerator;
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

        private IHashGenerator HashGenerator { get; set; }

        private ICryptographer Cryptographer { get; set; }


        public IEnumerable<IContactItem> GetContacts()
        {
            return ContactStore.GetAllContacts();
        }

        public IMessage GetDecryptedMessage(Guid id)
        {
            var message = MessageStore.GetMessage(id);
            var key = KeyStore.GetKeyPart(message.KeyName, message.KeyStartPos, message.Header.Length + message.Subject.Length + message.Content.Length);

            return message.Decrypt(Cryptographer, key);
        }

        public IEnumerable<IMessage> GetMessages(Guid contactId)
        {
            // JAG ÄR HÄR. Måste hämta nyckeln associerad med kontakten och dekryptera messageheadern för att hitta vilka meddelanden som hör till kontakten.
            var messages = MessageStore.GetMessagesByKeyName("");

            return messages;
        }
    }
}
