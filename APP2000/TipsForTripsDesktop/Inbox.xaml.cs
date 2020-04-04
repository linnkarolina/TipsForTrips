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
using System.Data;
using System.Windows.Media.Animation;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Page
    {

        private string searchText;

        public Inbox()
        {
            InitializeComponent();
            searchText = Search_Bar.Text;
        }

        public void InboxTable()
        {
            DataTable dt = new DataTable();
            DataColumn from = new DataColumn("From", typeof(string));
            DataColumn subject = new DataColumn("Subject", typeof(string));
            DataColumn Received = new DataColumn("Received", typeof(string));
            
            dt.Columns.Add(from);
            dt.Columns.Add(subject);
            dt.Columns.Add(Received);

            for (int i = 0; i < DatabaseCount("SELECT count(*) FROM admin_inbox;"); i++)
            {
                DataRow row = dt.NewRow();
                row[0] = ConnectToDatabase("SELECT user_username FROM admin_inbox LIMIT " + i + ",1;");
                row[1] = ConnectToDatabase("SELECT subject FROM admin_inbox LIMIT " + i + ",1;");
                row[2] = ConnectToDatabase("SELECT time_sent FROM admin_inbox LIMIT " + i + ",1;"); 
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

        public void New_Message_Click(object sender, RoutedEventArgs e)
        {
            /*New_Web_User nwe = new New_Web_User(this);
            nwe.Show();
            nwe.Topmost = true;*/
        }

        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            InboxTable();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn from = new DataColumn("From", typeof(string));
            DataColumn subject = new DataColumn("Subject", typeof(string));
            DataColumn Received = new DataColumn("Received", typeof(string));

            dt.Columns.Add(from);
            dt.Columns.Add(subject);
            dt.Columns.Add(Received);

            if (ConnectToDatabase("SELECT user_username FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%';") != "")
            {
                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%';"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT user_username FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[1] = ConnectToDatabase("SELECT subject FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
                    row[2] = ConnectToDatabase("SELECT time_sent FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' LIMIT " + i + ",1;");
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
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;
                searchText = Search_Bar.Text;
                MessageBox.Show("No results were found", "Oops...");
            }
        }

        public void Open_Click(object sender, RoutedEventArgs e)
        {/*
            try
            {
                DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
                String username = drv[0].ToString();
                String password = drv[1].ToString();
                String location = drv[2].ToString();
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
            }*/
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {/*
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            String username = drv[0].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + username + "?", "Delete user", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        MessageBox.Show(username + " was deleted.", "Delete user");
                        ConnectToDatabase("DELETE FROM admin_inbox WHERE user_username = '" + username + "';");
                        InboxTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Phew, " + username + " will live to see the light another day!", "Delete user");
                    break;
            }*/
        }

            private void Search_MouseUp(object sender, RoutedEventArgs e)
        {
            if (Search_Bar.Text.Equals(searchText))
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
            InboxTable();
        }
    }
}
