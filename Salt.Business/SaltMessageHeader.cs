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

        public SaltMessageHeader(Guid messageId, DateTime date, Guid senderId, string senderName, Guid recipientId, string recipientName, string subject)
        {
            MessageId = messageId;
            Date = date;
            SenderId = senderId;
            SenderName = senderName;
            RecipientId = recipientId;
            RecipientName = recipientName;
            Subject = subject;
        }

        public Guid MessageId { get; private set; }

        public DateTime Date { get; private set; }

        public Guid SenderId { get; private set; }

        public string SenderName { get; private set; }

        public Guid RecipientId { get; private set; }

        public string RecipientName { get; private set; }

        public string Subject { get; private set; }

        public override string ToString()
        {
            return "Date: " + Date.ToShortDateString() + ", Sender: " + SenderName;
        }
    }
}
