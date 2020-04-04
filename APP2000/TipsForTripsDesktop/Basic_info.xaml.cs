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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Basic_info.xaml
    /// </summary>
    public partial class Basic_info : Page
    {
        private string username;
        MainWindow mainWindow;

        public Basic_info(string user, MainWindow mw)
        {
            username = user;
            mainWindow = mw;
            InitializeComponent();
            setTextBoxContent();
        }

        private void setTextBoxContent()
        {
            string query = "SELECT username FROM admin WHERE username = '" + username + "';";
            string Name = ConnectToDatabase(query);
            Username.Text = Name;

            query = "SELECT full_name FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Full_Name.Text = Name;

            query = "SELECT email FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Email.Text = Name;

            query = "SELECT city FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            City.Text = Name;

            query = "SELECT phone_NR FROM admin WHERE username = '" + username + "';";
            Name = ConnectToDatabase(query);
            Phone_Number.Text = Name;
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

        // Save click
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = 0;
                string s = Phone_Number.Text;
                bool result = int.TryParse(s, out i);

                if (Username.Text == "" || City.Text == "" || Email.Text == "" || Full_Name.Text == "" || Phone_Number.Text == "")
                {
                    MessageBox.Show("All fields must be filled.", "Oops...");
                }
                else if (i == 0)
                {
                    MessageBox.Show("Phone number must be a numeric value.", "Oops...");
                }
                else
                {
                    ConnectToDatabase("UPDATE admin SET username = '" + Username.Text + "',full_name = '" + Full_Name.Text + "', email = '" + Email.Text + "',city = '" + City.Text + "',phone_NR = '" + Phone_Number.Text + "';");
                    MessageBox.Show("Your info has been changed.");
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Delete click
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete your account?", "Delete profile", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    string query = "DELETE FROM admin WHERE username = '" + username + "';";
                    ConnectToDatabase(query);
                    MessageBox.Show("Your account was deleted, you will be signed out.", "Delete profile");
                    Login_Window liw = new Login_Window();
                    liw.Show();
                    mainWindow.Close();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Phew, my account is safe!", "Delete profile");
                    break;
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
    }
}
