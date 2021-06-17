using Salt.Interfaces;
using System;

namespace Salt.Cypher
{
    public class Cryptographer : ICryptographer
    {
        public string Decrypt(string message, string keyPart)
        {
            return message;
        }

        public string Encrypt(string message, string keyPart)
        {
            return message;
        }
    }
}
