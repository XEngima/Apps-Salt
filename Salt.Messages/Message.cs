﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    [Serializable]
    public class Message
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [JsonProperty(PropertyName = "D")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the sender as a guid.
        /// </summary>
        [JsonProperty(PropertyName = "S")]
        public Guid Sender { get; set; }

        /// <summary>
        /// Gets or sets the reciever(s) as a comma separated list of guids.
        /// </summary>
        [JsonProperty(PropertyName = "R")]
        public string Recievers { get; set; }

        /// <summary>
        /// Gets or sets the subject (header) of the message.
        /// </summary>
        [JsonProperty(PropertyName = "H")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        [JsonProperty(PropertyName = "C")]
        public string Content { get; set; }
    }
}
