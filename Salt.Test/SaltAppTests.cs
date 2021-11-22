using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Business;
using Salt.Cypher;
using Salt.Interfaces;
using Salt.Messages;
using System;
using System.Collections.Generic;

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
                Messages = new List<IMessageStoreItem>
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
    }
}
