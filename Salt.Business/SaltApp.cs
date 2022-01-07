using Salt.Cypher;
using Salt.Keys;
using System;
using System.Collections.Generic;
using System.Text;
using Salt.Contacts;
using Salt.Messages;
using Newtonsoft.Json;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Salt.Business
{
    public class SaltApp : ISaltApp
    {
        public SaltApp(ISettings settings, IContactStore contactStore = null, IMessageStore messageStore = null, IKeyStore keyStore = null, ICryptographer cryptographer = null)
        {
            Settings = settings;
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

        private ISettings Settings { get; set; }

        private IContactStore ContactStore { get; set; }

        private IMessageStore MessageStore { get; set; }

        private IKeyStore KeyStore { get; set; }

        private ICryptographer Cryptographer { get; set; }


        public IEnumerable<IContactStoreItem> GetContacts()
        {
            return ContactStore.GetAllContacts();
        }

        public SaltMessage GetMessage(Guid id)
        {
            var messageStoreItem = MessageStore.GetMessageStoreItem(id);
            var keyPart = KeyStore.GetKeyPart(messageStoreItem.KeyName, messageStoreItem.KeyStartPos, messageStoreItem.KeyLength);

            string jsonHeader = Cryptographer.Decrypt(messageStoreItem.Header, keyPart);
            ItemHeader header = JsonConvert.DeserializeObject<ItemHeader>(jsonHeader);
            string subject = Cryptographer.Decrypt(messageStoreItem.Subject, keyPart.Substring(jsonHeader.Length));
            string message = Cryptographer.Decrypt(messageStoreItem.Message, keyPart.Substring(jsonHeader.Length + subject.Length));

            return new SaltMessage(messageStoreItem.Id, header.Date, header.Sender, header.Recipient, subject, message);
        }

        public IEnumerable<SaltMessageHeader> GetMessageHeadersByAnyContactId(Guid contactId)
        {
            var keyNames = KeyStore.GetAllKeyNames();
            var contactStoreItems = ContactStore.GetAllContacts();

            var messageHeaders = new List<SaltMessageHeader>();

            foreach (var keyName in keyNames)
            {
                var headerItems = MessageStore.GetMessageHeadersByKeyName(keyName);

                foreach (var headerItem in headerItems)
                {
                    string encryptedSubject = MessageStore.GetSubjectByMessageId(headerItem.MessageId);
                    string keyPart = KeyStore.GetKeyPart(keyName, headerItem.KeyStartPos, headerItem.KeyLength);

                    // Decrypt header
                    string jsonHeader = Cryptographer.Decrypt(headerItem.Content, keyPart);
                    ItemHeader header = JsonConvert.DeserializeObject<ItemHeader>(jsonHeader);

                    // Decrypt subject
                    string subject = Cryptographer.Decrypt(encryptedSubject, keyPart.Substring(jsonHeader.Length));

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

        public IEnumerable<SaltMessageHeader> GetMessageHeadersByRecipientId(Guid recipientId)
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

        public IEnumerable<IMessageStoreItem> GetMessageStoreItemsByRecipientId(Guid recipientId)
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

        public void SendMessage(Guid recipient, string subject, string message, string keyName)
        {
            // Find next key position

            int keyPos = MessageStore.FindNextKeyPos(keyName);

            // Create the message store item

            var itemHeader = new ItemHeader(DateTime.Now, Settings.MyContactId, recipient);

            // Encrypt it

            string header = itemHeader.ToJson();

            if (!KeyStore.KeyExists(keyName))
            {
                throw new MessagingException("The key '" + keyName + "' does not exist in the key store.");
            }

            string keyPart;

            try
            {
                keyPart = KeyStore.GetKeyPart(keyName, keyPos, header.Length + subject.Length + message.Length);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new MessagingException("The key '" + keyName + "' is exceeded.");
            }

            string encryptedHeader = Cryptographer.Encrypt(header, keyPart.Substring(0, header.Length));
            //string decryptedHeader = Cryptographer.Decrypt(encryptedHeader, keyPart.Substring(0));

            string encryptedSubject = Cryptographer.Encrypt(subject, keyPart.Substring(header.Length, subject.Length));
            string encryptedMessage = Cryptographer.Encrypt(message, keyPart.Substring(header.Length + subject.Length, message.Length));

            // Send it

            MessageStoreItem item = new MessageStoreItem(Guid.NewGuid(), keyName, keyPos, keyPart.Length, encryptedHeader, encryptedSubject, encryptedMessage);
            MessageStore.SendMessage(item);
        }
    }
}
