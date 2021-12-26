using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Salt.Messages
{
    public class ItemHeader : IMessageHeader
    {
        [JsonProperty(PropertyName = "D")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "S")]
        public Guid Sender { get; set; }

        [JsonProperty(PropertyName = "R")]
        public Guid Recipient { get; set; }

        /// <summary>
        /// Gets or sets the subject (header) of the message.
        /// </summary>
        [JsonProperty(PropertyName = "H")]
        public string Subject { get; set; }

        public override string ToString()
        {
            return "Date: " + Date.ToShortDateString() + ", " + Sender + "->" + Recipient;
        }
    }
}
