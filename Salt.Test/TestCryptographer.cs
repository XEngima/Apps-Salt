using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salt.Cypher;
using Salt.Interfaces;

namespace Salt.Test
{
    public class TestCryptographer : ICryptographer
    {
        public string Decrypt(string message, string keyPart)
        {
            if (message == "jeH")
            {
                return "Hej";
            }

            if (message == "nasjeH")
            {
                return "Hejsan";
            }

            return "";
        }

        public string Encrypt(string message, string keyPart)
        {
            return "";
        }
    }
}
