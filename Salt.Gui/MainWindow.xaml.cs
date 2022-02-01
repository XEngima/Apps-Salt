using Salt.Business;
using Salt.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Salt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private new MainWindowViewModel DataContext { get { return (MainWindowViewModel)base.DataContext; } }

        private void ContactsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext.LoadMessageHeaders();
            DataContext.MessageContent = "";
            //MessageBox.Show(DataContext.SelectedContactId.ToString());
        }

        private void HeaderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext.ShowMessage();
        }

        private void NewMessageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //DataContext.SendMessage(Guid.Parse("15d439c1-e6a9-450a-a054-75c258c28a3e"), "fine breakfast today!", "hi maria!\n\nthis is a dynamic message, created for real by the application!", "DanielMariaKey");

            var messagingWindow = new MessagingWindow();

            messagingWindow.SendMessage += MessagingWindow_SendMessage;

            messagingWindow.ShowDialog();
        }

        private void MessagingWindow_SendMessage(object sender, SendMessageEventArgs e)
        {
            var recipient = DataContext.Contacts.FirstOrDefault(c => c.Name == e.Recipient);

            try
            {
                if (recipient != null)
                {
                    if (string.IsNullOrEmpty(recipient.KeyName))
                    {
                        MessageBox.Show("The recipient " + e.Recipient + " does not have an associated key. Make sure that the contact has a key specified and that the key exists in the key store.", "Message not sent", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        DataContext.SendMessage(recipient.Id, e.Subject, e.Message, recipient.KeyName);
                        MessageBox.Show("Message sent to " + e.Recipient + ".", "Message sent", MessageBoxButton.OK, MessageBoxImage.Information);
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("The recipient " + e.Recipient + " could not be found in the contact store.", "Message not sent", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MessagingException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = new ContactWindow();

            contactWindow.UpdateContact += ContactWindow_UpdateContact;

            contactWindow.ShowDialog();
        }

        private void ContactWindow_UpdateContact(object sender, ContactEventArgs e)
        {
            Guid uid;
            if (Guid.TryParse(e.ContactId, out uid))
            {
                DataContext.SaveContact(e.Name, uid, e.KeyName);
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("The ID must be a valid UUID.");
            }
        }
    }
}
