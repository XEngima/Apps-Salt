using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface ICryptographer
    {
        string Encrypt(string text, string keyPart);

        string Decrypt(string text, string keyPart);
    }
}
