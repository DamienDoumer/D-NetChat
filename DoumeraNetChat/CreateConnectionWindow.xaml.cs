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
using DoumeraNetChat.VirtualWifiHotspotCreator;

namespace DoumeraNetChat
{
    /// <summary>
    /// Interaction logic for CreateConnectionWindow.xaml
    /// </summary>
    public partial class CreateConnectionWindow : Window
    {
        public delegate void HotspotStartsEventHandler(string SSID, string psw);
        public delegate void HotstpotStopsEventHandler(string SSID, string psw);
        public event HotspotStartsEventHandler HotspotStarts;
        public event HotstpotStopsEventHandler HotspotStops;

        public CreateConnectionWindow()
        {
            InitializeComponent();
            Wifi.OnHotspotStarted(new WifiHotspot.HotspotStartedEventHandler(OnHotspotStarted));
            Wifi.OnHotspotStopped(new WifiHotspot.HotspotStoppedEventHandler(OnHotspotStopped));
            Wifi.OnErrorOccured(new WifiHotspot.ErrorOccuredEventHandler(OnErrorOccured));
            Wifi.OnHotspotStarting(new WifiHotspot.HotspotStartingEventHandler(OnHotspotStarting));
            Wifi.OnHotspotStopping(new WifiHotspot.HotspotStoppingEventHandler(OnHotspotStopping));
        }

        private void aboutLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutLabel.Background = Brushes.Gray;
        }

        private void aboutLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutLabel.Background = Brushes.Transparent;
        }

        private void aboutLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AboutWifiHotspotCreator abt = new AboutWifiHotspotCreator();
            abt.ShowDialog();
        }
        private void OnHotspotStopping(object sender)
        {
            this.Cursor = Cursors.Wait;
        }
        private void OnHotspotStarted(object sender)
        {
            this.Cursor = Cursors.Arrow;
            startStopBtn.Content = "Stop";
            statusLabel.Content = "Wifi hotspot started";
            passwordBox.IsEnabled = false;
            SSIDTextBox.IsEnabled = false;
        }
        private void OnHotspotStopped(object Sender)
        {
            this.Cursor = Cursors.Arrow;
            startStopBtn.Content = "Start";
            statusLabel.Content = "Wifi hotspot stopped";
            passwordBox.IsEnabled = true;
            SSIDTextBox.IsEnabled = true;
        }
        private void OnErrorOccured(object sender, Exception ex)
        {
            MessageBox.Show(ex.Message, "An error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
            Wifi.StopHotspot();
            startStopBtn.Content = "Start";
        }
        private void OnHotspotStarting(object sender)
        {
            this.Cursor = Cursors.Wait;
        }
        private void startStopBtn_Click(object sender, RoutedEventArgs e)
        {
            string btnText = (string)startStopBtn.Content;
            if (btnText == "Start")
            {
                Wifi.SetKey(passwordBox.Password);
                Wifi.SetSSID(SSIDTextBox.Text);
                Wifi.StartHotspot();
                if (HotspotStarts != null)
                {
                    HotspotStarts(SSIDTextBox.Text, passwordBox.Password);
                }
            }
            else
            {
                Wifi.StopHotspot();
                if (HotspotStops != null)
                {
                    HotspotStops(SSIDTextBox.Text, passwordBox.Password);
                }
            }
        }
    }
}
