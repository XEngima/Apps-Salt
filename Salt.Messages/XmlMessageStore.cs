using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Salt.Messages
{
    public class XmlMessageStore : IMessageStore
    {
        public XmlMessageStore(string folderPath)
        {
            FolderPath = folderPath;
            MessageStoreItems = new List<IMessageStoreItem>();

            // Create a new Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(MessageStoreItem));

            var filePaths = Directory.GetFiles(FolderPath, "*.xml");

            foreach (var filePath in filePaths)
            {
                // Create a new StreamWriter
                var reader = new StreamReader(filePath);

                // Serialize the file
                var messageStoreItem = (IMessageStoreItem)serializer.Deserialize(reader);

                // Close the writer
                reader.Close();

                MessageStoreItems.Add(messageStoreItem);
            }

        }

        public string FolderPath { get; set; }

        private IList<IMessageStoreItem> MessageStoreItems { get; set; }

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

        public void SendMessage(IMessageStoreItem messageStoreItem)
        {
            // Create a new Serializer
            XmlSerializer serializer = new XmlSerializer(messageStoreItem.GetType());

            // Create a new StreamWriter
            TextWriter writer = new StreamWriter(FolderPath + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + "_" + messageStoreItem.KeyName + "_" + messageStoreItem.KeyStartPos + ".xml");

            // Serialize the file
            serializer.Serialize(writer, messageStoreItem);

            // Close the writer
            writer.Close();

            MessageStoreItems.Add(messageStoreItem);
        }
    }
}
