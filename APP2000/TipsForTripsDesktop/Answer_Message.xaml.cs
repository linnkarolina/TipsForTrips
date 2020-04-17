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
    /// Interaction logic for Answer_Message.xaml
    /// </summary>
    public partial class Answer_Message : Window
    {
        private string message_ID;
        private MainWindow mainWindow;
        public Answer_Message(string ID, MainWindow mw)
        {
            message_ID = ID;
            mainWindow = mw;
            InitializeComponent();
            SetTextBoxContent();
        }

        private void SetTextBoxContent()
        {
            string to = ConnectToDatabase("SELECT user_username FROM admin_inbox WHERE message_ID ='" + message_ID + "';");
            string subject = "Re: " + ConnectToDatabase("SELECT subject FROM admin_inbox WHERE message_ID ='" + message_ID + "';");
            string message = "\n__________________________________________\nFrom: " + ConnectToDatabase("SELECT user_username FROM admin_inbox WHERE message_ID ='" + message_ID + "';") + "\n\n" + ConnectToDatabase("SELECT message FROM admin_inbox WHERE message_ID ='" + message_ID + "';");
            To.Text = to;
            Subject.Text = subject;
            Message.Text = message;
        }

        public void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (To.Text == "" || Subject.Text == "" || Message.Text == "")
                {
                    MessageBox.Show("All fields must be filled.", "Oops...");
                }
                else if (ConnectToDatabase("SELECT username FROM user WHERE username = '" + To.Text + "';") == "")
                {
                    MessageBox.Show("The user " + To.Text + " does not exist.", "Oops...");
                }
                else
                {
                    ConnectToDatabase("INSERT INTO user_inbox VALUES('NULL','" + mainWindow.adminName.DataContext + "','" + To.Text + "','" + Subject.Text + "'," +
                    "'" + Message.Text + "',CURRENT_TIMESTAMP());");
                    ConnectToDatabase("UPDATE admin_inbox SET isanswered = 1 WHERE message_ID = '" + message_ID + "';");
                    this.Close();
                    MessageBox.Show("Your message was sent.","Successful");
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

        // Enteer animation
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
