using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Business;
using Salt.Messages;
using Salt.Contacts;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Salt.Test
{
    [TestClass]
    public class SaltAppTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Cryptographer = new TestCryptographer();

            // Contact Store

            ContactStore = new TestContactStore()
            {
                Contacts = new List<IContactStoreItem>
                {
                    new ContactItem
                    {
                        Id = TobiasContactId,
                        Name = "Tobias",
                        KeyName = "DanielTobiasKey"
                    },
                    new ContactItem
                    {
                        Id = SamuelContactId,
                        Name = "Samuel",
                        KeyName = "DanielSamuelKey"
                    },
                }
            };

            // Key Store

            KeyStore = new TestKeyStore()
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("DanielTobiasKey", 0, 4, "case"),
                    new TestKeyStoreItem("DanielSamuelKey", 0, 4, "case"),
                }
            };

            // Create messages in the test message store

            TobiasContactId = Guid.Parse("00000001-f760-4cf6-a84d-526397dc8b2a");
            DanielContactId = Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a");
            SamuelContactId = Guid.Parse("00000003-f760-4cf6-a84d-526397dc8b2a");

            var messageStoreItems = new List<IMessageStoreItem>();

            MessageTobiasToDanielId = Guid.Parse("10000001-9575-4f71-ba28-cf09c5fdf200");
            MessageSamuelToDanielId = Guid.Parse("10000002-9575-4f71-ba28-cf09c5fdf200");
            MessageDanielToSamuelId = Guid.Parse("10000003-9575-4f71-ba28-cf09c5fdf200");

            // Message from Tobias to Daniel

            var header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 01, 12, 00, 00),
                Sender = TobiasContactId,
                Recipients = new List<Guid>() { DanielContactId }
            };

            var message = new Message
            {
                Subject = "A SIGN IN THE STARS",
                Content = "THAT'S CORRECT! THIS IS AWESOME!"
            };

            messageStoreItems.Add(new MessageStoreItem(MessageTobiasToDanielId, "DanielTobiasKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            // Message from Samuel to Daniel

            header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 02, 12, 00, 00),
                Sender = SamuelContactId,
                Recipients = new List<Guid>() { DanielContactId }
            };

            message = new Message
            {
                Subject = "LIN WOOD?",
                Content = "IS HE CORRUPT?"
            };

            messageStoreItems.Add(new MessageStoreItem(MessageSamuelToDanielId, "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            // Message from Daniel to Samuel

            header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 03, 12, 00, 00),
                Sender = DanielContactId,
                Recipients = new List<Guid>() { SamuelContactId }
            };

            message = new Message
            {
                Subject = "RE: LIN WOOD?",
                Content = "NO, HE IS NOT CORRUPT!"
            };

            messageStoreItems.Add(new MessageStoreItem(MessageDanielToSamuelId, "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            MessageStore = new MemoryMessageStore
            {
                MessageStoreItems = messageStoreItems
            };
        }

        private TestCryptographer Cryptographer { get; set; }
        private MemoryMessageStore MessageStore { get; set; }
        private TestKeyStore KeyStore { get; set; }
        private TestContactStore ContactStore { get; set; }

        private Guid TobiasContactId { get; set; }
        private Guid DanielContactId { get; set; }
        private Guid SamuelContactId { get; set; }

        private Guid MessageTobiasToDanielId { get; set; }
        private Guid MessageSamuelToDanielId { get; set; }
        private Guid MessageDanielToSamuelId { get; set; }

        [TestMethod]
        public void EncryptedMessageInStore_GetMessageFromStore_GotDecryptedMessage()
        {
            // Arrange
            var saltApp = new SaltApp(null, MessageStore, KeyStore, Cryptographer);

            // Act
            var jsonMessage = saltApp.GetDecryptedMessage(MessageTobiasToDanielId);

            var message = JsonConvert.DeserializeObject<Message>(jsonMessage.Content);

            // Assert
            Assert.AreEqual("a sign in the stars", message.Subject);
            Assert.AreEqual("that's correct! this is awesome!", message.Content);
        }

        [TestMethod]
        public void EncryptedMessagesInStore_GetMessageHeadersByRecipientId_CorrectMessagesReturned()
        {
            // Arrange
            var saltApp = new SaltApp(ContactStore, MessageStore, KeyStore, Cryptographer);

            // Act
            var messageHeader = saltApp.GetDecryptedMessageHeadersByRecipientId(DanielContactId);

            // Assert
            Assert.AreEqual(2, messageHeader.Count());
        }

        [TestMethod]
        public void EncryptedMessagesInStore_GetMessageStoreItemsByRecipientId_CorrectMessagesReturned()
        {
            var saltApp = new SaltApp(ContactStore, MessageStore, KeyStore, Cryptographer);

            // Act
            var messageStoreItems = saltApp.GetDecryptedMessageStoreItemsByRecipientId(DanielContactId);

            // Assert
            Assert.AreEqual(2, messageStoreItems.Count());
        }
    }
}
