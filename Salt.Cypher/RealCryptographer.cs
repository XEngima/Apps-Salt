using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Salt.Cypher
{
    public class RealCryptographer : ICryptographer
    {
        public const int cCharValueCount = 221;

        public static int CharToValue(char ch)
        {
            if (ch >= 9 && ch <= 10)
                return ch - 9; // 0-1

            if (ch == 13)
                return 2; // 2

            if (ch >= 32 && ch <= 126)
                return ch - 29; // 3-97

            if (ch == 128)
                return ch - 30; // 98

            if (ch >= 130 && ch <= 140)
                return ch - 31; // 99-109

            if (ch == 142)
                return ch - 32; // 110

            if (ch >= 145 && ch <= 156)
                return ch - 34; // 111-122

            if (ch >= 158 && ch <= 255)
                return ch - 35; // 123-220

            throw new NotSupportedException("The character '" + ch + "'(" + (int)ch + ") is not supported.");
        }

        public static char ValueToChar(int value)
        {
            if (value <= 1)
                return Convert.ToChar(value + 9);

            if (value == 2)
                return Convert.ToChar(13);

            if (value <= 97)
                return Convert.ToChar(value + 29);

            if (value == 98)
                return Convert.ToChar(128);

            if (value <= 109)
                return Convert.ToChar(value + 31);

            if (value == 110)
                return Convert.ToChar(142);

            if (value <= 122)
                return Convert.ToChar(value + 34);

            if (value <= 220)
                return Convert.ToChar(value + 35);

            throw new NotSupportedException("The value " + value + " cannot be mapped to a character.");
        }

        public string Encrypt(string text, string keyPart)
        {
            // 1. Perform Core Encryption

            var sbEncryptedText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                int textValue = CharToValue(text[i]);
                int keyValue = CharToValue(keyPart[i]);
                int newValue = textValue + keyValue;

                if (newValue >= cCharValueCount)
                {
                    newValue -= cCharValueCount;
                }

                char newChar = ValueToChar(newValue);
                sbEncryptedText.Append(newChar);
            }

            // 2. Escape the escape character

            string textMadeToString = sbEncryptedText.ToString();
            string escapedEncryptedText = textMadeToString.Replace(@"\", @"\\");

            // 3. Escape invisible characters

            string fullyEncryptedText = escapedEncryptedText.Replace(Convert.ToChar(11).ToString(), @"\t").Replace(Convert.ToChar(10).ToString(), @"\n").Replace(Convert.ToChar(13).ToString(), @"\r").Replace(" ", @"\s").Replace(Convert.ToChar(160).ToString(), @"\b");

            return fullyEncryptedText;
        }

        public string Decrypt(string text, string keyPart)
        {
            // 1. Unescape invisible characters

            string encryptedTextWithEscapedEscapeChar = text.Replace(@"\t", Convert.ToChar(11).ToString()).Replace(@"\n", Convert.ToChar(10).ToString()).Replace(@"\r", Convert.ToChar(13).ToString()).Replace(@"\s", " ").Replace(@"\b", Convert.ToChar(160).ToString());

            // 2. Unescape the escape character

            string encryptedText = encryptedTextWithEscapedEscapeChar.ToString().Replace(@"\\", @"\");

            // 1. Perform Core Decryption

            var sbDecryptedText = new StringBuilder();

            for (int i = 0; i < encryptedText.Length; i++)
            {
                int textValue = CharToValue(encryptedText[i]);
                int keyValue = CharToValue(keyPart[i]);
                int newValue = textValue - keyValue;

                if (newValue < 0)
                {
                    newValue += cCharValueCount;
                }

                char newChar = ValueToChar(newValue);
                sbDecryptedText.Append(newChar);
            }

            return sbDecryptedText.ToString();
        }
    }
}
