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
            var cryptographer = new RealCryptographer();

            // Act
            string encryptedString = cryptographer.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            Assert.AreEqual("¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾FEW", encryptedString);
        }

        [TestMethod]
        public void NormalText_Encrypting_TextCorrectlyEncrypted2()
        {
            // Arrange
            var cryptographer = new RealCryptographer();

            // Act
            string encryptedString = cryptographer.Encrypt("Hej älskling!", "fafkjsdfuiglh");

            Assert.AreEqual("Ñéó®\u0093\\rúô!õøö¬", encryptedString);
        }

        [TestMethod]
        public void EncryptedText_Decrypting_TextCorrectlyDecrypted()
        {
            // Arrange
            var cryptographer = new RealCryptographer();

            // Act
            string decryptedString = cryptographer.Decrypt("¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾FEW", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            // Assert
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ", decryptedString);
        }

        [TestMethod]
        public void EncryptedText_Decrypting_TextCorrectlyDecrypted2()
        {
            // Arrange
            var cryptographer = new RealCryptographer();

            // Act
            string decryptedString = cryptographer.Decrypt("Ñéó®\u0093\\rúô!õøö¬", "fafkjsdfuiglh");

            // Assert
            Assert.AreEqual("Hej älskling!", decryptedString);
        }
    }
}
