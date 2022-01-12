using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Salt.Contacts
{
    public class XmlContactStore : IContactStore
    {
        public XmlContactStore(string folderPath, Guid myId, Guid myKeyId)
        {
            FolderPath = folderPath;
            ContactStoreItems = new List<ContactStoreItem>();

            // Create a new Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(ContactStoreItem));

            // If the folder path does not exist, then create it

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // If there are no files in the contact store, then create one with "my" id.

            var filePaths = Directory.GetFiles(FolderPath, "*.xml");

            if (filePaths.Length == 0)
            {
                var meItem = new ContactStoreItem(myId, "Me", myKeyId.ToString());

                // Create a new StreamWriter
                TextWriter writer = new StreamWriter(Path.Combine(FolderPath, "Me.xml"));

                // Serialize the file
                serializer.Serialize(writer, meItem);

                // Close the writer
                writer.Close();
            }

            // Read all files

            filePaths = Directory.GetFiles(FolderPath, "*.xml");

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

        /// <summary>
        /// Gets a contact.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <returns>A contact. null if no contact with the current name exists.</returns>
        public IContactStoreItem GetContactByName(string name)
        {
            return ContactStoreItems.FirstOrDefault(i => i.Name == name);
        }
    }
}
