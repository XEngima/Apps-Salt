using Newtonsoft.Json;
using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salt.Messages
{
    public class FakeMessageStore : IMessageStore
    {
        private List<IMessageStoreItem> _messageStoreItems;

        public FakeMessageStore()
        {
            _messageStoreItems = new List<IMessageStoreItem>();

            var header = new MessageHeader
            {
                Date = DateTime.Now,
                Sender = Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf200"), // Tobias
                Recipients = new List<Guid> {
                    Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf200") // Också Tobias
                }
            };

            var message = new Message
            {
                Subject = "Stjärntecknet",
                Content = "Det stämmer! Det är mäktigt detta!"
            };

            _messageStoreItems.Add(new MessageStoreItem(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"), "KEYNAME", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));
        }

        public IEnumerable<IMessageStoreItem> FetchMessages(string keyName)
        {
            return new List<IMessageStoreItem>();
        }

        public IMessageStoreItem GetMessageStoreItem(Guid id)
        {
            return _messageStoreItems.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<IMessageStoreItem> GetMessagesByKeyName(string keyName)
        {
            var messages = new List<IMessageStoreItem>();

            foreach (var message in _messageStoreItems)
            {
                //var header = JsonConvert.DeserializeObject<MessageHeader>(message.Header);

                if (message.KeyName == keyName)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }

        public void SaveMessage(IMessageStoreItem message)
        {
            throw new NotImplementedException();
        }
    }
}
