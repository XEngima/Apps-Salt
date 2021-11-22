using Salt.Business;
using Salt.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Salt.Contacts;

namespace Salt
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainWindowViewModel()
        {
            SaltApp = new SaltApp(Factory.CreateContactStore(), Factory.CreateMessageStore());

            Contacts = new ObservableCollection<IContactItem>();
            MessageHeaders = new ObservableCollection<IMessageStoreItem>();
            MessageContent = "";

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

            foreach (var message in SaltApp.GetMessageStoreItemsByContactId(SelectedContactId))
            {
                MessageHeaders.Add(message);
            }
        }

        public void ShowMessage()
        {
            MessageContent = SaltApp.GetDecryptedMessage(SelectedMessageId).Content;
        }

        private ISaltApp SaltApp { get; set; }

        public ObservableCollection<IContactItem> Contacts { get; set; }

        public ObservableCollection<IMessageStoreItem> MessageHeaders { get; set; }

        private Guid _selectedContactId { get; set; }

        public Guid SelectedContactId
        {
            get { return _selectedContactId; }
            set
            {
                if (_selectedContactId != value)
                {
                    _selectedContactId = value;
                    OnPropertyChanged("SelectedContactId");
                }
            }
        }

        private Guid _selectedMessageId { get; set; }

        public Guid SelectedMessageId
        {
            get { return _selectedMessageId; }
            set
            {
                if (_selectedMessageId != value)
                {
                    _selectedMessageId = value;
                    OnPropertyChanged("SelectedMessageId");
                }
            }
        }

        private string _messageContent;
        public string MessageContent
        {
            get { return _messageContent; }
            set
            {
                if (_messageContent != value)
                {
                    _messageContent = value;
                    OnPropertyChanged("MessageContent");
                }
            }
        }
    }
}
