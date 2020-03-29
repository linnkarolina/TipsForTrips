using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Basic_info.xaml
    /// </summary>
    public partial class Basic_info : Page
    {
        private string username;

        public Basic_info(string user)
        {
            username = user;
            InitializeComponent();
            setTextBoxContent();
        }

        private void setTextBoxContent()
        {
            string query = "SELECT username FROM user WHERE username = '" + username + "';";
            string Name = ConnectToDatabase(query);
            Username.Text = Name;

            query = "SELECT full_name FROM user WHERE username = 'raneik';";
            Name = ConnectToDatabase(query);
            Full_Name.Text = Name;

            query = "SELECT email FROM user WHERE username = 'raneik';";
            Name = ConnectToDatabase(query);
            Email.Text = Name;

            query = "SELECT location FROM user WHERE username = 'raneik';";
            Name = ConnectToDatabase(query);
            Location.Text = Name;

            query = "SELECT phone_NR FROM user WHERE username = 'raneik';";
            Name = ConnectToDatabase(query);
            Phone_Number.Text = Name;
        }

        /// <summary>
        /// Database connection
        /// </summary>
        private string ConnectToDatabase(string query)
        {
            string name;

            // Azure connection
            /* 
            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");
            */

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3308;DATABASE=TipsForTrips;UID=root;PASSWORD=");
            MySqlCommand cmd = new MySqlCommand(query, MyCon);
            MyCon.Open();
            var queryResult = cmd.ExecuteScalar(); //Return an object so first check for null
            if (queryResult != null)
            {
                // If we have result, then convert it from object to string.
                name = Convert.ToString(queryResult);
            }
            else
            {
                // Else make id = "" so you can later check it.
                name = "";
            }

            MyCon.Close();

            return name;
        }
    }
}
