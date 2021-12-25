using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Salt.Contacts;
using Salt.Messages;

namespace Salt.Business
{
    public static class Factory
    {
        private static MemoryContactStore InitializeMemoryContactStore()
        {
            var contactStore = new MemoryContactStore();

            var tobiasContactId = Guid.Parse("00000001-f760-4cf6-a84d-526397dc8b2a");
            var samuelContactId = Guid.Parse("00000003-f760-4cf6-a84d-526397dc8b2a");
            var danielContactId = Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a");

            contactStore.Add(new ContactItem
            {
                Id = tobiasContactId,
                Name = "Tobias",
                KeyName = "DanielTobiasKey"
            });

            contactStore.Add(new ContactItem
            {
                Id = samuelContactId,
                Name = "Samuel",
                KeyName = "DanielSamuelKey"
            });

            contactStore.Add(new ContactItem
            {
                Id = danielContactId,
                Name = "Daniel",
                KeyName = ""
            });

            return contactStore;
        }

        public static IContactStore CreateContactStore()
        {
            return InitializeMemoryContactStore();
        }

        private static MemoryMessageStore InitializeMemoryMessageStore()
        {
            // Create messages in the test message store

            var tobiasContactId = Guid.Parse("00000001-f760-4cf6-a84d-526397dc8b2a");
            var danielContactId = Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a");
            var samuelContactId = Guid.Parse("00000003-f760-4cf6-a84d-526397dc8b2a");

            var messageStoreItems = new List<IMessageStoreItem>();

            var messageTobiasToDanielId = Guid.Parse("10000001-9575-4f71-ba28-cf09c5fdf200");
            var messageSamuelToDanielId = Guid.Parse("10000002-9575-4f71-ba28-cf09c5fdf200");
            var messageDanielToSamuelId = Guid.Parse("10000003-9575-4f71-ba28-cf09c5fdf200");

            // Message from Tobias to Daniel

            var header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 01, 12, 00, 00),
                Sender = tobiasContactId,
                Recipients = new List<Guid>() { danielContactId }
            };

            var message = new Message
            {
                Subject = "A SIGN IN THE STARS",
                Content = "THAT'S CORRECT! THIS IS AWESOME!"
            };

            messageStoreItems.Add(new MessageStoreItem(messageTobiasToDanielId, "DanielTobiasKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            // Message from Samuel to Daniel

            header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 02, 12, 00, 00),
                Sender = samuelContactId,
                Recipients = new List<Guid>() { danielContactId }
            };

            message = new Message
            {
                Subject = "LIN WOOD?",
                Content = "IS HE CORRUPT?"
            };

            messageStoreItems.Add(new MessageStoreItem(messageSamuelToDanielId, "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            // Message from Daniel to Samuel

            header = new MessageHeader
            {
                Date = new DateTime(2021, 01, 03, 12, 00, 00),
                Sender = danielContactId,
                Recipients = new List<Guid>() { samuelContactId }
            };

            message = new Message
            {
                Subject = "RE: LIN WOOD?",
                Content = "NO, HE IS NOT CORRUPT!"
            };

            messageStoreItems.Add(new MessageStoreItem(messageDanielToSamuelId, "DanielSamuelKey", 0, JsonConvert.SerializeObject(header), JsonConvert.SerializeObject(message)));

            return new MemoryMessageStore
            {
                MessageStoreItems = messageStoreItems
            };
        }

        public static IMessageStore CreateMessageStore()
        {
            return InitializeMemoryMessageStore();
        }
    }
}
