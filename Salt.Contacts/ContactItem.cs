using System;

namespace Salt.Contacts
{
    public class ContactItem : IContactStoreItem
    {
        public ContactItem(Guid id, string name, string keyName)
        {
            Id = id;
            Name = name;
            KeyName = keyName;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string KeyName { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
