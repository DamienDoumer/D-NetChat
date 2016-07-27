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
    /// Interaction logic for AboutWifiHotspotCreator.xaml
    /// </summary>
    public partial class AboutWifiHotspotCreator : Window
    {
        public AboutWifiHotspotCreator()
        {
            InitializeComponent();
        }

        private void WindowBackGround_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void closeBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            closeBorder.BorderBrush = Brushes.SkyBlue;
        }

        private void closeBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            closeBorder.BorderBrush = Brushes.Transparent;
        }
    }
}
