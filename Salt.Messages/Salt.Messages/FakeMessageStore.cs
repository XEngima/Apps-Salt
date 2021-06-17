using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public class FakeMessageStore : IMessageStore
    {
        public IEnumerable<IMessage> FetchMessages(string keyName)
        {
            return new List<IMessage>
            {
                new Message("", "", "En grej", "Måste berätta en sak", 0)
            };
        }
    }
}
