using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetChatDataAccesors;
using System.Windows;
using NetChatDataAccesors;

namespace DoumeraNetChat.Friends
{
    class Chat
    {
        public string IP { get; }
        NetChatDao dao ;

        public Chat(string ip)
        {
            IP = ip;
            dao = NetChatDao.Instance;
            
        }
        public List<string> GetMessages()
        {
            return dao.RetriveConversations(IP);
        } 
        public List<string> GetMessageTime()
        {
            return dao.RetrieveConversationDateTime(IP);
        }
        public void SaveChat(string message, bool isUser)
        {
            dao.SaveConversation(IP, message, isUser);
        }
        public void Error(object sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
