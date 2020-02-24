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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectToDatabase();
        }

        private void Dash_Click(object sender, RoutedEventArgs e)
        {
            Content_Frame.Content = new Page1();
        }

        public void Dash_Enter(object sender, System.EventArgs e)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            Dashboard_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
            Dashboard_Button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
            Dashboard_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            Dashboard_Button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        public void Dash_Leave(object sender, System.EventArgs e)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            Dashboard_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
            Dashboard_Button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
            Dashboard_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            Dashboard_Button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        public void Places_Enter(object sender, System.EventArgs e)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            Places_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
            Places_Button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
            Places_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            Places_Button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        public void Places_Leave(object sender, System.EventArgs e)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            Places_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
            Places_Button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
            Places_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            Places_Button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        public void Users_Enter(object sender, System.EventArgs e)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            Users_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
            Users_Button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
            Users_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            Users_Button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        public void Users_Leave(object sender, System.EventArgs e)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            Users_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
            Users_Button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
            Users_Button.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            Users_Button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
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
