using Salt.Interfaces;
using Salt.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestMessageStore : IMessageStore
    {
        public TestMessageStore()
        {
            Messages = new List<IMessage>();
        }

        public IEnumerable<IMessage> Messages { get; set; }

        public IMessage GetMessage(Guid id)
        {
            foreach (var message in Messages)
            {
                if (message.Id == id)
                {
                    return message;
                }
            }

            return null;
        }

        public IEnumerable<IMessage> GetMessagesByKeyName(string keyName)
        {
            return null;
        }

        public void SaveMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
