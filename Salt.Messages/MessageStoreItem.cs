using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public class MessageStoreItem : IMessageStoreItem
    {
        public MessageStoreItem()
        {
            Id = Guid.NewGuid();
            KeyName = "";
            KeyStartPos = 0;
            Message = "";
        }

        public MessageStoreItem(Guid id, string keyName, int keyStartPos, string header, string message)
        {
            Id = id;
            KeyName = keyName;
            KeyStartPos = keyStartPos;
            Header = header;
            Message = message;
        }

        public Guid Id { get; set; }
        public int CryptoVersion { get; set; }
        public string KeyName { get; set; }
        public int KeyStartPos { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
    }
}
