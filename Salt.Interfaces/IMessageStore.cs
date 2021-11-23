using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageStore
    {
        IEnumerable<IMessageHeaderItem> GetMessageHeadersByKeyName(string keyName);

        IEnumerable<IMessageStoreItem> GetMessageStoreItemsByKeyName(string keyName);

        void SaveMessage(IMessageStoreItem message);

        IMessageStoreItem GetMessageStoreItem(Guid id);
    }
}
