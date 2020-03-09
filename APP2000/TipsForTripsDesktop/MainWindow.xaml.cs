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
using System.Windows.Threading;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool check = true;
        private double width;

        public MainWindow()
        {
            InitializeComponent();
            string query = "SELECT username FROM admin WHERE ID_admin = 1;";
            ConnectToDatabase(query);
        }

        private void Dash_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            Content_Frame.Content = new Page1();
            if(check == false)
            {
                width = SubMenu.ActualWidth;
                da.From = width;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                Storyboard.SetTarget(da, SubMenu);
                Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
                sb.Children.Add(da);
                sb.Begin();
                sb.Children.Remove(da);
                check = true;
            }
            else if (check == true)
            {
                width = SubMenu.ActualWidth;
                da.From = 0;
                da.To = width;
                da.Duration = new Duration(TimeSpan.FromSeconds(0.4));// Animation target
                Storyboard.SetTarget(da, SubMenu);
                Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
                sb.Children.Add(da);
                sb.Begin();
                sb.Children.Remove(da);
                check = false;
            }
        }

        public void Dash_Enter(object sender, System.EventArgs e)
        {
            if (check)
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
        }

        public void Dash_Leave(object sender, System.EventArgs e)
        {
            if (check)
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

        public void Logout_Enter(object sender, System.EventArgs e)
        {
            if (check)
            {
                ColorAnimation buttonAnimation = new ColorAnimation();
                ColorAnimation textAnimation = new ColorAnimation();

                buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
                buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
                buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
                textAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
                textAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
                textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
                Log_out.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
                Log_out.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
                Log_out.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
                Log_out.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
            }
        }

        public void Logout_Leave(object sender, System.EventArgs e)
        {
            if (check)
            {
                ColorAnimation buttonAnimation = new ColorAnimation();
                ColorAnimation textAnimation = new ColorAnimation();

                buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#86ac41");
                buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#324851");
                buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
                textAnimation.From = (Color)ColorConverter.ConvertFromString("#324851");
                textAnimation.To = (Color)ColorConverter.ConvertFromString("#86ac41");
                textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
                Log_out.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#324851"));
                Log_out.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#86ac41"));
                Log_out.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
                Log_out.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
            }
        }

        /// <summary>
        /// Database connection
        /// </summary>
        public string ConnectToDatabase(string query)
        {
            string name;

            // Azure connection
            /* 
            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");
            */

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3307;DATABASE=TipsForTrips;UID=root;PASSWORD=");
            MySqlCommand cmd = new MySqlCommand(query, MyCon);
            MyCon.Open();
            var queryResult = cmd.ExecuteScalar(); //Return an object so first check for null
            if (queryResult != null)
            {
                // If we have result, then convert it from object to string.
                name = Convert.ToString(queryResult);
                Console.WriteLine(name + " 1");
            }
            else
            {
                // Else make id = "" so you can later check it.
                name = "";
                Console.WriteLine(name + " 2");
            }

            MyCon.Close();

            Console.WriteLine(name + " 3");

            adminName.DataContext = name;

            Console.WriteLine(name + " 4");

            return name;
        }
    }
}
