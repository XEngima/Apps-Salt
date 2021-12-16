﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public class MessageHeaderItem : IMessageHeaderItem
    {
        public MessageHeaderItem(Guid messageId, string content)
        {
            MessageId = messageId;
            Content = content;
        }

        public Guid MessageId { get; private set; }

        public string Content { get; private set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
