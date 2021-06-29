using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salt.Cypher;
using System;

namespace Salt.Test
{
    [TestClass]
    public class CryptographerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // A
            var cryptographer = new Cryptographer();

            // A
            string encryptedString = cryptographer.Encrypt("abc", "aaa");

            // Assert
            Assert.AreEqual("bde", encryptedString);
        }
    }
}
