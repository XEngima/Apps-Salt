using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Contacts
{
    public interface IContactStore
    {
        IEnumerable<IContactStoreItem> GetAllContacts();

        /// <summary>
        /// Gets a contact.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <returns>A contact. null if no contact with the current name exists.</returns>
        IContactStoreItem GetContactByName(string name);

        void SaveContact(string name, Guid id, string keyName);
    }
}
