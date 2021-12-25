using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Messages
{
    public class MemoryMessageStore : IMessageStore
    {
        public MemoryMessageStore()
        {
            MessageStoreItems = new List<IMessageStoreItem>();
        }

        public IEnumerable<IMessageStoreItem> MessageStoreItems { get; set; }

        public IEnumerable<IMessageHeaderItem> GetMessageHeadersByKeyName(string keyName)
        {
            var items = new List<IMessageHeaderItem>();

            foreach (var messageStoreItem in MessageStoreItems)
            {
                if (messageStoreItem.KeyName == keyName)
                {
                    items.Add(new MessageHeaderItem(messageStoreItem.Id, messageStoreItem.Header));
                }
            }

            return items;
        }

        public IMessageStoreItem GetMessageStoreItem(Guid id)
        {
            foreach (var message in MessageStoreItems)
            {
                if (message.Id == id)
                {
                    return message;
                }
            }

            return null;
        }

        public IEnumerable<IMessageStoreItem> GetMessageStoreItemsByKeyName(string keyName)
        {
            var items = new List<IMessageStoreItem>();

            foreach (var messageStoreItem in MessageStoreItems)
            {
                if (messageStoreItem.KeyName == keyName)
                {
                    items.Add(messageStoreItem);
                }
            }

            return items;
        }

        public void SaveMessage(IMessageStoreItem message)
        {
            throw new NotImplementedException();
        }
    }
}
