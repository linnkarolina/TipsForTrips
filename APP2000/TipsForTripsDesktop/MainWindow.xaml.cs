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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Database connection
            string name;

            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");

            MySqlCommand cmd = new MySqlCommand("select username from admin where ID_admin=1;", MyCon);
            MyCon.Open();
            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                name = Convert.ToString(queryResult);
            else
                // Else make id = "" so you can later check it.
                name = "";

            MyCon.Close();

            adminName.DataContext = "Welcome " + name + "!";
        }
    }
}
