using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace NetChatDataAccesors
{
    class NetChatDao : DataSaver
    {//uses the singleton pattern
        private static NetChatDao netChatDao;
        public SQLiteConnection Connection { get { return connect; } }

        static NetChatDao()//static ctor to get only one instance
        {
            netChatDao = new NetChatDao();
        }

        public static NetChatDao Instance { get { return netChatDao; } }//to get only one instance

        private NetChatDao()//Private ctor to hinder creation of this object outside
        {
            Database = "NetChatDatabase.db";
            connect = new SQLiteConnection("Data Source = " + Database + "; version = 3;");
            command = new SQLiteCommand();
        }

        public List<string> GetAllFriendIP()
        {
            if (CountNumberOfRows("Friends") != 0)
            {
                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection("DataSource = " + Database + "; Version = 3;"))
                    {
                        using (SQLiteCommand command = new SQLiteCommand())
                        {
                            string Querry;
                            //int v = 0;
                            Querry = "select FriendIP from Friends";
                            connection.Open();
                            command.Connection = connection;
                            command.CommandText = Querry;
                            //command.ExecuteNonQuery();
                            SQLiteDataReader reader = command.ExecuteReader();
                            List<string> values = new List<string>();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    //values.Add(Convert.ToString(reader["FriendIP"]));
                                    //IF the above doesnt work try 
                                    //reader.GetString(v);
                                    values.Add(reader[0].ToString());
                                }
                                reader.Close();
                            }
                            return values;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    return null;
                }
            }
            else
            {
                var l = new List<string>();
                l.Add("You have no friend yet");
                return l;
            }
        }

        public string GetUserName()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("DataSource = " + Database + "; Version = 3;"))
                {
                    using (SQLiteCommand command = new SQLiteCommand())
                    {
                        string Querry;

                        Querry = "select UserName from User";
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = Querry;
                        command.ExecuteNonQuery();
                        SQLiteDataReader reader = command.ExecuteReader();
                        string values;
                        reader.Read();
                        values = "" + reader[0];
                        return values;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "No current row")
                {
                    return "";//returns this if the user has no data stored
                }
                else
                {
                    throw ex;
                }
            }
        }

        public void DeleteAllChats(string friendIP)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("DataSource = " + Database + "; Version = 3;"))
                {
                    using (SQLiteCommand command = new SQLiteCommand())
                    {
                        connection.Open();
                        string Table = "Chat";
                        string Querry = "Delete From " + Table + " where FriendIP = " + friendIP;
                        command.Connection = connection;
                        command.CommandText = Querry;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveFriendWithoutPic(string FriendName, string FriendIP)
        {
            setAttributes(new string[] { "FriendIP", "FriendName" });
            setAttributeValues(new string[] { FriendIP, FriendName });

            Table = "Friends";
            SaveData();
        }

        public void SaveConversation(string friendIP, string message, bool isUser)
        {
            if (isUser)//if it is the user chatting
            {
                message = "User# " + message;
                setAttributes(new string[] { "FriendIP", "Date_Time", "Messages" });
                setAttributeValues(new string[] { friendIP, Convert.ToString(DateTime.Now), message });
                Table = "Chat";
                message = null;
                SaveData();
            }
            else
            {
                message = "Friend# " + message;
                setAttributes(new string[] { "FriendIP", "Date_Time", "Messages" });
                setAttributeValues(new string[] { friendIP, Convert.ToString(DateTime.Now), message });
                Table = "Chat";
                message = null;
                SaveData();
            }
        }

        public void SaveFriend(string FriendName, string FriendIP, string picturePath = "Icons\\Default face.ico")//works
        {
            //connect.Close();
            setAttributes(new string[] { "FriendIP", "FriendName", "PicturePath" });
            setAttributeValues(new string[] { FriendIP, FriendName, picturePath });

            Table = "Friends";
            SaveData();
            //connect.Open();
            //command.Connection = connect;
            //command.CommandText = "Insert into Friends (FriendIP, FriendName, PicturePath) value ('" + FriendIP + "', '" +
            //    FriendName + "', '" + picturePath + "' );";
            //command.ExecuteNonQuery();
            //connect.Close();

        }

        public int CountNumberOfRows(string Table)
        {
            int num = 0;
            try
            {
                connect.Open();
                command.Connection = connect;
                string query = "select count(*) from " + Table + " ;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                num = Convert.ToInt32(reader[0]);
                reader.Close();
                connect.Close();

                return num;


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteAllData()
        {
            this.Table = "Chat";
            this.ResetDatabase();
            this.Table = "User";
            this.ResetDatabase();
            this.Table = "Friends";
            this.ResetDatabase();
        }

        public List<String> RetrieveConversationDateTime(string friendIP)
        {
            List<string> result = new List<string>();
            if (CountNumberOfRows("Chat") != 0)
            {
                try
                {
                    connect.Open();
                    string query = "Select Date_Time From Chat where FriendIP = '" + friendIP + "' order by Date_Time";
                    //Console.WriteLine(query);
                    command.CommandText = query;
                    command.Connection = connect;
                    command.ExecuteNonQuery();
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                    connect.Close();
                    reader.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                    result.Add("");
                    return result;
                }
            }
            else
            {
                var l = new List<string>();
                l.Add("You have no chats yet.");
                return l;
            }
        }

        public List<String> RetriveConversations(string FriendIP)
        {
            List<String> result = new List<string>();
            if (CountNumberOfRows("Chat") != 0)
            {
                try
                {

                    connect.Open();
                    string query = "Select Messages From Chat where FriendIP = '" + FriendIP + "' order by Date_Time";
                    command.CommandText = query;
                    command.Connection = connect;
                    command.ExecuteNonQuery();
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                    reader.Close();
                    connect.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                    //result.Add("");
                }
            }
            else
            {
                throw new Exception("You dont have any chat yet.");
             }
        }

        public void ChangeFriendName(string friendIP, string newName)
        {
            try
            {
                if (RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP) != null)//checks if the friend exist
                {
                    connect.Open();
                    Table = "Friends";
                    string Querry = "Update " + Table + " Set FriendName = '" + newName + "' where FriendIP = '" + friendIP + "' ;";

                    command.Connection = connect;
                    command.CommandText = Querry;
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "SQL logic error or missing database" + Environment.NewLine + "unrecognized token: \"" + friendIP + "\"")
                {
                    throw new Exception("You don't have a friend with the IP address: " + friendIP);
                    
                }
                else
                {
                    throw ex;
                }
            }
        }

        public void ChangeFriendPicture(string friendIP, string picturePath)
        {
            try
            {

                if (RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP) != null)//checks if the friend exist
                {
                    connect.Open();
                    Table = "Friends";
                    string Querry = "Update " + Table + " Set PicturePath = '" + picturePath + "' where FriendIP = '" + friendIP + "' ;";

                    command.Connection = connect;
                    command.CommandText = Querry;
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "SQL logic error or missing database" + Environment.NewLine + "unrecognized token: \"" + friendIP + "\"")
                {
                    throw new Exception("You don't have a friend with the IP address: " + friendIP);
                }
                else
                {
                    throw ex;
                }
            }
        }

        private void DeleteAllUserData()
        {
            try
            {

                connect.Open();
                command.CommandText = "delete from User;";
                command.Connection = connect;
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string RetrieveSingleData(string table, string attributeToBeRetrieved,
            string conditionAttribute, string conditionAttributeValue)
        {
            string result = null;
            try
            {
                string query = "Select " + attributeToBeRetrieved + " from " + table + " Where " + conditionAttribute + " = '" +
                    conditionAttributeValue + "';";
                connect.Open();
                command.Connection = connect;
                command.CommandText = query;
                command.ExecuteNonQuery();
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                result = (string)reader[attributeToBeRetrieved];
                reader.Close();
                connect.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == "No current row")
                {
                    throw new Exception("Sorry, but " + conditionAttributeValue + " was not found.");
                }
                else
                {
                    throw ex;
                }
            }
        }

        public List<string> GetFriend(string friendIP)
        {
            List<string> list = new List<string>();
            if (RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP) != null)//tests if the friend's IP exists
            {
                list.Add(RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP));
                list.Add(RetrieveSingleData("Friends", "FriendName", "FriendIP", friendIP));
                list.Add(RetrieveSingleData("Friends", "PicturePath", "FriendIP", friendIP));
                return list;
            }
            else
            {
                return null;
            }
        }

        public void ChangeFriendIP(string friendIP, string newIP)
        {
            try
            {
                if (RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP) != null)//checks if the friend exist
                {
                    connect.Open();
                    Table = "Friends";
                    string Querry = "Update " + Table + " Set FriendIP = '" + newIP + "' where FriendIP = '" + friendIP + "' ;";

                    command.Connection = connect;
                    command.CommandText = Querry;
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "SQL logic error or missing database" + Environment.NewLine + "unrecognized token: \"" + friendIP + "\"")
                {
                    throw new Exception("You don't have a friend with the IP address: " + friendIP);
                }
                else
                {
                    throw ex;
                }
            }
        }

        private void DeleteAllChatsofAFriend(string friendIP)
        {
            try
            {
                if (RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP) != null)//checks if the friend exist
                {
                    connect.Open();
                    Table = "Chat";
                    string Querry = "Delete From " + Table + " where FriendIP = '" + friendIP + "';";
                    command.Connection = connect;
                    command.CommandText = Querry;
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAFriend(string friendIP)
        {
            try
            {
                DeleteAllChatsofAFriend(friendIP);//deletes all the chats this friend has had with the user
                                                      //connect.Close();
                Table = "Friends";
                if (RetrieveSingleData("Friends", "FriendIP", "FriendIP", friendIP) != null)//checks if the friend exist
                {
                    connect.Open();
                    string Querry = "Delete From " + Table + " Where FriendIp = '" + friendIP + "';";
                    //Console.WriteLine(Querry);
                    command.Connection = connect;
                    command.CommandText = Querry;
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUserData(string Name)//works
        {
            try
            {
                //DeleteAllUserData();//delete all user data before storing so there will always be one user
                if (CountNumberOfRows("User") != 0)
                {
                    DeleteAllUserData();
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "Insert into User ( UserName ) Values ( '" + Name + "' );";
                    command.ExecuteNonQuery();
                    connect.Close();
                }
                else
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "Insert into User ( UserName ) Values ( '" + Name + "' );";
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            //SaveData();
        }
        public void ChangeSoundPath(string path)
        {
            try
            {
                if (CountNumberOfRows("Sound") != 0)
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "Update Sound set Path = '" + path + "' ;";
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string RetrieveSoundPath()
        {
            string result = null; ;
            try
            {
                if (CountNumberOfRows("Sound") != 0)
                {
                    string query = "Select * from Sound;";
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    SQLiteDataReader reader = command.ExecuteReader();
                    reader.Read();
                    result = (string)reader["Path"];
                    reader.Close();
                    connect.Close();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
