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

namespace DoumeraNetChat
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public delegate void UserNameEnteredEventHandler(string name);
        public event UserNameEnteredEventHandler UserNameEntered;

        public Welcome()
        {
            InitializeComponent();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTextBox.Text != "")
            {
                if (UserNameEntered != null)
                {
                    UserNameEntered(UserNameTextBox.Text);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please input a user name before continuing.",
                    "Warinng.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Done_MouseEnter(object sender, MouseEventArgs e)
        {
            Done.Background = Brushes.SkyBlue;
        }

        private void Done_MouseLeave(object sender, MouseEventArgs e)
        {
            Done.Background = Brushes.Transparent;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
