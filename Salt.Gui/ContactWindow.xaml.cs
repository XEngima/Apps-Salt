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

namespace Salt.Gui
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow()
        {
            InitializeComponent();
        }

        public event UpdateContactEventHandler UpdateContact;

        protected void OnUpdateContact(ContactEventArgs e)
        {
            if (UpdateContact != null)
            {
                UpdateContact(this, e);
            }
        }

        public new ContactWindowViewModel DataContext { get { return (ContactWindowViewModel)base.DataContext; } }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var ev = new ContactEventArgs(DataContext.ContactName, DataContext.ContactId, DataContext.KeyName);

            OnUpdateContact(ev);

            if (ev.Handled)
            {
                Close();
            }
        }
    }

    public delegate void UpdateContactEventHandler(object sender, ContactEventArgs e);
}
