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
    /// Interaction logic for EditFriend.xaml
    /// </summary>
    public partial class EditFriend : Window
    {
        public delegate void FriendEditedEventHandler(string name, string IP, string pic);
        public event FriendEditedEventHandler FriendEdited;

        public EditFriend()
        {
            InitializeComponent();
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

        private void validateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IPTextBox.Text == "" || nameTextBox.Text == "")
            {
                FriendEdited(nameTextBox.Text, IPTextBox.Text, pictureTextBox.Text);
                this.Close();
            }
            else
            {
                if (FriendEdited != null)
                {
                    FriendEdited(nameTextBox.Text, IPTextBox.Text, pictureTextBox.Text);
                }
                this.Close();
            }
        }
    }
}
