using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public interface IMessageStoreItem
    {
        Guid Id { get; }

        int CryptoVersion { get; }

        string KeyName { get; }

        int KeyStartPos { get; }

        string Header { get; }

        string Subject { get; }

        string Message { get; }
    }
}
