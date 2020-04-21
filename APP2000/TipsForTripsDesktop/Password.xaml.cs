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
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : Page
    {

        private string user;

        public Password(string username) {
            user = username;
            InitializeComponent();
        }

        // Button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Cur_Pass = Current_Password.Password.ToString();
            string New_Pass = New_Password.Password.ToString();
            string Verify_Pass = Verify_Password.Password.ToString();
            string query = "SELECT password FROM admin WHERE username='" + user + "';";

            if (New_Pass == "" || Verify_Pass == "")
            {
                MessageBox.Show("All fields must be filled.", "Oops...");
            }
            else if (ConnectToDatabase(query) != Cur_Pass)
            {
                Success.DataContext = "";
                ERROR.DataContext = "The current password is incorrect.";
            }
            else if (New_Pass != Verify_Pass)
            {
                Success.DataContext = "";
                ERROR.DataContext = "The new passwords don't match.";
            }
            else if (ConnectToDatabase(query) == New_Pass)
            {
                Success.DataContext = "";
                ERROR.DataContext = "You cannot change to your current password.";
            }
            else if(Cur_Pass == "" || New_Pass == "" || Verify_Pass == "")
            {
                Success.DataContext = "";
                ERROR.DataContext = "You have to enter text in all fields.";
            }
            else
            {
                query = "UPDATE admin set password='" +New_Pass+ "' WHERE username='" + user + "';";
                ConnectToDatabase(query);
                ERROR.DataContext = "";
                Success.DataContext = "Password updated successfully!";
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

        // Database connection
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
    }

}
