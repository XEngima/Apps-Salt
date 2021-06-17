using Salt.Interfaces;
using Salt.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Contacts
{
    public class FakeContactStore : IContactStore
    {
        public IEnumerable<IContactItem> GetAllContacts()
        {
            return new List<IContactItem>
            {
                new ContactItem { Id = Guid.NewGuid(), Name = "Tobias", KeyName = "abc" },
                new ContactItem { Id = Guid.NewGuid(), Name = "Samuel", KeyName = "abc" },
                new ContactItem { Id = Guid.NewGuid(), Name = "Daniel", KeyName = "abc" },
            };
        }
    }
}
