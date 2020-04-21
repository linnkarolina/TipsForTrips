using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
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
using System.Windows.Controls.Primitives;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Admin_Tag.xaml
    /// </summary>
    public partial class Admin_Tag : Window
    {

        private string searchText;
        private string user;

        public Admin_Tag(string username)
        {
            user = username;
            InitializeComponent();
            TagTable();
            searchText = Search_Bar.Text;
        }

        public void TagTable()
        {
            DataTable dt = new DataTable();
            DataColumn check = new DataColumn("Checked", typeof(Boolean));
            DataColumn tag = new DataColumn("Tag", typeof(string));

            check.DefaultValue = false;

            dt.Columns.Add(check);
            dt.Columns.Add(tag);

            for (int i = 0; i < DatabaseCount("SELECT count(*) FROM tag;"); i++)
            {
                DataRow row = dt.NewRow();
                string query = ConnectToDatabase("SELECT tag FROM tag ORDER BY tag LIMIT " + i + ",1;");
                row[1] = query;

                for (int o = 0; o < DatabaseCount("SELECT count(*) FROM admin_tag where username = '" + user + "';"); o++)
                {
                    if (query == ConnectToDatabase("SELECT tag FROM admin_tag WHERE username = '" + user + "' LIMIT " + o + ",1;"))
                    {
                        row[0] = true;
                        break;
                    }
                    else
                    {
                        row[0] = false;
                    }
                }
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;
            }
            if (DatabaseCount("SELECT count(*) FROM tag;") == 0)
            {
                dt.Rows.Clear();
                Table.ItemsSource = dt.DefaultView;
            }
        }

        private void CheckedChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = Table.SelectedItem as DataRowView;
                string tag = drv.Row.ItemArray[1].ToString();
                if (drv.Row.ItemArray[0].ToString() == "True")
                {
                    ConnectToDatabase("DELETE FROM admin_tag WHERE username = '" + user + "' AND tag = '" + tag + "';");
                    TagTable();
                }
                else if (drv.Row.ItemArray[0].ToString() == "False")
                {
                    ConnectToDatabase("INSERT INTO admin_tag VALUES ('" + tag + "','" + user + "');");
                    TagTable();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Noe gikk galt");
                MessageBox.Show(ex.ToString());
            }
        }

        // AUTO-generated
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = Table.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (row == null)
            {
                Table.UpdateLayout();
                Table.ScrollIntoView(Table.Items[index]);
                row = (DataGridRow) Table.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
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

        private void Search_MouseUp(object sender, RoutedEventArgs e)
        {
            if (Search_Bar.Text.Equals(searchText))
            {
                Search_Bar.Text = "";
            }
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn tag = new DataColumn("Tag", typeof(string));

            dt.Columns.Add(tag);
            if (ConnectToDatabase("SELECT tag FROM tag WHERE tag LIKE '%" + Search_Bar.Text + "%';") != "")
            {
                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM tag WHERE tag LIKE '%" + Search_Bar.Text + "%';"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT tag FROM tag WHERE tag LIKE '%" + Search_Bar.Text + "%' ORDER BY tag LIMIT " + i + ",1;");
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

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

    }
}
