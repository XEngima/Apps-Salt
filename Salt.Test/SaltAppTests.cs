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
        public void EncryptedMessageInStore_GettingFromStore_DeliversDecryptedMessage()
        {
            // Arrange
            var cryptographer = new TestCryptographer();
            var hashGenerator = new TestHashGenerator();

            var messageStore = new TestMessageStore()
            {
                Messages = new List<IMessage>
                {
                    new Message(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"), "MyKey123", "redaeh", "jeH", "nasjeH", 0)
                }
            };


            var keyStore = new TestKeyStore(hashGenerator)
            {
                Items = new List<TestKeyStoreItem>
                {
                    new TestKeyStoreItem("MyKey", 0, 15, "backwardsbackwa")
                }
            };

            var saltApp = new SaltApp(null, messageStore, keyStore, hashGenerator, cryptographer);

            // Act
            var message = saltApp.GetDecryptedMessage(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"));

            // Assert
            Assert.AreEqual("header", message.Header);
            Assert.AreEqual("Hej", message.Subject);
            Assert.AreEqual("Hejsan", message.Content);
        }
    }
}
