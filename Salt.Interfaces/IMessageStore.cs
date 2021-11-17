using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageStore
    {
        IEnumerable<IMessage> FetchMessages(string keyName);

        IEnumerable<IMessage> GetMessages(Guid contactId);

        IMessage GetMessage(Guid id);
    }
}
