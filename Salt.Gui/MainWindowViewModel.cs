using Salt.Business;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Salt.Contacts;
using Salt.Messages;
using Salt.Cypher;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using Salt.Gui;

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

        /// <summary>
        /// Loads the settings, or if file does not exist, creates a new settings object.
        /// </summary>
        /// <returns></returns>
        private ISettings GetSettings()
        {
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, "Settings.xml");
                ISettings settings;

                // Create a new Serializer
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                if (File.Exists(path))
                {
                    // Create a new StreamWriter
                    var reader = new StreamReader(path);

                    // Serialize the file
                    settings = (Settings)serializer.Deserialize(reader);

                    // Close the writer
                    reader.Close();
                }
                else
                {
                    settings = new Settings(Environment.CurrentDirectory);

                    // Create a new StreamWriter
                    TextWriter writer = new StreamWriter(path);

                    // Serialize the file
                    serializer.Serialize(writer, settings);

                    // Close the writer
                    writer.Close();
                }

                return settings;
            }
            catch
            {
                return new Settings("C:\\Projekt\\Salt");
            }
        }

        public ISettings Settings { get; private set; }

        public void Initialize()
        {
            // Create a settings object a

            try
            {
                string settingsFilePath = Path.Combine(Environment.CurrentDirectory, "Settings.xml");
                bool firstStartup = !File.Exists(settingsFilePath);

                Settings = GetSettings();

                // if the key store directory does not exist, then create it

                if (!Directory.Exists(Settings.KeyStoreFolderPath))
                {
                    Directory.CreateDirectory(Settings.KeyStoreFolderPath);
                }

                // If there are no key files - e.g. first startup - then create a key.

                var filePaths = Directory.GetFiles(Settings.KeyStoreFolderPath, "*.key");
                var keyId = Guid.Empty;

                if (filePaths.Length == 0)
                {
                    keyId = KeyFileGenerator.CreateNewKeyFile(Settings.KeyStoreFolderPath, 300000);
                }

                var keyStore = Factory.CreateFileKeyStore(Settings);
                var contactStore = Factory.CreateXmlContactStore(Settings, keyId);

                SaltApp = new SaltApp(Settings, contactStore, Factory.CreateXmlMessageStore(Settings), keyStore, new RealCryptographer());

                Contacts = new ObservableCollection<IContactStoreItem>();
                MessageHeaders = new ObservableCollection<MessageHeaderViewModel>();
                MessageContent = "";

                var messageFilePaths = Directory.GetFiles(Settings.MessageStoreFolderPath, "*.xml");

                if (messageFilePaths.Length == 0)
                {
                    var contact = contactStore.GetContactByName("Me");
                    SaltApp.SendMessage(Settings.MyContactId, "Welcome!", DefaultMessage.GetInstructionsMessage(), contact.KeyName.ToString());
                }

                LoadContacts();

                ////Debug
                //string text = DefaultMessage.GetInstructionsMessage();
                //string keyPart = keyStore.GetKeyPart("f83e6dcb-ff45-48d2-a7e8-bbddf58cd92f", 0, text.Length);

                //if (text.Contains("Here are som instructions to get you started."))
                //{
                //    //int index = text.IndexOf("Here are som instructions to get you started.");
                //    //int length = "Here are som instructions to get you started.".Length;

                //    //text = text.Substring(index, length);
                //    //keyPart = keyPart.Substring(index, length * 2);

                //    var chryptograpther = new RealCryptographer();
                //    string encryptedText = chryptograpther.Encrypt(text, keyPart);

                //    TextWriter writer = new StreamWriter(Path.Combine(@"C:\Users\SCBDEIS\Documents\", "Test.xml"));
                //    writer.Write(encryptedText);
                //    writer.Close();


                //    StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\SCBDEIS\Documents\", "Test.xml"));

                //    string encryptedText2 = reader.ReadToEnd();
                //    reader.Close();

                //    var decryptedText = chryptograpther.Decrypt(encryptedText2, keyPart);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        public MainWindowViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Initialize();
            }
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

        public void SaveContact(string name, Guid id, string keyName)
        {
            SaltApp.SaveContact(name, id, keyName);
            Contacts.Add(new ContactStoreItem(id, name, keyName));
        }

        public Guid GenerateKey()
        {
            return SaltApp.GenerateKey();
        }
    }
}
