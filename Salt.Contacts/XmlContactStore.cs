using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Salt.Contacts
{
    public class XmlContactStore : IContactStore
    {
        public XmlContactStore(string folderPath)
        {
            FolderPath = folderPath;
            ContactStoreItems = new List<ContactStoreItem>();

            // Create a new Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(ContactStoreItem));

            var filePaths = Directory.GetFiles(FolderPath, "*.xml");

            foreach (var filePath in filePaths)
            {
                // Create a new StreamWriter
                var reader = new StreamReader(filePath);

                // Serialize the file
                var contactStoreItem = (ContactStoreItem)serializer.Deserialize(reader);

                // Close the writer
                reader.Close();

                ContactStoreItems.Add(contactStoreItem);
            }
        }

        private string FolderPath { get; set; }

        private List<ContactStoreItem> ContactStoreItems { get; set; }

        public IEnumerable<IContactStoreItem> GetAllContacts()
        {
            return ContactStoreItems;
        }
    }
}
