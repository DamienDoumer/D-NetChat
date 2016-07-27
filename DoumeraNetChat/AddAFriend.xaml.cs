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
using Microsoft.Win32;

namespace DoumeraNetChat
{
    /// <summary>
    /// Interaction logic for AddAFriend.xaml
    /// </summary>
    public partial class AddAFriend : Window
    {
        public delegate void FriendAddedEventHandler(string name, string IP, string PicturePath);
        public event FriendAddedEventHandler FriendAdded;

        public AddAFriend()
        {
            InitializeComponent();
        }

        private void validateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IPTextBox.Text == "" || nameTextBox.Text == "")
            {
                MessageBox.Show("Please you must input your friend's IP address and Name.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (FriendAdded != null)
                {
                    FriendAdded(nameTextBox.Text, IPTextBox.Text, pictureTextBox.Text);
                }
                this.Close();
              }
          }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image files (*.png;*.jpeg;*.jpg;*.ico)|*.png;*.jpeg;*.jpg;*.ico|All files (*.*)|*.*";
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            if (open.ShowDialog() == true)//If the user opens a file the result will be true
            {
                pictureTextBox.Text = open.FileName;
                pictureTextBox.IsEnabled = false;
            }
            
        }
    }
}
