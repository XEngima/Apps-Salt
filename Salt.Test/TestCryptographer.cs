using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salt.Cypher;

namespace Salt.Test
{
    public class TestCryptographer : ICryptographer
    {
        /// <summary>
        /// Encrypts a text using a key - but very weak, since it is for test only.
        /// </summary>
        /// <param name="text">The text to be encrypted.</param>
        /// <param name="key">The key - values "case" or "brackets" are possible values.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string text, string key)
        {
            if (text.Length != key.Length)
            {
                throw new ArgumentException("The text being encrypted need to have a key as long as the text.");
            }

            var sbText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (key[i] == 'a')
                {
                    sbText.Append(text[i].ToString().ToUpper());
                }
                else
                {
                    sbText.Append("*");
                }
            }

            return sbText.ToString();
        }

        /// <summary>
        /// Decrypts a text using a key - for test only.
        /// </summary>
        /// <param name="text">The text to be decrypted.</param>
        /// <param name="key">The key - values "case" or "brackets" are possible values.</param>
        /// <returns>The decrypted text.</returns>
        public string Decrypt(string text, string key)
        {
            var sbText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (key[i] == 'a')
                {
                    sbText.Append(text[i].ToString().ToLower());
                }
                else
                {
                    sbText.Append("*");
                }
            }

            return sbText.ToString();
        }
    }
}
