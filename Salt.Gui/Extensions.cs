using Salt.Business;
using Salt.Cypher;
using Salt.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt
{
    public static class Extensions
    {
        public static MessageHeaderViewModel ToMessageHeaderViewModel(this SaltMessageHeader messageHeader)
        {
            return new MessageHeaderViewModel(messageHeader.MessageId, messageHeader.Date, messageHeader.Subject, messageHeader.RecipientName);
        }
    }
}
