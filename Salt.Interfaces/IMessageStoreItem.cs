using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageStoreItem
    {
        Guid Id { get; set; }

        int CryptoVersion { get; set; }

        string KeyName { get; set; }

        int KeyStartPos { get; set; }

        string Header { get; set; }

        string Message { get; set; }
    }
}
