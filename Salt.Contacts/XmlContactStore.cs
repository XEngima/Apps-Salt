using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Salt.Shared;

namespace Salt.Contacts
{
    public class XmlContactStore : IContactStore
    {
        public XmlContactStore(IFileService fileService, string folderPath, Guid myId, Guid myKeyId)
        {
            FileService = fileService;
            FolderPath = folderPath;
            ContactStoreItems = new List<ContactStoreItem>();

            // Create a new Serializer
            //XmlSerializer serializer = new XmlSerializer(typeof(ContactStoreItem));

            // If the folder path does not exist, then create it

            if (!FileService.DirectoryExists(folderPath))
            {
                FileService.CreateDirectory(folderPath);
            }

            // If there are no files in the contact store, then create one with "my" id.

            var filePaths = FileService.GetDirectoryFiles(FolderPath, "*.xml");

            if (filePaths.Length == 0)
            {
                var meItem = new ContactStoreItem(myId, "Me", myKeyId.ToString());
                FileService.SaveAsXmlFile(Path.Combine(FolderPath, "Me.xml"), meItem);
            }

            // Read all files

            filePaths = FileService.GetDirectoryFiles(FolderPath, "*.xml");

            foreach (var filePath in filePaths)
            {
                var contactStoreItem = (ContactStoreItem)FileService.ReadXmlFile(filePath, typeof(ContactStoreItem));

                //// Create a new StreamWriter
                //var reader = new StreamReader(filePath);

                //// Serialize the file
                //var contactStoreItem = (ContactStoreItem)serializer.Deserialize(reader);

                //// Close the writer
                //reader.Close();

                ContactStoreItems.Add(contactStoreItem);
            }
        }

        private IFileService FileService { get; set; }

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

        public void SaveContact(string name, Guid id, string keyName)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(ContactStoreItem));

            var item = new ContactStoreItem(id, name, keyName);

            FileService.SaveAsXmlFile(Path.Combine(FolderPath, name + ".xml"), item);

            //// Create a new StreamWriter
            //TextWriter writer = new StreamWriter(Path.Combine(FolderPath, name + ".xml"));

            //// Serialize the file
            //serializer.Serialize(writer, item);

            //// Close the writer
            //writer.Close();

            ContactStoreItems.Add(item);
        }
    }
}
