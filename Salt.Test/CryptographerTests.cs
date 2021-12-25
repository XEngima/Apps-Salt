using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class CryptographerTests
    {
        [TestMethod]
        public void NormalText_Encrypting_TextCorrectlyEncrypted()
        {
            // Arrange
            var cryptographer = new CaseCryptographer();

            // Act
            string encryptedString = cryptographer.Encrypt("abc", "case");

            // Assert
            Assert.AreEqual("ABC", encryptedString);
        }

        [TestMethod]
        public void EncryptedText_Decrypting_TextCorrectlyDecrypted()
        {
            // Arrange
            var cryptographer = new CaseCryptographer();

            // Act
            string decryptedString = cryptographer.Decrypt("ABC", "case");

            // Assert
            Assert.AreEqual("abc", decryptedString);
        }
    }
}
