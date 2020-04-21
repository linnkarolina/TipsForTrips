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
    /// Interaction logic for Edit_Admin_User.xaml
    /// </summary>
    public partial class Edit_Admin_User : Window
    {

        private string username;
        private Admin admin_User;
        private MainWindow mainWindow;

        public Edit_Admin_User(string user, Admin au, MainWindow mw)
        {
            username = user;
            admin_User = au;
            mainWindow = mw;
            InitializeComponent();
            SetTextBoxContent();
        }

        private void SetTextBoxContent()
        {
            string query = "SELECT username FROM admin WHERE username = '" + username + "';";
            string Name = ConnectToDatabase(query);
            Username.Text = Name;

            query = "SELECT password FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Password.Text = Name;

            query = "SELECT full_name FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Full_Name.Text = Name;

            query = "SELECT email FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Email.Text = Name;

            Show_Cities();

            query = "SELECT phone_NR FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Phone_Number.Text = Name;
        }

        private void Show_Cities()
        {
            MySqlConnection Con = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=TipsForTrips;UID=root;PASSWORD=");
            try
            {
                Con.Open();
                string query = "SELECT * FROM location;";
                MySqlCommand cmd = new MySqlCommand(query, Con);
                MySqlDataReader dr = cmd.ExecuteReader();
                int i = 0;

                while (dr.Read())
                {
                    string city = dr.GetString(0);
                    City.Items.Add(city);

                    query = "SELECT city FROM admin WHERE username = '" + username + "';";
                    string Name = ConnectToDatabase(query);

                    if (city.Equals(Name))
                    {
                        City.SelectedIndex = i;
                    }
                    i++;
                }

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                else if (ConnectToDatabase("SELECT username FROM user WHERE username = '" + Username.Text + "';") != "")
                {
                    MessageBox.Show("This username is already taken by a regular user.", "Oops...");
                }
                else
                {
                    string user = Username.Text;
                    string password = Password.Text;
                    string full_name = Full_Name.Text;
                    string email = Email.Text;
                    string city = City.Text;
                    string phone_NR = Phone_Number.Text;
                    string query = "UPDATE admin SET username = '" + user + "', password = '" + password + "', full_name = '" + full_name + "', email = '" + email + "'," +
                        " city = '" + city + "', phone_NR = '" + phone_NR + "' WHERE username = '" + username + "' ;";
                    ConnectToDatabase(query);
                    mainWindow.adminName.DataContext = user;
                    admin_User.UserTable();
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
    }
}
