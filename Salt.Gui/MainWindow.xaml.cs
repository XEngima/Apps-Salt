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
            DataContext.SendMessage(Guid.Parse("15d439c1-e6a9-450a-a054-75c258c28a3e"), "fine breakfast today!", "hi maria!\n\nthis is a dynamic message, created for real by the application!", "DanielMariaKey");
        }
    }
}
