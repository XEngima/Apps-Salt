using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageStore
    {
        IEnumerable<IMessage> GetMessagesByKeyName(string keyName);

        void SaveMessage(IMessage message);

        IMessage GetMessage(Guid id);
    }
}
