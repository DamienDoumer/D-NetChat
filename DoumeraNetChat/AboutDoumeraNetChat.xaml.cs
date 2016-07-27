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
    /// Interaction logic for AboutDoumeraNetChat.xaml
    /// </summary>
    public partial class AboutDoumeraNetChat : Window
    {
        public AboutDoumeraNetChat()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeBorderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void closeBorderMouseEnter(object sender, MouseEventArgs e)
        {
            closeBorder.BorderBrush = Brushes.SkyBlue;
        }

        private void closeBorderMouseLeave(object sender, MouseEventArgs e)
        {
            closeBorder.BorderBrush = Brushes.Transparent;
        }
    }
}
