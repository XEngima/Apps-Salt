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
            var cryptographer = new TestCryptographer();

            // Act
            string encryptedString = cryptographer.Encrypt("abc", "case");

            // Assert
            Assert.AreEqual("ABC", encryptedString);
        }
    }
}
