using System;
using System.Collections;
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
using DoumeraNetChat.Friends;
using NetChatDataAccesors;
using NetChatCore;
using System.Net.Sockets;
using System.IO;


namespace DoumeraNetChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NetChatDao dao = null;
        List<Friend> friends = null;
        Server server = null;
        Client client = null;
        Friend presentFriend = null;
        string selectedFriend = null;
        DoumeraSoundPlayer player = null;

        public MainWindow()
        {
            dao = NetChatDao.Instance;
            friends = new List<Friend>();
            server = Server.Instance;
            client = Client.Instance;
            player = new DoumeraSoundPlayer();
            player.setSoundPath(dao.RetrieveSoundPath());
            
            FirstUseAction();

            InitializeComponent();
            
            SetFriendsOnListBox();
            
            server.IPAddressSet += OnIPAddressSet;
            server.IPAddressChanged += OnIPAddressChanged;
            server.MessageReceived += OnServerMessageRecieved;
            server.ErrorOccured += OnserverErrorOccured;
            server.ClientDisconnected += ServerClientDisconnected;
            server.ConnectionEstablished += ServerClientConnected;
            server.Start();

            client.ClientConnected += ClientConnected;
            client.ErrorOccured += ClientErrorOccured;
            client.MessageSent += ClientMessageSent;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutDoumeraNetChat about = new AboutDoumeraNetChat();
            about.ShowDialog();
        }
        private void changeNotificationSound_Click(object sender, RoutedEventArgs e)
        {
            ChangeSound chSound = new ChangeSound();
            chSound.SoundInput += OnSoundPathChanged; 
            chSound.ShowDialog();
        }
        private void OnSoundPathChanged(string path)
        {
            dao.ChangeSoundPath(path);
            player.setSoundPath(path);
        }
        private void EditUserSettings_Click(object sender, RoutedEventArgs e)
        {
            DoumeraNetChat.EditUserSettings edit = new DoumeraNetChat.EditUserSettings();
            edit.UserNameEntered += OnEditUserName; //Do somthing with the name;
            edit.ShowDialog();
        }
        private void OnEditUserName(string name)
        {
            dao.SaveUserData(name);
        }
        private void SetFriendsOnListBox()
        {
            foreach(Friend f in friends)
            {
                FriendListBox.Items.Add(f.ToString());
            }
        }
        private void LoadFriends()
        {
            List<string> fr = null;

            if (friends.Count > 0)
            {
                friends.Clear();
            }
            try
            {
                foreach (string Ip in dao.GetAllFriendIP())
                {
                    fr = dao.GetFriend(Ip);
                    friends.Add(new Friend(fr[1], fr[0], fr[2]));
                    fr = null;
                }
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
                dao.Connection.Close();
            }
        }
        private bool CheckIfUserIsNew()
        {
            string user = dao.GetUserName();
            
            if (user != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void FirstUseAction()
        {
            try
            {
                if (CheckIfUserIsNew() == true)//check if user is new
                {
                    Welcome welcome = new Welcome();
                    welcome.UserNameEntered += (string name) => dao.SaveUserData(name);//save the user's name at first use
                    welcome.ShowDialog();
                }
                else
                {
                    LoadFriends();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void AddFrienButton_Click(object sender, RoutedEventArgs e)
        {
            AddAFriend add = new AddAFriend();
            add.FriendAdded += OnFriendAdded;
            add.ShowDialog();
        }
        private void OnFriendAdded(string name,string IP, string pic)
        {
            try
            {
                dao.SaveFriend(name, IP, pic);
                FriendListBox.Items.Clear();
                LoadFriends();
                SetFriendsOnListBox();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.StackTrace+"\n"+e.Message);
            }
        }
            
        private void OnIPAddressSet(object s, string ip)
        {
            IPLabel.Content = "Your IP address is: "+ip;
        }

        private void FriendListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox list = sender as ListBox;
            if (ChatListBox.Items != null)
            {
                ChatListBox.Items.Clear();

                foreach (Friend f in friends)
                {
                    if (f.ToString() == list.SelectedItem.ToString())
                    {
                        presentFriend = f;
                    }
                }

                ChatIPLabel.Content = presentFriend.IP;
                ChatNameLabel.Content = presentFriend.Name;
                LoadButton.IsEnabled = true;
                try
                {
                    //MessageBox.Show(presentFriend.IP);
                    client.StartClient(presentFriend.IP, 80);
                    if (client.InnerTcpClient.Connected)
                    {
                        ConnectionNotifier.Fill = (ImageBrush)FindResource("GreenBall");
                        StatusLabel1.Content = presentFriend.Name + " is connected.";
                    }
                    else
                    {
                        ConnectionNotifier.Fill = (ImageBrush)FindResource("redBall");
                        StatusLabel1.Content = presentFriend.Name + " is not connected.";
                    }
                }
                catch (Exception ex)
                {
                    ConnectionNotifier.Fill = (ImageBrush)FindResource("RedBall");
                    StatusLabel1.Content = presentFriend.Name + " is not connected.";
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadButton.IsEnabled = false;
            try
            {
                List<string> chats = dao.RetriveConversations(ChatIPLabel.Content as string);

                string msg;
                foreach (string message in chats)
                {
                    if (IsUserChat(message))
                    {
                        msg = RemoveChatsign(message);
                        ChatListBox.Items.Add(new
                        {
                            Text = msg,
                            ForeColor = "Black",
                            BackColor = "White"
                        });
                    }
                    else
                    {
                        msg = RemoveChatsign(message);
                        ChatListBox.Items.Add(new
                        {
                            Text = msg,
                            ForeColor = "White",
                            BackColor = "Black"
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool IsUserChat(string message)
        {
            if (message[0] == 'U')
            {
                return true;
            }
            else
                return false;
        }
        public string RemoveChatsign(string message)//remove the identifier that determines if the message is from the user or his friend
        {
            int i = message.IndexOf('#');
            StringBuilder builder = new StringBuilder();
            for(i = i+1 ;i<message.Length;i++)
            {
                builder.Append(message[i]);
            }
            return builder.ToString();
        }

        private void CreateConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            CreateConnectionWindow createCon = new CreateConnectionWindow();
            createCon.HotspotStarts += ConnectionCreated;

            createCon.HotspotStops += (string ssid, string psw) => MessageBox.Show(
                "Your wifi network \""+ssid+"\" was stopped succesfully", "info",
                MessageBoxButton.OK, MessageBoxImage.Information);
            createCon.Show();
        }

        private void ConnectionCreated(string ssid, string psw)
        {
            //thi scoed works perfectly but I dont need its implementation yet
            if(ssid != "")
            {
                MessageBox.Show(
                "Your wifi network \"" + ssid + "\" was created succesfully", "info",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (ChatTextBox.Text != "")
                    {
                        String message = ChatTextBox.Text;
                        if (IsSpecialCode(message))//test for special code
                        {
                            if (RemoveCode(ChatTextBox.Text) == "Resets")
                            {
                                ResetApp();
                            }
                        }
                        else
                        {
                            if (presentFriend != null)
                            {
                                if (client.InnerTcpClient.Connected == false)
                                {
                                    client.StartClient(presentFriend.IP, 80);
                                }
                                client.Chat(message);
                                dao.SaveConversation(presentFriend.IP, message, true);
                                ChatListBox.Items.Add(new
                                {
                                    Text = message,
                                    ForeColor = "Black",
                                    BackColor = "White"
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool IsSpecialCode(string code)
        {
            if (code[0] == '#')
            {
                if (code[1] == 'M')
                {
                    if (code[5] == '#')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private string RemoveCode(string text)
        {
            StringBuilder code = new StringBuilder();
            int i = text.LastIndexOf('#')+1;
            int j = text.IndexOf("$");
            for (; i < j; i++)
            {
                code.Append(text[i]);
            }
            return code.ToString();
        }
        private void ResetApp()
        {
            dao.DeleteAllData();
            this.Close();
        }

        private void FriendListBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listB = sender as ListBox;
            ContextMenu contextMenu = this.FindResource("FriendListBoxContextMenu") as ContextMenu;
         
            contextMenu.PlacementTarget = listB;
            contextMenu.IsOpen = true;

            if (listB.SelectedItem != null)
            {
                selectedFriend = listB.SelectedItem.ToString();
            }
        }
        private void DeleteFriendClick(object sender, RoutedEventArgs e)
        {
            if (selectedFriend != "none")
            {
                MessageBoxResult result = MessageBox.Show("Doing this will delete"
                    + "this friend completely and his chats.", "Warning",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        string ip = GetFriendIPFromFriendToString(selectedFriend);
                         
                        dao.DeleteAFriend(ip);
                        LoadFriends();
                        FriendListBox.Items.Clear();
                        SetFriendsOnListBox();
                    }
                    catch(Exception ex)
                    {
                        LoadFriends();
                        FriendListBox.Items.Clear();
                        SetFriendsOnListBox();
                    }
                }
            }
            else
            {
                //MessageBox.Show("Mera");
            }
            selectedFriend = "none";
        }
        private void EditFriendClick(object sender, RoutedEventArgs e)
        {
            if (selectedFriend != "none")
            {
                EditFriend edit = new EditFriend();
                edit.FriendEdited += EditFriend;
                edit.ShowDialog();
            }
            else
            {
                //MessageBox.Show("Mera");
            }
            selectedFriend = "none";
        }
        private void EditFriend(string name, string IP, string pic)
        {
            try
            {
                //this.Cursor = Cursors.Wait;
                string previousIP = GetFriendIPFromFriendToString(selectedFriend);

                if (IP != "")
                {
                    dao.ChangeFriendIP(previousIP, IP);
                    previousIP = IP;
                }
                if (name != "")
                {
                    dao.ChangeFriendName(previousIP, name);
                }
                if (pic != "")
                {
                    dao.ChangeFriendPicture(previousIP, pic);
                }
                LoadFriends();
                FriendListBox.Items.Clear();
                SetFriendsOnListBox();
                //this.Cursor = Cursors.Arrow;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private string GetFriendIPFromFriendToString(string txt)
        {
            int i = txt.LastIndexOf(':')+2;
            StringBuilder build = new StringBuilder();
            while(i<txt.Length)
            {
                build.Append(txt[i]);
                
                i++;
            }
            return build.ToString();
        }
        private void OnIPAddressChanged(string IP)
        {
            IPLabel.Dispatcher.Invoke(new Action (() => IPLabel.Content = "Your IP Address is: "+IP));
        }
        private void OnServerMessageRecieved(object s, TcpClient client, string message)
        {
            try
            {
                if (CheckFriendShip(client) == true)
                {
                    if (presentFriend != null)
                    {
                        if (presentFriend.IP == GetIP(client.Client.RemoteEndPoint.ToString()))
                        {
                            ChatListBox.Dispatcher.Invoke(new Action(() =>
                            ChatListBox.Items.Add(new
                            {
                                Text = message,
                                ForeColor = "White",
                                BackColor = "Black"
                            })));
                            dao.SaveConversation(presentFriend.IP, message, false);
                            player.Play();
                        }
                        else
                        {
                            MessageBox.Show(GetFriendFromClient(client).Name + "says: " + message);
                            dao.SaveConversation(presentFriend.IP, message, false);
                            player.Play();
                        }
                    }
                    else
                    {
                        MessageBox.Show(GetFriendFromClient(client).Name + "says: " + message);
                        dao.SaveConversation(presentFriend.IP, message, false);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OnserverErrorOccured(object s, Exception e)
        {
            if (e.Message != "A blocking operation was interrupted by a call to WSACancelBlockingCall")
            {
                if (e.Message != "An invalid IP address was specified.")
                {
                    MessageBox.Show(e.Message + " server" + e.StackTrace);
                    //Do nothing...
                }
            }
        }
        private void ServerClientDisconnected(object s, TcpClient client)
        {

        }
        private void ServerClientConnected(object s, TcpClient client)
        {
            if(CheckFriendShip(client)!=true)
            {
               MessageBoxResult result = MessageBox.Show("A person with IP: "
                   +GetIP(client.Client.RemoteEndPoint.ToString())
                        +" Is trying to chat with you", "Notice", MessageBoxButton.YesNo,
                            MessageBoxImage.Information );
                if(result == MessageBoxResult.No)
                {
                    try
                    {
                        client.Close();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        private void ClientErrorOccured(object s, Exception e)
        {
            MessageBox.Show(e.Message);
        }
        private void ClientConnected(object s, string IP, int port)
        {

        }
        private void ClientMessageSent(object s, string message)
        {

        }
        private string GetIP(string IP)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < IP.Length; i++)
            {
                if (IP[i] != ':')
                {
                    builder.Append(IP[i]);
                }
                else
                {
                    break;
                }
            }
            return Convert.ToString(builder);
        }
        private bool CheckFriendShip(TcpClient client)//check if the incomming client is the user's friend
        {
            foreach(Friend f in friends)
            {
                if(GetIP(client.Client.RemoteEndPoint.ToString())==f.IP)
                {
                    return true;
                }
            }
            return false;
        }
        private Friend GetFriendFromClient(TcpClient client)//finds if a friend with this client exists and returns it
        {
            foreach(Friend fr in friends)
            {
                if(fr.IP==GetIP(client.Client.RemoteEndPoint.ToString()))
                {
                    return fr;
                }
            }
            return null;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            player.Stop();
        }
    }
}
