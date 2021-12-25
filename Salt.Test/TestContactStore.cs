using Salt.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestContactStore : IContactStore
    {
        public TestContactStore()
        {
            Contacts = new List<IContactStoreItem>();
        }

        public IEnumerable<IContactStoreItem> Contacts { get; set; }

        public void Add(IContactStoreItem contactStoreItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IContactStoreItem> GetAllContacts()
        {
            return Contacts;
        }

        public IContactStoreItem GetContactStoreItem(Guid id)
        {
            foreach (var contact in Contacts)
            {
                if (contact.Id == id)
                {
                    return contact;
                }
            }

            return null;
        }
    }
}
