using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class KeyStoreTests
    {
        [TestMethod]
        public void UnencryptedText_Encrypting_EncryptionCorrect()
        {
            // Arrange
            var cryptographer = new Cryptographer();

            // Act
            string encryptedString = cryptographer.Encrypt("abc", "aaa");

            // Assert
            Assert.AreEqual("bcd", encryptedString);
        }
    }
}
