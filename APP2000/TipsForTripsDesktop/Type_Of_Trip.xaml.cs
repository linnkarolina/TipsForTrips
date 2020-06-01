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
    /// Interaction logic for Type_Of_Trip.xaml
    /// </summary>
    /// 
    public partial class Type_Of_Trip : Page
    {

        private string searchText;

        public Type_Of_Trip()
        {
            InitializeComponent();
            TypeTable();
            searchText = Search_Bar.Text;
        }

        public void TypeTable()
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn type = new DataColumn("Type of trip", typeof(string));

                dt.Columns.Add(type);

                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM type_of_trip;"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT type_of_trip FROM type_of_trip ORDER BY type_of_trip LIMIT " + i + ",1;");
                    dt.Rows.Add(row);
                    Table.ItemsSource = dt.DefaultView;
                }
                if (DatabaseCount("SELECT count(*) FROM type_of_trip;") == 0)
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

        public void New_Type_Click(object sender, RoutedEventArgs e)
        {
             New_Type_Of_Trip tot = new New_Type_Of_Trip(this);
             tot.Show();
             tot.Topmost = true;
        }

        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            TypeTable();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn type = new DataColumn("Type of trip", typeof(string));

                dt.Columns.Add(type);
                if (ConnectToDatabase("SELECT type_of_trip FROM type_of_trip WHERE type_of_trip LIKE '%" + Search_Bar.Text + "%';") != "")
                {
                    for (int i = 0; i < DatabaseCount("SELECT count(*) FROM type_of_trip WHERE type_of_trip LIKE '%" + Search_Bar.Text + "%' ORDER BY type_of_trip;"); i++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = ConnectToDatabase("SELECT type_of_trip FROM type_of_trip WHERE type_of_trip LIKE '%" + Search_Bar.Text + "%' ORDER BY type_of_trip LIMIT " + i + ",1;");
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

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            String type = drv[0].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + type + "?", "Delete type of trip", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        MessageBox.Show(type + " was deleted.", "Delete type_of_trip");
                        ConnectToDatabase("DELETE FROM type_of_trip WHERE type_of_trip = '" + type + "';");
                        TypeTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
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

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=app2000;UID=root;PASSWORD=");
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

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=app2000;UID=root;PASSWORD=");
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
            TypeTable();
        }
    }
}
