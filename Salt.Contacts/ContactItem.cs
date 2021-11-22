using Salt.Interfaces;
using System;

namespace Salt.Contacts
{
    public class ContactItem : IContactItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string KeyName { get; set; }
    }
}
