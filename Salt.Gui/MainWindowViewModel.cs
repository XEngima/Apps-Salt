using Salt.Business;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Salt.Contacts;
using Salt.Messages;
using Salt.Cypher;

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
            //SaltApp = new SaltApp(new Settings(), Factory.CreateMemoryContactStore(), Factory.CreateMemoryMessageStore());

            var settings = new Settings();

            SaltApp = new SaltApp(settings, Factory.CreateMemoryContactStore(), Factory.CreateXmlMessageStore(settings), Factory.CreateFileKeyStore(settings), new RealCryptographer());

            Contacts = new ObservableCollection<IContactStoreItem>();
            MessageHeaders = new ObservableCollection<MessageHeaderViewModel>();
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

            foreach (var message in SaltApp.GetMessageHeadersByAnyContactId(SelectedContactId))
            {
                MessageHeaders.Add(message.ToMessageHeaderViewModel());
            }
        }

        public void ShowMessage()
        {
            MessageContent = SaltApp.GetMessage(SelectedMessageId).Content;
        }

        private ISaltApp SaltApp { get; set; }

        public ObservableCollection<IContactStoreItem> Contacts { get; set; }

        public ObservableCollection<MessageHeaderViewModel> MessageHeaders { get; set; }

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

        public void SendMessage(Guid recipient, string subject, string message, string keyName)
        {
            SaltApp.SendMessage(recipient, subject, message, keyName);
        }
    }
}
