using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    [Serializable]
    public class ItemMessage
    {
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
