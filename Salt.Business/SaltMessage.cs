using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class SaltMessage
    {
        public SaltMessage()
        {
        }

        public SaltMessage(Guid id, DateTime date, string sender, string recipients, string subject, string content)
        {
            Id = id;
            Date = date;
            Sender = sender;
            Recipients = recipients;
            Subject = subject;
            Content = content;
        }

        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets or sets the sender as a guid.
        /// </summary>
        public string Sender { get; private set; }

        /// <summary>
        /// Gets or sets the reciever(s) as a comma separated list of guids.
        /// </summary>
        public string Recipients { get; private set; }

        /// <summary>
        /// Gets or sets the subject (header) of the message.
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        public string Content { get; private set; }

        public override string ToString()
        {
            return Id + ", " + Content;
        }
    }
}
