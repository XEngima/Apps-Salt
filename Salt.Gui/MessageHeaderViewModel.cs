using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt
{
    public class MessageHeaderViewModel
    {
        public MessageHeaderViewModel(Guid messageId, DateTime time, string subject)
        {
            MessageId = messageId;
            Time = time;
            Subject = subject;
        }

        public Guid MessageId { get; private set; }

        public DateTime Time { get; private set; }

        public string Subject { get; private set; }

        public string Text { get { return Time.ToString("yyyy-MM-dd HH:mm") + " - " + Subject; } }

        public override string ToString()
        {
            return Text;
        }
    }
}
