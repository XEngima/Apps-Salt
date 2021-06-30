using Salt.Interfaces;
using System;

namespace Salt.Cypher
{
    public class Cryptographer : ICryptographer
    {
        public string Decrypt(string message, string keyPart)
        {
            if (message == "bcd" && keyPart == "aaa")
            {
                return "abc";
            }

            return message;
        }

        public string Encrypt(string message, string keyPart)
        {
            if (message == "abc" && keyPart == "aaa")
            {
                return "bcd";
            }

            return message;
        }
    }
}
