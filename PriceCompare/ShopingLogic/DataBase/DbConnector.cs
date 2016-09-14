using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ShopingLogic.DataBase
{
    public class DbConnector
    {
        public MySqlConnection Connection { get; private set; }
        private string server;
        private string dataBase;
        private string password;
        private string uid;


        public DbConnector()
        {
            Initialize();
        }
        

        private void Initialize()
        {
            server = "localhost";
            dataBase = "shopingCart";
            uid = "root";
            password = "Shizuka25@";
            StringBuilder connectionComand = new StringBuilder();
            connectionComand.AppendFormat("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", server, dataBase, uid, password);
            Connection = new MySqlConnection(connectionComand.ToString());          
       }

        public void OpenConnection()
        {
            try
            {
                Connection.Open();
               
            }
            catch(MySqlException e)
            {
               
            }
        }

        public void CloseConnection()
        {
            try
            {
                Connection.Close();
              
            }
            catch(MySqlException e)
            {
               
            }
        }

        public void Insert(string query)
        {
                OpenConnection();            
                MySqlCommand command = new MySqlCommand(query,Connection);
                command.ExecuteNonQuery();
                CloseConnection();            
        }


    }
}
