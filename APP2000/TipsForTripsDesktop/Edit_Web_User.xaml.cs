﻿using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Edit_Web_User.xaml
    /// </summary>
    public partial class Edit_Web_User : Window
    {

        private string username;

        public Edit_Web_User(string user)
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

            query = "SELECT password FROM user WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Password.Text = Name;

            query = "SELECT full_name FROM user WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Full_Name.Text = Name;

            query = "SELECT email FROM user WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Email.Text = Name;

            query = "SELECT location FROM user WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Location.Text = Name;

            query = "SELECT phone_NR FROM user WHERE username = '" + username + "';";
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

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string user = Username.Text;
                string password = Password.Text;
                string full_name = Full_Name.Text;
                string email = Email.Text;
                string location = Location.Text;
                string phone_NR = Phone_Number.Text;
                string query = "UPDATE user SET username = '" + user + "', password = '" + password + "', full_name = '" + full_name + "', email = '" + email + "', location = '" +
                    location + "', phone_NR = '" + phone_NR + "' WHERE username = '" + username + "' ;";
                ConnectToDatabase(query);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
