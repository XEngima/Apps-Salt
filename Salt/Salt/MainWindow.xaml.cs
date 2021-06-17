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
            MessageBox.Show(DataContext.SelectedContactId.ToString());
        }
    }
}
