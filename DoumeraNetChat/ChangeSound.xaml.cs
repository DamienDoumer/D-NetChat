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
    /// Interaction logic for ChangeSound.xaml
    /// </summary>
    public partial class ChangeSound : Window
    {
        public delegate void SoundInputEventHandler(string soundPath);
        public event SoundInputEventHandler SoundInput;
        public ChangeSound()
        {
            InitializeComponent();
            textBox.IsEnabled = false;
        }

        private void validateButton_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text != "")
            {
                if(SoundInput!=null)
                {
                    SoundInput(textBox.Text);
                }
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Sound files (*.wav;|*.wav;|All files (*.*)|*.*";
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
            if (open.ShowDialog() == true)//If the user opens a file the result will be true
            {
                textBox.Text = open.FileName;
                textBox.IsEnabled = false;
            }
        }
    }
}

