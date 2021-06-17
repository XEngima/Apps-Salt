using Salt.Business;
using Salt.Interfaces;
using Salt.Shared;
using System;
using System.Collections.ObjectModel;

namespace Salt
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            SaltApp = new SaltApp();

            Contacts = new ObservableCollection<IContactItem>();
            MessageHeaders = new ObservableCollection<string>();

            LoadContacts();
        }

        private void LoadContacts()
        {
            foreach (var contact in SaltApp.GetContacts())
            {
                Contacts.Add(contact);
            }
        }

        public void LoadMessageHeaders()
        {
            MessageHeaders.Clear();

            string keyName = "";

            foreach (var message in SaltApp.GetMessagesByKeyName("", keyName))
            {
                MessageHeaders.Add(message.Subject);
            }
        }

        private ISaltApp SaltApp { get; set; }

        public ObservableCollection<IContactItem> Contacts { get; set; }

        public ObservableCollection<string> MessageHeaders { get; set; }

        public Guid SelectedContactId { get; set; }
    }
}
