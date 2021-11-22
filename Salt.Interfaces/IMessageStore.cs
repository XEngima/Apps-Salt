using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageStore
    {
        IEnumerable<IMessageStoreItem> GetMessagesByKeyName(string keyName);

        void SaveMessage(IMessageStoreItem message);

        IMessageStoreItem GetMessageStoreItem(Guid id);
    }
}
