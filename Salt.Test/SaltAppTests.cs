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

            // Create messages in the test message store

            TobiasContactId = Guid.Parse("00000001-f760-4cf6-a84d-526397dc8b2a");
            DanielContactId = Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a");
            SamuelContactId = Guid.Parse("00000003-f760-4cf6-a84d-526397dc8b2a");

            var messageStoreItems = new List<IMessageStoreItem>();

            var header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 01, 12, 00, 00),
                Sender = TobiasContactId,
                Recipients = new List<Guid>() { DanielContactId }
            };

            var message = new Message
            {
                Subject = "A sign in the stars",
                Content = "That's correct! This is awesome!"
            };

            messageStoreItems.Add(new MessageStoreItem(Guid.Parse("10000001-9575-4f71-ba28-cf09c5fdf200"), "DanielTobiasKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 02, 12, 00, 00),
                Sender = SamuelContactId,
                Recipients = new List<Guid>() { DanielContactId }
            };

            message = new Message
            {
                Subject = "Lin Wood?",
                Content = "Is he corrupt?"
            };

            messageStoreItems.Add(new MessageStoreItem(Guid.Parse("10000002-9575-4f71-ba28-cf09c5fdf200"), "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 03, 12, 00, 00),
                Sender = DanielContactId,
                Recipients = new List<Guid>() { SamuelContactId }
            };

            message = new Message
            {
                Subject = "Re: Lin Wood?",
                Content = "No, he's not corrupt!"
            };

            messageStoreItems.Add(new MessageStoreItem(Guid.Parse("10000003-9575-4f71-ba28-cf09c5fdf200"), "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            MessageStore = new TestMessageStore
            {
                MessageStoreItems = messageStoreItems
            };
        }

        private TestCryptographer Cryptographer { get; set; }
        private TestMessageStore MessageStore { get; set; }

        private Guid TobiasContactId { get; set; }
        private Guid DanielContactId { get; set; }
        private Guid SamuelContactId { get; set; }

        [TestMethod]
        public void EncryptedMessageInStore_GetMessageFromStore_GotDecryptedMessage()
        {
            // Arrange
            var keyStore = new TestKeyStore()
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("MyKey123", 0, 4, "case")
                }
            };

            var saltApp = new SaltApp(null, MessageStore, keyStore, Cryptographer);

            // Act
            var message = saltApp.GetDecryptedMessage(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"));

            // Assert
            Assert.AreEqual("abcd", message.Content);
        }

        [TestMethod]
        public void EncryptedMessagesInStore_GetMessageHeadersByContactId_CorrectMessagesReturned()
        {
            // Arrange
            var contactStore = new TestContactStore()
            {
                Contacts = new List<IContactStoreItem>
                {
                    new ContactItem
                    {
                        Id = TobiasContactId,
                        Name = "Tobias",
                        KeyName = "MyKey123"
                    }
                }
            };

            var keyStore = new TestKeyStore()
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("MyKey123", 0, 4, "case")
                }
            };

            var saltApp = new SaltApp(contactStore, MessageStore, keyStore, Cryptographer);

            // Act
            var messageHeader = saltApp.GetMessageHeadersByContactId(TobiasContactId);

            // Assert
            Assert.AreEqual(1, messageHeader.Count());
        }

        [TestMethod]
        public void EncryptedMessagesInStore_GetMessageStoreItemsByContactId_CorrectMessagesReturned()
        {
            // Arrange
            var contactStore = new TestContactStore()
            {
                Contacts = new List<IContactStoreItem>
                {
                    new ContactItem
                    {
                        Id = TobiasContactId,
                        Name = "Tobias",
                        KeyName = "MyKey123"
                    }
                }
            };

            var keyStore = new TestKeyStore()
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("MyKey123", 0, 4, "case")
                }
            };

            var saltApp = new SaltApp(contactStore, MessageStore, keyStore, Cryptographer);

            // Act
            var messageStoreItems = saltApp.GetMessageStoreItemsByContactId(TobiasContactId);

            // Assert
            Assert.AreEqual(1, messageStoreItems.Count());
        }
    }
}
