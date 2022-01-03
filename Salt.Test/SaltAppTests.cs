using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Business;
using Salt.Messages;
using Salt.Contacts;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Salt.Keys;

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

            TobiasContactId = Guid.Parse("00000001-f760-4cf6-a84d-526397dc8b2a");
            DanielContactId = Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a");
            SamuelContactId = Guid.Parse("00000003-f760-4cf6-a84d-526397dc8b2a");

            ContactStore = new TestContactStore()
            {
                Contacts = new List<IContactStoreItem>
                {
                    new ContactItem(TobiasContactId, "Tobias", "DanielTobiasKey"),
                    new ContactItem(SamuelContactId, "Samuel", "DanielSamuelKey"),
                }
            };

            // Key Store

            KeyStore = new TestKeyStore();
            KeyStore.Add(new TestKeyStoreItem("DanielTobiasKey", 0, 200, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
            KeyStore.Add(new TestKeyStoreItem("DanielSamuelKey", 0, 200, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));

            // Create messages in the test message store

            var messageStoreItems = new List<IMessageStoreItem>();

            MessageTobiasToDanielId = Guid.Parse("10000001-9575-4f71-ba28-cf09c5fdf200");
            MessageSamuelToDanielId = Guid.Parse("10000002-9575-4f71-ba28-cf09c5fdf200");
            MessageDanielToSamuelId = Guid.Parse("10000003-9575-4f71-ba28-cf09c5fdf200");

            // Message from Tobias to Daniel

            var header = new ItemHeader
            {
                Date = new DateTime(2021, 01, 01, 12, 00, 00),
                Sender = TobiasContactId,
                Recipient = DanielContactId,
            };

            string subject = "A SIGN IN THE STARS";
            string message = "THAT'S CORRECT! THIS IS AWESOME!";

            messageStoreItems.Add(new MessageStoreItem(MessageTobiasToDanielId, "DanielTobiasKey", 0, JsonConvert.SerializeObject(header), subject, message));

            // Message from Samuel to Daniel

            header = new ItemHeader
            {
                Date = new DateTime(2021, 01, 02, 12, 00, 00),
                Sender = SamuelContactId,
                Recipient = DanielContactId,
            };

            subject = "LIN WOOD?";
            message = "IS HE CORRUPT?";

            messageStoreItems.Add(new MessageStoreItem(MessageSamuelToDanielId, "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), subject, message));

            // Message from Daniel to Samuel

            header = new ItemHeader
            {
                Date = new DateTime(2021, 01, 03, 12, 00, 00),
                Sender = DanielContactId,
                Recipient = SamuelContactId,
            };

            subject = "RE: LIN WOOD?";
            message = "NO, HE IS NOT CORRUPT!";

            messageStoreItems.Add(new MessageStoreItem(MessageDanielToSamuelId, "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), subject, message));

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
            var message = saltApp.GetDecryptedMessage(MessageTobiasToDanielId);

            // Assert
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
        public void EncryptedMessagesInStore_GetMessageHeadersByAnyContactId_CorrectMessagesReturned()
        {
            // Arrange
            var saltApp = new SaltApp(ContactStore, MessageStore, KeyStore, Cryptographer);

            // Act
            var messageHeaders = saltApp.GetDecryptedMessageHeadersByAnyContactId(TobiasContactId);

            // Assert
            Assert.AreEqual(1, messageHeaders.Count());

            var header = messageHeaders.First();
            Assert.AreEqual("Tobias", header.SenderName);
            Assert.AreEqual("a sign in the stars", header.Subject);
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

        [TestMethod]
        public void SomeMessagesInStore_SendingMessage_MessageSent()
        {
            var saltApp = new SaltApp(ContactStore, MessageStore, KeyStore, Cryptographer);

            // Act
            saltApp.SendMessage(SamuelContactId, "about lin wood", "he's actually a real hero!", "DanielSamuelKey");

            // Assert
            Assert.AreEqual(4, MessageStore.MessageStoreItems.Count());
        }
    }
}
