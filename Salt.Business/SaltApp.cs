using Salt.Interfaces;
using Salt.Cypher;
using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Messages;

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
                KeyStore = new FakeKeyStore();
            }
        }

        private IContactStore ContactStore { get; set; }

        private IMessageStore MessageStore { get; set; }

        private IKeyStore KeyStore { get; set; }

        private ICryptographer Cryptographer { get; set; }


        public IEnumerable<IContactStoreItem> GetContacts()
        {
            return ContactStore.GetAllContacts();
        }

        public MessageViewModel GetDecryptedMessage(Guid id)
        {
            var messageStoreItem = MessageStore.GetMessageStoreItem(id);
            var key = KeyStore.GetKeyPart(messageStoreItem.KeyName, messageStoreItem.KeyStartPos, messageStoreItem.Message.Length);

            return messageStoreItem.Decrypt(Cryptographer, key);
        }

        private IMessageHeader EncryptMessageHeader(IMessageHeaderItem messageHeaderItem)
        {
            return new MessageHeader();
        }

        public IEnumerable<IMessageHeader> GetMessageHeadersByContactId(Guid contactId)
        {
            var keyNames = KeyStore.GetAllKeyNames();

            var messageHeaders = new List<IMessageHeader>();

            foreach (var keyName in keyNames)
            {
                var headerItems = MessageStore.GetMessageHeadersByKeyName(keyName);

                foreach (var headerItem in headerItems)
                {
                    messageHeaders.Add(EncryptMessageHeader(headerItem));
                }
            }

            return messageHeaders;
        }

        public IEnumerable<IMessageStoreItem> GetMessageStoreItemsByContactId(Guid contactId)
        {
            var keyNames = KeyStore.GetAllKeyNames();

            var messageStoreItems = new List<IMessageStoreItem>();

            foreach (var keyName in keyNames)
            {
                var messages = MessageStore.GetMessageStoreItemsByKeyName(keyName);
                messageStoreItems.AddRange(messages);
            }

            return messageStoreItems;
        }
    }
}
