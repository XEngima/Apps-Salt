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

        IMessageStoreItem GetMessageStoreItem(Guid id);

        void SendMessage(IMessageStoreItem message);

        /// <summary>
        /// Finds the first free key position for a key.
        /// </summary>
        /// <param name="keyName">The name of the key to be used.</param>
        /// <returns>The first free key position.</returns>
        int FindNextKeyPos(string keyName);
    }
}
