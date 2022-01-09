using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Salt.Cypher
{
    public class KeyFileGenerator
    {
        public static Guid CreateNewKeyFile(string keyStoreFolder, int length)
        {
            Guid keyId = Guid.NewGuid();
            string fileName = keyId.ToString() + ".key";

            var writer = new StreamWriter(Path.Combine(keyStoreFolder, fileName));
            var randomGenerator = new TrueRandomGenerator();

            for (int i = 0; i < length; i++)
            {
                int charValue = randomGenerator.GetNextNumber(RealCryptographer.cCharValueCount - 1);
                writer.Write(RealCryptographer.ValueToChar(charValue));
            }

            writer.Close();

            return keyId;
        }
    }
}
