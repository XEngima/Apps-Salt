using Salt.Cypher;
using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Messages;
using Newtonsoft.Json;
using System.Linq;

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

        public IEnumerable<SaltMessageHeader> GetDecryptedMessageHeadersByAnyContactId(Guid contactId)
        {
            var keyItems = KeyStore.Items;
            var contactStoreItems = ContactStore.GetAllContacts();

            var messageHeaders = new List<SaltMessageHeader>();

            foreach (var keyItem in keyItems)
            {
                var headerItems = MessageStore.GetMessageHeadersByKeyName(keyItem.KeyName);

                foreach (var headerItem in headerItems)
                {
                    string encryptedSubject = MessageStore.GetSubjectByMessageId(headerItem.MessageId);
                    string keyPart = keyItem.Key.Substring(headerItem.KeyStartPos, headerItem.Content.Length + encryptedSubject.Length);

                    // Decrypt header
                    string jsonHeader = Cryptographer.Decrypt(headerItem.Content, keyPart.Substring(0, headerItem.Content.Length));
                    ItemHeader header = JsonConvert.DeserializeObject<ItemHeader>(jsonHeader);

                    // Decrypt subject
                    string subject = Cryptographer.Decrypt(encryptedSubject, keyPart.Substring(headerItem.Content.Length));

                    if (header.Recipient == contactId || header.Sender == contactId)
                    {
                        var senderContactItem = contactStoreItems.FirstOrDefault(c => c.Id == header.Sender);
                        var recipientContactItem = contactStoreItems.FirstOrDefault(c => c.Id == header.Recipient);

                        var saltMessageHeader = new SaltMessageHeader(headerItem.MessageId, header.Date, header.Sender, senderContactItem?.Name, header.Recipient, recipientContactItem?.Name, subject);

                        messageHeaders.Add(saltMessageHeader);
                    }
                }
            }

            return messageHeaders;
        }

        public IEnumerable<SaltMessageHeader> GetDecryptedMessageHeadersByRecipientId(Guid recipientId)
        {
            var keyItems = KeyStore.Items;

            var messageHeaders = new List<SaltMessageHeader>();

            foreach (var keyItem in keyItems)
            {
                var headerItems = MessageStore.GetMessageHeadersByKeyName(keyItem.KeyName);

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
            var keyItems = KeyStore.Items;

            var returnItems = new List<IMessageStoreItem>();

            foreach (var keyItem in keyItems)
            {
                var messageStoreItems = MessageStore.GetMessageStoreItemsByKeyName(keyItem.KeyName);

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
