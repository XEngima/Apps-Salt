using System;
using System.Xml.Serialization;

namespace Salt.Contacts
{
    [XmlType("Contact")]
    public class ContactStoreItem : IContactStoreItem
    {
        public ContactStoreItem()
        {
        }

        public ContactStoreItem(Guid id, string name, string keyName)
        {
            Id = id;
            Name = name;
            KeyName = keyName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string KeyName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
