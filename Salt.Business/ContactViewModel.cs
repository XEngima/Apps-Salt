using Salt.Interfaces;
using System;
using Salt.Contacts;

namespace Salt.Business
{
    public class ContactViewModel : IContactStoreItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string KeyName { get; set; }
    }
}
