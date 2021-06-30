using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class CryptographerTests
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

        [TestMethod]
        public void EncryptedText_Decrypting_DecryptionCorrect()
        {
            // Arrange
            var cryptographer = new Cryptographer();

            // Act
            string decryptedString = cryptographer.Decrypt("bcd", "aaa");

            // Assert
            Assert.AreEqual("abc", decryptedString);
        }
    }
}
