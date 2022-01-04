using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Salt.Messages
{
    public class ItemHeader : IMessageHeader
    {
        public ItemHeader()
        {
        }

        public ItemHeader(DateTime date, Guid sender, Guid recipient)
        {
            Date = date;
            Sender = sender;
            Recipient = recipient;
        }

        [JsonProperty(PropertyName = "D")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "S")]
        public Guid Sender { get; set; }

        [JsonProperty(PropertyName = "R")]
        public Guid Recipient { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return "Date: " + Date.ToShortDateString() + ", " + Sender + "->" + Recipient;
        }
    }
}
