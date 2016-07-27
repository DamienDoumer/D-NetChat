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
    /// Interaction logic for EditUserSettings.xaml
    /// </summary>
    public partial class EditUserSettings : Window
    {
        public delegate void UserNameEnteredEventHandler(string name);
        public event UserNameEnteredEventHandler UserNameEntered;


        public EditUserSettings()
        {
            InitializeComponent();
        }

        private void OKbutton_Click(object sender, RoutedEventArgs e)
        {
            if(UserNameEntered!=null)
            {
                UserNameEntered(textBox.Text);
            }
            this.Close();
        }
    }
}
