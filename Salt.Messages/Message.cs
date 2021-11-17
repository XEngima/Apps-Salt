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
            KeyName = "";
            Header = "";
            Subject = "";
            Content = "";
            KeyStartPos = 0;
            TotalLength = 0;
        }

        public Message(Guid id, string keyName, string header, string subject, string content, int keyStartPos)
        {
            Id = id;
            KeyName = keyName;
            Header = header;
            Subject = subject;
            Content = content;
            KeyStartPos = keyStartPos;
            TotalLength = header.Length + subject.Length + content.Length;
        }

        public Guid Id { get; set; }
        public string KeyName { get; set; }
        public string Header { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int KeyStartPos { get; set; }
        public int TotalLength { get; private set; }
    }
}
