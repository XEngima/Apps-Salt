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
        private List<IMessage> _messages;

        public FakeMessageStore()
        {
            _messages = new List<IMessage>();

            var header = new MessageHeader
            {
                Date = DateTime.Now,
                Sender = Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf200") // Tobias
            };

            _messages.Add(new Message(Guid.Parse("00000001-9575-4f71-ba28-cf09c5fdf200"), "", JsonConvert.SerializeObject(header), "Stjärntecknet", "Det stämmer! Det är mäktigt detta!", 0));

            header = new MessageHeader
            {
                Date = DateTime.Now,
                Sender = Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf300") // Samuel
            };

            _messages.Add(new Message(Guid.Parse("00000002-9575-4f71-ba28-cf09c5fdf200"), "", JsonConvert.SerializeObject(header), "Mike Lindell", "Grattis! Du får en kudde!", 0));
        }

        public IEnumerable<IMessage> FetchMessages(string keyName)
        {
            return new List<IMessage>();
        }

        public IMessage GetMessage(Guid id)
        {
            return _messages.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<IMessage> GetMessagesByKeyName(string keyName)
        {
            var messages = new List<IMessage>();

            foreach (var message in _messages)
            {
                //var header = JsonConvert.DeserializeObject<MessageHeader>(message.Header);

                if (message.KeyName == keyName)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }

        public void SaveMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
