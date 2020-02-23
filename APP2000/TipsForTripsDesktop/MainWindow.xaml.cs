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
using System.Windows.Media.Animation;


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
            ConnectToDatabase();
        }
        public void Dash_Enter(object sender, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.To = (Color)ColorConverter.ConvertFromString("#243745");
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            Dashboard_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#243745"));
            Dashboard_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void Dash_Leave(object sender, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#243745");
            animation.To = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            Dashboard_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2f4050"));
            Dashboard_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void Places_Enter(object sender, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.To = (Color)ColorConverter.ConvertFromString("#243745");
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            Places_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#243745"));
            Places_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void Places_Leave(object sender, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#243745");
            animation.To = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            Places_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2f4050"));
            Places_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }
        public void Users_Enter(object sender, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.To = (Color)ColorConverter.ConvertFromString("#243745");
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            Users_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#243745"));
            Users_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void Users_Leave(object sender, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#243745");
            animation.To = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            Users_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2f4050"));
            Users_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public string ConnectToDatabase()
        {
            // Database connection
            string name;

            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");

            MySqlCommand cmd = new MySqlCommand("SELECT username FROM admin WHERE ID_admin=1;", MyCon);
            MyCon.Open();
            var queryResult = cmd.ExecuteScalar(); //Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                name = Convert.ToString(queryResult);
            else
                // Else make id = "" so you can later check it.
                name = "";

            MyCon.Close();

            adminName.DataContext = name;

            return name;
        }
    }
}
