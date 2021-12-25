using System;
using System.Collections.Generic;
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
    }
}
