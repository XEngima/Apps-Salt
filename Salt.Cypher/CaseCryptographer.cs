using System;

namespace Salt.Cypher
{
    public class CaseCryptographer : ICryptographer
    {
        /// <summary>
        /// Encrypts a text using a key - but very weak, since it is for test only.
        /// </summary>
        /// <param name="text">The text to be encrypted.</param>
        /// <param name="key">The key - values "case" or "brackets" are possible values.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string text, string key)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(key))
            {
                return text.ToUpper();
            }

            return "";
        }

        /// <summary>
        /// Decrypts a text using a key - for test only.
        /// </summary>
        /// <param name="text">The text to be decrypted.</param>
        /// <param name="key">The key - values "case" or "brackets" are possible values.</param>
        /// <returns>The decrypted text.</returns>
        public string Decrypt(string text, string key)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(key))
            {
                return text.ToLower();
            }

            return "";
        }
    }
}
