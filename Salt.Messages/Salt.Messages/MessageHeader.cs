using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Salt.Interfaces;

namespace Salt.Messages
{
    public class MessageHeader : IMessageHeader
    {
        [JsonProperty(PropertyName = "D")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "S")]
        public Guid Sender { get; set; }

        [JsonProperty(PropertyName = "R")]
        public List<Guid> Recipients { get; set; }
    }
}
