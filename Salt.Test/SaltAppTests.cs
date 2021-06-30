using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Business;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class SaltAppTests
    {
        [TestMethod]
        public void UnencryptedText_Encrypting_EncryptionCorrect()
        {
            // Arrange
            var messageStore = new TestMessageStore();
            var cryptographer = new TestCryptographer();

            var saltApp = new SaltApp(null, messageStore, cryptographer);

            // Act
            var message = saltApp.GetDecryptedMessage(10);

            // Assert
            Assert.AreEqual("Hej", message.Subject);
            Assert.AreEqual("Hejsan", message.Content);
        }
    }
}
