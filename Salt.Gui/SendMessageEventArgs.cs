using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Gui
{
    public class SendMessageEventArgs : EventArgs
    {
        public SendMessageEventArgs(string recipient, string keyName, string subject, string message)
        {
            Recipient = recipient;
            KeyName = keyName;
            Subject = subject;
            Message = message;
        }

        public string Recipient { get; set; }

        public string KeyName { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
