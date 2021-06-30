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
        public IEnumerable<IMessage> FetchMessages(string keyName)
        {
            return null;
        }

        public IMessage GetMessage(int id)
        {
            if (id == 10)
            {
                return new Message(10, "", "header", "jeH", "nasjeH", 0);
            }

            return null;
        }

        public IEnumerable<IMessage> GetMessages(Guid contactId)
        {
            return null;
        }
    }
}
