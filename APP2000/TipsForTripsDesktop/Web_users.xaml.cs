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

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Web_users.xaml
    /// </summary>
    public partial class Web_users : Page
    {
        public Web_users()
        {
            InitializeComponent();
        }

        public void UserTable()
        {
            DataTable dt = new DataTable();
            DataColumn username = new DataColumn("Username", typeof(string));
            DataColumn password = new DataColumn("Password", typeof(string));
            DataColumn location = new DataColumn("Location", typeof(string));
            DataColumn email = new DataColumn("E-mail", typeof(string));
            DataColumn full_name = new DataColumn("Full name", typeof(string));
            DataColumn phone_NR = new DataColumn("Phone", typeof(string));

            dt.Columns.Add(username);
            dt.Columns.Add(password);
            dt.Columns.Add(location);
            dt.Columns.Add(email);
            dt.Columns.Add(full_name);
            dt.Columns.Add(phone_NR);

            for (int i = 0; i < DatabaseCount("SELECT count(*) FROM user;"); i++)
            {
                DataRow row = dt.NewRow();
                row[0] = ConnectToDatabase("SELECT username FROM user LIMIT " + i + ",1;");
                row[1] = ConnectToDatabase("SELECT password FROM user LIMIT " + i + ",1;");
                row[2] = ConnectToDatabase("SELECT location FROM user LIMIT " + i + ",1;");
                row[3] = ConnectToDatabase("SELECT email FROM user LIMIT " + i + ",1;");
                row[4] = ConnectToDatabase("SELECT full_name FROM user LIMIT " + i + ",1;");
                row[5] = ConnectToDatabase("SELECT phone_NR FROM user LIMIT " + i + ",1;");
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;
            }
        }

        public void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
                String username = drv[0].ToString();
                String password = drv[1].ToString();
                String location = drv[2].ToString();
                String email = drv[3].ToString();
                String full_name = drv[4].ToString();
                String phone_NR = drv[5].ToString();
                MessageBox.Show("Username " + username + ", Password " + password + location + email + full_name + phone_NR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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
                Console.WriteLine(name);
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserTable();
        }

        private void Table_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
