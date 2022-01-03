using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class TestCryptographerTests
    {
        [TestMethod]
        public void UnencryptedText_Encrypting_EncryptionCorrect()
        {
            // Arrange
            var cryptographer = new TestCryptographer();

            // Act
            string encryptedString = cryptographer.Encrypt("abcd", "aaaa");

            // Assert
            Assert.AreEqual("ABCD", encryptedString);
        }
    }
}
