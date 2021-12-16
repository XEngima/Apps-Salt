﻿using System;

namespace Salt.Contacts
{
    public class ContactItem : IContactStoreItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string KeyName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
