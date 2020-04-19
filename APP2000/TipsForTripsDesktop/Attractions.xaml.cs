using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Attractions.xaml
    /// </summary>
    public partial class Attractions : Page
    {
        private string searchText;
        public Attractions()
        {
            InitializeComponent();
            AttractionTable();
            searchText = Search_Bar.Text;
        }

        public void AttractionTable()
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn ID = new DataColumn("ID", typeof(string));
                DataColumn name = new DataColumn("Name", typeof(string));
                DataColumn city = new DataColumn("City", typeof(string));

                dt.Columns.Add(ID);
                dt.Columns.Add(name);
                dt.Columns.Add(city);

                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM trip;"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT trip_ID FROM trip ORDER BY trip_name LIMIT " + i + ",1");
                    row[1] = ConnectToDatabase("SELECT trip_name FROM trip ORDER BY trip_name LIMIT " + i + ",1;");
                    row[2] = ConnectToDatabase("SELECT city FROM trip ORDER BY trip_name LIMIT " + i + ",1;");
                    dt.Rows.Add(row);
                    Table.ItemsSource = dt.DefaultView;
                }
                if (DatabaseCount("SELECT count(*) FROM trip;") == 0)
                {
                    dt.Rows.Clear();
                    Table.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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

        public void New_Attraction_Click(object sender, RoutedEventArgs e)
        {
            New_Attraction na = new New_Attraction(this);
            na.Show();
            na.Topmost = true;
        }

        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            AttractionTable();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn ID = new DataColumn("ID", typeof(string));
                DataColumn name = new DataColumn("Name", typeof(string));
                DataColumn city = new DataColumn("City", typeof(string));

                dt.Columns.Add(ID);
                dt.Columns.Add(name);
                dt.Columns.Add(city);

                if (ConnectToDatabase("SELECT * FROM trip WHERE trip_name LIKE '%" + Search_Bar.Text + "%' OR city LIKE '%" + Search_Bar.Text + "%';") != "")
                {
                    for (int i = 0; i < DatabaseCount("SELECT count(*) FROM trip WHERE trip_name LIKE '%" + Search_Bar.Text + "%' OR city LIKE '%" + Search_Bar.Text + "%';"); i++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = ConnectToDatabase("SELECT trip_ID FROM trip WHERE trip_name LIKE '%" + Search_Bar.Text + "%' OR city LIKE '%" + Search_Bar.Text + "%' ORDER BY trip_name LIMIT " + i + ",1;");
                        row[1] = ConnectToDatabase("SELECT trip_name FROM trip WHERE trip_name LIKE '%" + Search_Bar.Text + "%' OR city LIKE '%" + Search_Bar.Text + "%' ORDER BY trip_name LIMIT " + i + ",1;");
                        row[2] = ConnectToDatabase("SELECT city FROM trip WHERE trip_name LIKE '%" + Search_Bar.Text + "%' OR city LIKE '%" + Search_Bar.Text + "%' ORDER BY trip_name LIMIT " + i + ",1;");
                        dt.Rows.Add(row);
                        Table.ItemsSource = dt.DefaultView;
                        searchText = Search_Bar.Text;
                    }
                }
                else
                {
                    dt.Rows.Clear();
                    Table.ItemsSource = dt.DefaultView;
                    searchText = Search_Bar.Text;
                    MessageBox.Show("No results were found", "Oops...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // ===========================================================Tag_Click===========================================================
        public void Tag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
                String username = drv[0].ToString();
                Admin_Tag at = new Admin_Tag(username/*, this, mainWindow*/);
                at.Show();
                at.Topmost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
                String trip_ID = drv[0].ToString();
                Edit_Attraction ea = new Edit_Attraction(trip_ID, this);
                ea.Show();
                ea.Topmost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            string ID = drv[0].ToString();
            string name = drv[1].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + name + "?", "Delete attraction", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        MessageBox.Show(name + " was deleted.", "Delete attraction");
                        ConnectToDatabase("DELETE FROM review WHERE trip_ID = '" + ID + "';");
                        ConnectToDatabase("DELETE FROM image WHERE trip_ID = '" + ID + "';");
                        ConnectToDatabase("DELETE FROM trip_with_type WHERE trip_ID = '" + ID + "';");
                        ConnectToDatabase("DELETE FROM map_coordinates WHERE trip_ID = '" + ID + "';");
                        ConnectToDatabase("DELETE FROM trip_tag WHERE trip_ID = '" + ID + "';");
                        ConnectToDatabase("DELETE FROM trip WHERE trip_ID = '" + ID + "';");
                        AttractionTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Phew, " + name + " will live to see the light another day!", "Delete attraction");
                    break;
            }
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

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=tipsfortrips;UID=root;PASSWORD=");
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

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=TipsForTrips;UID=root;PASSWORD=");
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AttractionTable();
        }
    }
}
