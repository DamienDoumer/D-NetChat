using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetChatDataAccesors;
using System.Windows;
using System.IO;
using NetChatDataAccesors;

namespace DoumeraNetChat.Friends
{
    class Friend
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public string PicturePath { get; set; } = Environment.CurrentDirectory+"\\Default face.png";
        public Chat chat;
        private NetChatDao dao = NetChatDao.Instance;


        public Friend(string name, string ip)
        {
            Name = name;
            IP = ip;
            chat = new Chat(IP);
        }
        public Friend(string name, string ip, string picture)
        {
            if (TestIfPicExists(picture))
            {
                Name = name;
                IP = ip;
                PicturePath = picture;
                chat = new Chat(IP);
            }
            else
            {
                Name = name;
                IP = ip;
                chat = new Chat(IP);
            }
        }
        public void Error(object sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }
        public void ChangeIP(string newIP)
        {
            dao.ChangeFriendIP(IP, newIP);
        }
        public void ChangeFriendName(string newName)
        {
            dao.ChangeFriendName(IP, newName);
        }
        public void ChangeFriendPicture(string newPic)
        {
            dao.ChangeFriendPicture(IP, newPic);
        }
        public void SetPicToDefault()
        {
            PicturePath = Environment.CurrentDirectory + "\\Default face.png";
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Name: " + Name + "\n" + "IP: " + IP);
            return builder.ToString();
        }
        public void Save()
        {
            dao.SaveFriend(Name, IP, PicturePath);
        }
        public bool TestIfPicExists(string picPath)
        {
            if(File.Exists(picPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
