using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public interface IMessageStore
    {
        IEnumerable<IMessageHeaderItem> GetMessageHeadersByKeyName(string keyName);

        string GetSubjectByMessageId(Guid messageId);

        IEnumerable<IMessageStoreItem> GetMessageStoreItemsByKeyName(string keyName);

        void SaveMessage(IMessageStoreItem message);

        IMessageStoreItem GetMessageStoreItem(Guid id);
    }
}
