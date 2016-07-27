using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NetChatCore;

namespace DoumeraNetChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Server server = Server.Instance;
        private Client client = Client.Instance;

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            server.InnerListener.Stop();
            client.InnerTcpClient.Close();
        }
    }
}
