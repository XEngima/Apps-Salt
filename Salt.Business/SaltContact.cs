using System;
using Salt.Contacts;

namespace Salt.Business
{
    public class SaltContact : IContactStoreItem
    {
        public SaltContact(Guid id, string name, string keyName)
        {
            Id = id;
            Name = name;
            KeyName = keyName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string KeyName { get; set; }
    }
}
