using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class SaltMessageHeader
    {
        public SaltMessageHeader()
        {
        }

        public SaltMessageHeader(Guid messageId, DateTime date, Guid senderId, string senderName, IEnumerable<Guid> recipientIds, IEnumerable<string> recipientNames)
        {
            MessageId = messageId;
            Date = date;
            SenderId = senderId;
            SenderName = senderName;
            RecipientIds = recipientIds;
            RecipientNames = recipientNames;
        }

        public Guid MessageId { get; private set; }

        public DateTime Date { get; private set; }

        public Guid SenderId { get; private set; }

        public string SenderName { get; private set; }

        public IEnumerable<Guid> RecipientIds { get; private set; }

        public IEnumerable<string> RecipientNames { get; private set; }

        public override string ToString()
        {
            return "Date: " + Date.ToShortDateString() + ", Sender: " + SenderName;
        }
    }
}
