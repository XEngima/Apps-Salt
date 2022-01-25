using Salt.Gui;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Salt
{
    /// <summary>
    /// Interaction logic for MessagingWindow.xaml
    /// </summary>
    public partial class MessagingWindow : Window
    {
        public MessagingWindow()
        {
            InitializeComponent();
        }

        private new MessagingWindowViewModel DataContext { get { return (MessagingWindowViewModel)base.DataContext; } }

        public event SendMessageEventHandler SendMessage;

        protected void OnSendMessage(SendMessageEventArgs e)
        {
            if (SendMessage != null)
            {
                SendMessage(this, e);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var ev = new SendMessageEventArgs(DataContext.Recipient, DataContext.KeyName, DataContext.Subject, DataContext.Message);
            OnSendMessage(ev);

            if (ev.Handled)
            {
                Close();
            }
        }

        public delegate void SendMessageEventHandler(object sender, SendMessageEventArgs e);
    }
}
