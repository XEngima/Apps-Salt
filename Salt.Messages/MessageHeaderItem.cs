using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public class MessageHeaderItem : IMessageHeaderItem
    {
        public MessageHeaderItem(Guid messageId, string content, int keyStartPos, int keyLength)
        {
            MessageId = messageId;
            Content = content;
            KeyStartPos = keyStartPos;
            KeyLength = keyLength;
        }

        public Guid MessageId { get; private set; }

        public string Content { get; private set; }

        public int KeyStartPos { get; private set; }

        public int KeyLength { get; private set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
