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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for New_Web_User.xaml
    /// </summary>
    public partial class New_Web_User : Window
    {

        private Web_users Web_users;
        public New_Web_User(Web_users wu)
        {
            Web_users = wu;
            InitializeComponent();
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

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = 0;
                string s = Phone_Number.Text;
                bool result = int.TryParse(s, out i);

                if (Username.Text == "" || Password.Text == "" || City.Text == "" || Email.Text == "" || Full_Name.Text == "" || Phone_Number.Text == "")
                {
                    MessageBox.Show("All fields must be filled.", "Oops...");
                }
                else if (i == 0)
                {
                    MessageBox.Show("Phone number must be a numeric value.", "Oops...");
                }
                else if (ConnectToDatabase("SELECT username FROM admin WHERE username = '" + Username.Text + "';") != "")
                {
                    MessageBox.Show("This username is already taken by an administrator.", "Oops...");
                }
                else
                {
                    ConnectToDatabase("INSERT INTO user VALUES('" + Username.Text + "','" + Password.Text + "','" + City.Text + "'," +
                    "'" + Email.Text + "','" + Full_Name.Text + "','" + Phone_Number.Text + "');");
                    Web_users.UserTable();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
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

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=TipsForTrips;UID=root;PASSWORD=");
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
