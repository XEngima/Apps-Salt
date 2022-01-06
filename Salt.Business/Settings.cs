using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class Settings : ISettings
    {
        public Guid MyContactId {
            get { return Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a"); }
            set { }
        }

        public string MessageStoreFolderPath {
            get { return @"C:\Projekt\Salt\Data\MessageStore\"; }
            set { }
        }

        public string KeyStoreFolderPath
        {
            get { return @"C:\Projekt\Salt\Data\KeyStore\"; }
            set { }
        }

        public string ContactStoreFolderPath
        {
            get { return @"C:\Projekt\Salt\Data\ContactStore\"; }
            set { }
        }
    }
}
