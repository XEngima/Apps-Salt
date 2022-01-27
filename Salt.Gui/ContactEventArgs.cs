using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Gui
{
    public class ContactEventArgs : EventArgs
    {
        public ContactEventArgs(string name, string contactId, string keyName)
        {
            Name = name;
            ContactId = contactId;
            KeyName = keyName;
            Handled = false;
        }

        public string Name { get; set; }

        public string ContactId { get; set; }

        public string KeyName { get; set; }

        public bool Handled { get; set; }
    }
}
