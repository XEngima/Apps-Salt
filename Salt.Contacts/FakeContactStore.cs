using Salt.Interfaces;
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
                new ContactItem { Id = Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf200"), Name = "Tobias", KeyName = "abc" },
                new ContactItem { Id = Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf300"), Name = "Samuel", KeyName = "abc" },
                new ContactItem { Id = Guid.Parse("2d782cd5-9575-4f71-ba28-cf09c5fdf400"), Name = "Hanna", KeyName = "abc" },
            };
        }
    }
}
