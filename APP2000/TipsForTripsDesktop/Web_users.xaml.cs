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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Media.Animation;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Web_users.xaml
    /// </summary>
    public partial class Web_users : Page
    {

        private string searchText;

        public Web_users()
        {
            InitializeComponent();
            searchText = Search_Bar.Text;
        }

        public void UserTable()
        {
            DataTable dt = new DataTable();
            DataColumn username = new DataColumn("Username", typeof(string));
            DataColumn password = new DataColumn("Password", typeof(string));
            DataColumn city = new DataColumn("City", typeof(string));
            DataColumn email = new DataColumn("E-mail", typeof(string));
            DataColumn full_name = new DataColumn("Full name", typeof(string));
            DataColumn phone_NR = new DataColumn("Phone", typeof(string));

            dt.Columns.Add(username);
            dt.Columns.Add(password);
            dt.Columns.Add(city);
            dt.Columns.Add(email);
            dt.Columns.Add(full_name);
            dt.Columns.Add(phone_NR);

            for (int i = 0; i < DatabaseCount("SELECT count(*) FROM user;"); i++)
            {
                DataRow row = dt.NewRow();
                row[0] = ConnectToDatabase("SELECT username FROM user LIMIT " + i + ",1;");
                row[1] = ConnectToDatabase("SELECT password FROM user LIMIT " + i + ",1;");
                row[2] = ConnectToDatabase("SELECT city FROM user LIMIT " + i + ",1;");
                row[3] = ConnectToDatabase("SELECT email FROM user LIMIT " + i + ",1;");
                row[4] = ConnectToDatabase("SELECT full_name FROM user LIMIT " + i + ",1;");
                row[5] = ConnectToDatabase("SELECT phone_NR FROM user LIMIT " + i + ",1;");
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;
            }
        }

        // Enter animation
        private void Button_Enter(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#4296d6");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#6caddf");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6caddf"));
            b.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
        }

        // Leave animation
        private void Button_Leave(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#6caddf");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#4296d6");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4296d6"));
            b.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
        }

        public void New_User_Click(object sender, RoutedEventArgs e)
        {
            New_Web_User nwe = new New_Web_User(this);
            nwe.Show();
            nwe.Topmost = true;
        }

        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UserTable();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn username = new DataColumn("Username", typeof(string));
            DataColumn password = new DataColumn("Password", typeof(string));
            DataColumn city = new DataColumn("City", typeof(string));
            DataColumn email = new DataColumn("E-mail", typeof(string));
            DataColumn full_name = new DataColumn("Full name", typeof(string));
            DataColumn phone_NR = new DataColumn("Phone", typeof(string));

            dt.Columns.Add(username);
            dt.Columns.Add(password);
            dt.Columns.Add(city);
            dt.Columns.Add(email);
            dt.Columns.Add(full_name);
            dt.Columns.Add(phone_NR);
            if (ConnectToDatabase("SELECT username FROM user WHERE username LIKE '%" + Search_Bar.Text + "%';") != "")
            {
                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM user WHERE username LIKE '%" + Search_Bar.Text + "%';"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT username FROM user WHERE username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[1] = ConnectToDatabase("SELECT password FROM user WHERE username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[2] = ConnectToDatabase("SELECT city FROM user WHERE username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[3] = ConnectToDatabase("SELECT email FROM user WHERE username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[4] = ConnectToDatabase("SELECT full_name FROM user WHERE username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[5] = ConnectToDatabase("SELECT phone_NR FROM user WHERE username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    dt.Rows.Add(row);
                    Table.ItemsSource = dt.DefaultView;
                    searchText = Search_Bar.Text;
                }
            }
            else
            {
                DataRow row = dt.NewRow();
                row[0] = "";
                row[1] = "";
                row[2] = "";
                row[3] = "";
                row[4] = "";
                row[5] = "";
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;
                searchText = Search_Bar.Text;
                MessageBox.Show("No results were found","Oops...");
            }
        }

        public void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
                String username = drv[0].ToString();
                String password = drv[1].ToString();
                String city = drv[2].ToString();
                String email = drv[3].ToString();
                String full_name = drv[4].ToString();
                String phone_NR = drv[5].ToString();
                Edit_Web_User ewu = new Edit_Web_User(username, this);
                ewu.Show();
                ewu.Topmost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            String username = drv[0].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + username + "?", "Delete user", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        MessageBox.Show(username + " was deleted.", "Delete user");
                        ConnectToDatabase("DELETE FROM user WHERE username = '" + username + "';");
                        UserTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Phew, " + username + " will live to see the light another day!", "Delete user");
                    break;
            }
        }

        private void Search_MouseUp(object sender, RoutedEventArgs e)
        {
            if(Search_Bar.Text.Equals(searchText))
            {
                Search_Bar.Text = "";
            }
        }

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

        public int DatabaseCount(string query)
        {
            int total;

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
                total = Convert.ToInt32(queryResult);
            }
            else
            {
                // Else make id = "" so you can later check it.
                total = 0;
            }

            MyCon.Close();

            return total;
        }

        private void Table_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserTable();
        }
    }
}
