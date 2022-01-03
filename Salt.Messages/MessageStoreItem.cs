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
            Header = "";
            Subject = "";
            Message = "";
        }

        public MessageStoreItem(Guid id, string keyName, int keyStartPos, string header, string subject, string message)
        {
            Id = id;
            KeyName = keyName;
            KeyStartPos = keyStartPos;
            Header = header;
            Subject = subject;
            Message = message;
        }

        public Guid Id { get; private set; }
        public int CryptoVersion { get; private set; }
        public string KeyName { get; private set; }
        public int KeyStartPos { get; private set; }
        public string Header { get; private set; }
        public string Subject { get; private set; }
        public string Message { get; private set; }

        public override string ToString()
        {
            return Id + ", " + Subject;
        }
    }
}
