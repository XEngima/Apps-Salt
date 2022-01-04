using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

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

        [XmlAttribute]
        public Guid Id { get; private set; }
        [XmlAttribute]
        public int CryptoVersion { get; private set; }
        [XmlAttribute]
        public string KeyName { get; private set; }
        [XmlAttribute]
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
