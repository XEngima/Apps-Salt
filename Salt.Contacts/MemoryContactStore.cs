using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salt.Contacts
{
    public class MemoryContactStore : IContactStore
    {
        public MemoryContactStore()
        {
            Items = new List<IContactStoreItem>();
        }

        private IList<IContactStoreItem> Items { get; set; }

        public void Add(IContactStoreItem contactStoreItem)
        {
            Items.Add(contactStoreItem);
        }

        public IEnumerable<IContactStoreItem> GetAllContacts()
        {
            return Items;
        }

        /// <summary>
        /// Gets a contact.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <returns>A contact. null if no contact with the current name exists.</returns>
        public IContactStoreItem GetContactByName(string name)
        {
            return Items.FirstOrDefault(i => i.Name == name);
        }

        public void SaveContact(string name, Guid id, string keyName)
        {
            Items.Add(new ContactStoreItem(id, name, keyName));
        }
    }
}
