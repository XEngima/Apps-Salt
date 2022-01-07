using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class CryptographerTests
    {
        [TestMethod]
        public void SwedishAlphabet_EncryptingAndDecrypting_TextCorrectlyEncryptedAndDecrypted()
        {
            // Arrange
            var cryptographer = new RealCryptographer();

            string keyPart = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

            // Act
            string encryptedString = cryptographer.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ", keyPart);
            string decryptedString = cryptographer.Decrypt(encryptedString, keyPart);

            Assert.AreEqual("efghijklmnopqrstuvwxyz{|}~éèú", encryptedString);
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ", decryptedString);
        }

        [TestMethod]
        public void LowEndEdgeText_EncryptingAndDecrypting_TextCorrectlyEncryptedAndDecrypted()
        {
            // Arrange
            var cryptographer = new RealCryptographer();

            string keyPart = "\t";

            // Act
            string encryptedString = cryptographer.Encrypt("\t", keyPart);
            string decryptedString = cryptographer.Decrypt(encryptedString, keyPart);

            Assert.AreEqual("\t", encryptedString);
            Assert.AreEqual("\t", decryptedString);
        }

        [TestMethod]
        public void HighEndEdgeText_EncryptingAndDecrypting_TextCorrectlyEncryptedAndDecrypted()
        {
            // Arrange
            var cryptographer = new RealCryptographer();

            string keyPart = "ÿ";

            // Act
            string encryptedString = cryptographer.Encrypt("ÿ", keyPart);
            string decryptedString = cryptographer.Decrypt(encryptedString, keyPart);

            Assert.AreEqual("þ", encryptedString);
            Assert.AreEqual("ÿ", decryptedString);
        }
    }
}
