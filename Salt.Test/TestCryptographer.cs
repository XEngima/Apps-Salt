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
        public string Decrypt(string text, string keyPart)
        {
            if (!string.IsNullOrEmpty(keyPart))
            {
                if (text == "redaeh")
                {
                    return "header";
                }

                if (text == "jeH")
                {
                    return "Hej";
                }

                if (text == "nasjeH")
                {
                    return "Hejsan";
                }
            }

            return "";
        }

        public string Encrypt(string message, string keyPart)
        {
            return "";
        }
    }
}
