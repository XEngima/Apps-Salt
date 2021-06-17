using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface ICryptographer
    {
        string Encrypt(string message, string keyPart);

        string Decrypt(string message, string keyPart);
    }
}
