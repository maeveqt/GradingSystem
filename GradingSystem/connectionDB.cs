using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace GradingSystem
{
    class connectionDB
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public void dbconnect()
        {

            server = "localhost";
            database = "csgradedb";
            uid = "root";
            password = "";
            string connectionString;
            //SERVER = localhost
            //DATABASE = csharpDB
            //UID = root
            //PASSWORD = ""
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";"
                + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

        }

        public bool OpenConnection()
        {
            try
            {

                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {


                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:

                        MessageBox.Show("Cannot connect to server. Contact the administrator");

                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }


        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }




    }
}

