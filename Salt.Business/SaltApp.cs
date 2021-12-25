using Salt.Cypher;
using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Messages;
using Newtonsoft.Json;

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
                ContactStore = Factory.CreateMemoryContactStore();
            }

            if (messageStore == null)
            {
                MessageStore = Factory.CreateMemoryMessageStore();
            }

            if (cryptographer == null)
            {
                Cryptographer = new CaseCryptographer();
            }

            if (keyStore == null)
            {
                KeyStore = Factory.CreateLetterKeyStore();
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

        public IEnumerable<SaltMessageHeader> GetDecryptedMessageHeadersByRecipientId(Guid recipientId)
        {
            var keyNames = KeyStore.GetAllKeyNames();

            var messageHeaders = new List<SaltMessageHeader>();

            foreach (var keyName in keyNames)
            {
                var headerItems = MessageStore.GetMessageHeadersByKeyName(keyName);

                foreach (var headerItem in headerItems)
                {
                    var header = JsonConvert.DeserializeObject<ItemHeader>(headerItem.Content);

                    if (header.Recipient == recipientId)
                    {
                        messageHeaders.Add(new SaltMessageHeader());
                    }
                }
            }

            return messageHeaders;
        }

        public IEnumerable<IMessageStoreItem> GetDecryptedMessageStoreItemsByRecipientId(Guid recipientId)
        {
            var keyNames = KeyStore.GetAllKeyNames();

            var returnItems = new List<IMessageStoreItem>();

            foreach (var keyName in keyNames)
            {
                var messageStoreItems = MessageStore.GetMessageStoreItemsByKeyName(keyName);

                foreach (var messageStoreItem in messageStoreItems)
                {
                    var headerItem = messageStoreItem.Header;
                    var header = JsonConvert.DeserializeObject<ItemHeader>(headerItem);

                    if (header.Recipient == recipientId)
                    {
                        returnItems.Add(messageStoreItem);
                    }
                }

            }

            return returnItems;
        }
    }
}
