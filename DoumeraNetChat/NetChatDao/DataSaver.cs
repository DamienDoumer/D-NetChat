using System;
using System.Text;
using System.Data.SQLite;

namespace NetChatDataAccesors
{
    class DataSaver
    {
        private String dataBase;
        private String table;
        private String[] attributes;
        private String[] attributeValues;
        private string attribute;
        public SQLiteConnection connect;
        public SQLiteCommand command;

        public String Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }
        public String Database
        {
            get { return dataBase; }
            set { this.dataBase = value; }
        }
        public String Table
        {
            get { return table; }
            set { table = value; }
        }

        public string[] getAttributes()
        {
            return attributes;
        }

        public string[] getAttributeValues()
        {
            return attributeValues;
        }

        public void setAttributeValues(string[] val)
        {
            attributeValues = val;
        }

        public void setAttributes(string[] val)
        {
            attributes = val;
        }
        public DataSaver()
        {
            dataBase = "NetChatDatabase.db";
            table = null;
            attributes = null;
            attributeValues = null;
            connect = new SQLiteConnection("Data Source = " + dataBase + "; version = 3;");
            command = new SQLiteCommand();
        }

        public void SaveData()
        {
            try
            {
                string txtAttrib = "Insert into " + table + " " + CreateStringAttribute("( ", attributes, " ) ");
                string txtValues = "Values " + CreateStringAttributeValues("( ", attributeValues, ");");
                connect.Open();
                command.CommandText = txtAttrib + txtValues;
                command.Connection = connect;
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResetDatabase()
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "delete from " + table;
                command.ExecuteNonQuery();
                //Console.WriteLine("Database succesfully reseted");
                connect.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string CreateStringAttributeValues(string start, string[] val, string end)
        {
            StringBuilder build = new StringBuilder();
            int v = 0;

            build.Append(start);
            if (int.TryParse(val[0], out v))//test if the value is an int
            {
                build.Append(val[0] + ", ");
            }
            else
            {
                build.Append("'" + val[0] + "', ");
            }

            for (int i = 1; i < val.Length; i++)
            {
                if (int.TryParse(val[i], out v)) //test if value is an integer
                {
                    if (i == val.Length - 1)
                    {
                        build.Append(val[i]);
                    }
                    else
                    {
                        build.Append(val[i] + ", ");
                    }
                }
                else
                {
                    if (i == val.Length - 1)
                    {
                        build.Append("'" + val[i] + "'");
                    }
                    else
                    {
                        build.Append("'" + val[i] + "', ");
                    }
                }
            }

            build.Append(end);
            return Convert.ToString(build);
        }

        private string CreateStringAttribute(string start, string[] val, string end)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(start);
            for (int i = 0; i < val.Length; i++)
            {
                if (i == val.Length - 1)
                {
                    builder.Append(val[i]);
                }
                else
                {
                    builder.Append(val[i] + ", ");
                }
            }

            builder.Append(end);
            return builder + "";
        }

        public int CountNumRows()
        {
            int numRows1 = 0;
            try
            {

                command.CommandText = "select count(*) from" + table + ";"; //command to be executed
                command.Connection = connect;
                connect.Open();
                SQLiteDataReader reader = command.ExecuteReader(); //starts the reader

                while (reader.Read())
                {
                    numRows1 = Convert.ToInt32(reader[0]); //puts the number of rows in a variable
                }
                reader.Close();
                connect.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            return numRows1;
        } //the database will close automatically since u are using 
    }
}
