using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public class Message : IMessage
    {
        public Message()
        {
            Id = Guid.NewGuid();
            KeyNameHash = "";
            Header = "";
            Subject = "";
            Content = "";
            KeyStartPos = 0;
        }

        public Message(string keyNameHash, string header, string subject, string content, int keyStartPos)
            :this()
        {
            KeyNameHash = keyNameHash;
            Header = header;
            Subject = subject;
            Content = content;
            KeyStartPos = keyStartPos;
        }

        public Guid Id { get; set; }
        public string KeyNameHash { get; set; }
        public string Header { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int KeyStartPos { get; set; }
    }
}
