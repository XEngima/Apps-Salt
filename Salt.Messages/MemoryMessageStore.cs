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

        public IList<IMessageStoreItem> MessageStoreItems { get; set; }

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

        public string GetSubjectByMessageId(Guid messageId)
        {
            foreach (var messageStoreItem in MessageStoreItems)
            {
                if (messageStoreItem.Id == messageId)
                {
                    return messageStoreItem.Subject;
                }
            }

            return "";
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

        public void SendMessage(IMessageStoreItem message)
        {
            MessageStoreItems.Add(message);
        }

        /// <summary>
        /// Finds the first free key position for a key.
        /// </summary>
        /// <param name="keyName">The name of the key to be used.</param>
        /// <returns>The first free key position.</returns>
        public int FindNextKeyPos(string keyName)
        {
            var messageItem = MessageStoreItems.OrderByDescending(m => m.KeyStartPos).FirstOrDefault(m => m.KeyName == keyName);

            if (messageItem != null)
            {
                return messageItem.KeyStartPos + messageItem.Header.Length + messageItem.Subject.Length + messageItem.Message.Length;
            }

            return 0;
        }
    }
}
