using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Business;
using Salt.Cypher;
using Salt.Interfaces;
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
        [TestMethod]
        public void EncryptedMessageInStore_GetMessageFromStore_GotDecryptedMessage()
        {
            // Arrange
            var cryptographer = new TestCryptographer();

            var messageStore = new TestMessageStore()
            {
                MessageStoreItems = new List<IMessageStoreItem>
                {
                    new MessageStoreItem(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"), "MyKey123", 0, "ABCD")
                }
            };

            var keyStore = new TestKeyStore()
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("MyKey123", 0, 4, "case")
                }
            };

            var saltApp = new SaltApp(null, messageStore, keyStore, cryptographer);

            // Act
            var message = saltApp.GetDecryptedMessage(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"));

            // Assert
            Assert.AreEqual("abcd", message.Content);
        }

        [TestMethod]
        public void EncryptedMessagesInStore_GetMessageStoreItemsByContactId_CorrectMessagesReturned()
        {
            // Arrange
            var cryptographer = new TestCryptographer();

            Guid tobiasContactId = Guid.Parse("00000001-f760-4cf6-a84d-526397dc8b2a");

            var contactStore = new TestContactStore()
            {
                Contacts = new List<IContactStoreItem>
                {
                    new ContactItem
                    {
                        Id = tobiasContactId,
                        Name = "Tobias",
                        KeyName = "MyKey123"
                    }
                }
            };

            var message = new Message
            {
                Date = DateTime.Now,
                Sender = tobiasContactId, // Tobias
                Recievers = "",
                Subject = "Stjärntecknet",
                Content = "Det stämmer! Det är mäktigt detta!"
            };

            var messageStore = new TestMessageStore()
            {
                MessageStoreItems = new List<IMessageStoreItem>
                {
                    new MessageStoreItem(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"), "MyKey123", 0, JsonConvert.SerializeObject(message))
                }
            };

            var keyStore = new TestKeyStore()
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("MyKey123", 0, 4, "case")
                }
            };

            var saltApp = new SaltApp(contactStore, messageStore, keyStore, cryptographer);

            // Act
            var messageStoreItems = saltApp.GetMessageStoreItemsByContactId(tobiasContactId);

            // Assert
            Assert.AreEqual(1, messageStoreItems.Count());
        }
    }
}
