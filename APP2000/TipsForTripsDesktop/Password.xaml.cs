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
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : Page
    {
        private string user;
        public Password(string username) {
            user = username;
            InitializeComponent();
        }

        // Button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Cur_Pass = Current_Password.Text;
            string New_Pass = New_Password.Text;
            string Verify_Pass = Verify_Password.Text;
            string query = "SELECT password FROM admin WHERE username='" + user + "';";

            if (ConnectToDatabase(query) != Cur_Pass)
            {
                Success.DataContext = "";
                ERROR.DataContext = "The current password is incorrect.";
            }
            else if (New_Pass != Verify_Pass)
            {
                Success.DataContext = "";
                ERROR.DataContext = "The new passwords don't match.";
            }
            else if (ConnectToDatabase(query) == New_Pass)
            {
                Success.DataContext = "";
                ERROR.DataContext = "You cannot change to your current password.";
            }
            else if(Cur_Pass == "" || New_Pass == "" || Verify_Pass == "")
            {
                Success.DataContext = "";
                ERROR.DataContext = "You have to enter text in all fields.";
            }
            else
            {
                query = "UPDATE admin set password='" +New_Pass+ "' WHERE username='" + user + "';";
                ConnectToDatabase(query);
                ERROR.DataContext = "";
                Success.DataContext = "Password updated successfully!";
            }
            
        }

        // Database connection
        public string ConnectToDatabase(string query)
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
