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
            Messages = new List<IMessageStoreItem>();
        }

        public IEnumerable<IMessageStoreItem> Messages { get; set; }

        public IMessageStoreItem GetMessageStoreItem(Guid id)
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

        public IEnumerable<IMessageStoreItem> GetMessagesByKeyName(string keyName)
        {
            return null;
        }

        public void SaveMessage(IMessageStoreItem message)
        {
            throw new NotImplementedException();
        }
    }
}
