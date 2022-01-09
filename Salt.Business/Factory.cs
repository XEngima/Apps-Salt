using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Salt.Contacts;
using Salt.Keys;
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

            contactStore.Add(new ContactStoreItem(tobiasContactId, "Tobias", "DanielTobiasKey"));
            contactStore.Add(new ContactStoreItem(samuelContactId, "Samuel", "DanielSamuelKey"));
            contactStore.Add(new ContactStoreItem(tobiasContactId, "Daniel", ""));

            return contactStore;
        }

        public static IContactStore CreateMemoryContactStore()
        {
            return InitializeMemoryContactStore();
        }

        public static IContactStore CreateXmlContactStore(ISettings settings, Guid myKeyName)
        {
            return new XmlContactStore(settings.ContactStoreFolderPath, settings.MyContactId, myKeyName);
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

            var header = new ItemHeader
            {
                Date = new DateTime(2021, 01, 01, 12, 00, 00),
                Sender = tobiasContactId,
                Recipient = danielContactId,
            };

            string sHeader = JsonConvert.SerializeObject(header);
            string subject = "A SIGN IN THE STARS";
            string message = "THAT'S CORRECT! THIS IS AWESOME!";

            messageStoreItems.Add(new MessageStoreItem(messageTobiasToDanielId, "DanielTobiasKey", 0, sHeader.Length + subject.Length + message.Length, sHeader, subject, message));

            // Message from Samuel to Daniel

            header = new ItemHeader
            {
                Date = new DateTime(2021, 01, 02, 12, 00, 00),
                Sender = samuelContactId,
                Recipient = danielContactId,
            };

            sHeader = JsonConvert.SerializeObject(header);
            subject = "LIN WOOD?";
            message = "IS HE CORRUPT?";

            messageStoreItems.Add(new MessageStoreItem(messageSamuelToDanielId, "DanielSamuelKey", 0, sHeader.Length + subject.Length + message.Length, sHeader, subject, message));

            // Message from Daniel to Samuel

            header = new ItemHeader
            {
                Date = new DateTime(2021, 01, 03, 12, 00, 00),
                Sender = danielContactId,
                Recipient = samuelContactId,
            };

            sHeader = JsonConvert.SerializeObject(header);
            subject = "RE: LIN WOOD?";
            message = "NO, HE IS NOT CORRUPT!";

            messageStoreItems.Add(new MessageStoreItem(messageDanielToSamuelId, "DanielSamuelKey", 136, sHeader.Length + subject.Length + message.Length, sHeader, subject, message));

            return new MemoryMessageStore
            {
                MessageStoreItems = messageStoreItems
            };
        }

        public static IMessageStore CreateMemoryMessageStore()
        {
            return InitializeMemoryMessageStore();
        }

        public static IMessageStore CreateXmlMessageStore(ISettings settings)
        {
            return new XmlMessageStore(settings.MessageStoreFolderPath);
        }

        public static IKeyStore CreateLetterKeyStore()
        {
            var keyStore = new LetterKeyStore();

            keyStore.Items.Add(new KeyStoreItem("DanielSamuelKey", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
            keyStore.Items.Add(new KeyStoreItem("DanielTobiasKey", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));

            return keyStore;
        }

        public static IKeyStore CreateFileKeyStore(ISettings settings)
        {
            return new FileKeyStore(settings.KeyStoreFolderPath);
        }
    }
}
