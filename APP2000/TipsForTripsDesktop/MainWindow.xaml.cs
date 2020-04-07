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
        private bool checkButtonClick = true;
        private bool checkArea = true;
        private bool checkUsers = true;
        private bool checkProfile = true;
        private double width;
        private Button b;
        private Grid g;
        private string user;

        public MainWindow(string username)
        {
            user = username;
            InitializeComponent();
            string query = "SELECT username FROM admin WHERE username='"+username+"';";
            ConnectToDatabase(query);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            b = (Button)sender;
            if (b.Equals(Area_Button))
            {
                g = SubMenu_Area;
            }
            else if (b.Equals(Users_Button))
            {
                g = SubMenu_Users;
            }
            else if (b.Equals(Profile_Button))
            {
                g = SubMenu_Profile;
            }
            if(checkButtonClick == false && checkArea == false)
            {
                CloseSubMenu(SubMenu_Area);
                RotateArrowBack(Area_Tag);
                if(b.Equals(Users_Button))
                {
                    OpenSubMenu();
                    RotateArrow(Users_Tag);

                    checkUsers = false;
                    checkArea = true;
                    checkProfile = true;
                }
                else if (b.Equals(Profile_Button))
                {
                    OpenSubMenu();
                    RotateArrow(Profile_Tag);

                    checkUsers = true;
                    checkArea = true;
                    checkProfile = false;
                }
                else
                {
                    checkButtonClick = true;
                    checkArea = true;
                    checkUsers = true;
                    checkProfile = true;
                }
            }
            else if (checkButtonClick == false && checkUsers == false)
            {
                CloseSubMenu(SubMenu_Users);
                RotateArrowBack(Users_Tag);
                if (b.Equals(Area_Button))
                {
                    OpenSubMenu();
                    RotateArrow(Area_Tag);

                    checkUsers = true;
                    checkArea = false;
                    checkProfile = true;

                }
                else if (b.Equals(Profile_Button))
                {
                    OpenSubMenu();
                    RotateArrow(Profile_Tag);

                    checkUsers = true;
                    checkArea = true;
                    checkProfile = false;
                }
                else
                {
                    checkButtonClick = true;
                    checkArea = true;
                    checkUsers = true;
                    checkProfile = true;
                }
            }
            else if (checkButtonClick == false && checkProfile == false)
            {
                CloseSubMenu(SubMenu_Profile);
                RotateArrowBack(Profile_Tag);
                if (b.Equals(Users_Button))
                {
                    OpenSubMenu();
                    RotateArrow(Users_Tag);

                    checkUsers = false;
                    checkArea = true;
                    checkProfile = true;

                }
                else if (b.Equals(Area_Button))
                {
                    OpenSubMenu();
                    RotateArrow(Area_Tag);

                    checkUsers = true;
                    checkArea = false;
                    checkProfile = true;
                }
                else
                {
                    checkButtonClick = true;
                    checkArea = true;
                    checkUsers = true;
                    checkProfile = true;
                }
            }
            // If everything is true
            else if (checkArea == true && checkUsers == true && checkProfile == true)
            {
                OpenSubMenu();
                checkButtonClick = false;
                if (b.Equals(Area_Button))
                {
                    checkArea = false;
                    RotateArrow(Area_Tag);
                }
                else if (b.Equals(Users_Button))
                {
                    checkUsers = false;
                    RotateArrow(Users_Tag);
                }
                else if (b.Equals(Profile_Button))
                {
                    checkProfile = false;
                    RotateArrow(Profile_Tag);
                }
            }
        }
        private void OpenSubMenu()
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            width = g.ActualWidth;
            da.From = 0;
            da.To = width;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.4));// Animation target
            Storyboard.SetTarget(da, g);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            sb.Children.Add(da);
            sb.Begin();
            sb.Children.Remove(da);

            if (checkArea == false)
            {
                Leave(Area_Button);
                RotateArrowBack(Area_Tag);
            }
            if (checkUsers == false)
            {
                Leave(Users_Button);
                RotateArrowBack(Users_Tag);
            }
            if (checkProfile == false)
            {
                Leave(Profile_Button);
                RotateArrowBack(Profile_Tag);
            }
        }

        private void CloseSubMenu(Grid grid)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            width = grid.ActualWidth;
            da.From = width;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.4));// Animation target
            Storyboard.SetTarget(da, grid);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            sb.Children.Add(da);
            sb.Begin();
            sb.Children.Remove(da);
        }

        public void Button_Enter(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;

            if (checkArea == false && b.Equals(Area_Button))
            {
                // Nothing is supposed to happen
            }
            else if (checkUsers == false && b.Equals(Users_Button))
            {
                // Nothing is supposed to happen
            }
            else if (checkProfile == false && b.Equals(Profile_Button))
            {
                // Nothing is supposed to happen
            }
            else
            {
                Enter(b);
            }
        }

        private void Enter(Button b)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#205072");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#7be495");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#7be495");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#205072");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7be495"));
            b.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#205072"));
            b.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            b.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        public void Button_Leave(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;

            if (checkArea == false && b.Equals(Area_Button))
            {
                // Nothing is supposed to happen
            }
            else if (checkUsers == false && b.Equals(Users_Button))
            {
                // Nothing is supposed to happen
            }
            else if (checkProfile == false && b.Equals(Profile_Button))
            {
                // Nothing is supposed to happen
            }
            else
            {
                Leave(b);
            }
        }

        private void Leave (Button b)
        {
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#7be495");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#205072");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            textAnimation.From = (Color)ColorConverter.ConvertFromString("#205072");
            textAnimation.To = (Color)ColorConverter.ConvertFromString("#7be495");
            textAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#205072"));
            b.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7be495"));
            b.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
            b.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, textAnimation);
        }

        private void RotateArrow (TextBlock tb)
        {
            var rotate = new DoubleAnimation(0, 90, TimeSpan.FromSeconds(0.4));
            var rt = (RotateTransform)tb.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotate);            
        }

        private void RotateArrowBack (TextBlock tb)
        {
            var rotate = new DoubleAnimation(90, 0, TimeSpan.FromSeconds(0.4));
            var rt = (RotateTransform)tb.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotate);
        }

        // Submenu enter and leave animations
        public void Submenu_Enter(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#cff4d2");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#7be495");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7be495"));
            b.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
        }

        public void Submenu_Leave(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            ColorAnimation buttonAnimation = new ColorAnimation();
            ColorAnimation textAnimation = new ColorAnimation();

            buttonAnimation.From = (Color)ColorConverter.ConvertFromString("#7be495");
            buttonAnimation.To = (Color)ColorConverter.ConvertFromString("#cff4d2");
            buttonAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.33));
            b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cff4d2"));
            b.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimation);
        }

        // Go to pages
        public void Button_Page_Click(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            // Revert animations
            if (checkButtonClick == false && checkArea == false)
            {
                RotateArrowBack(Area_Tag);
                CloseSubMenu(SubMenu_Area);
                Leave(Area_Button);
                checkArea = true;
                checkButtonClick = true;
            }
            else if (checkButtonClick == false && checkUsers == false)
            {
                RotateArrowBack(Users_Tag);
                CloseSubMenu(SubMenu_Users);
                Leave(Users_Button);
                checkUsers = true;
                checkButtonClick = true;
            }
            else if (checkButtonClick == false && checkProfile == false)
            {
                RotateArrowBack(Profile_Tag);
                CloseSubMenu(SubMenu_Profile);
                Leave(Profile_Button);
                checkProfile = true;
                checkButtonClick = true;
            }
            // Go to a new page
            if (b.Equals(Dashboard_Button))
            {
                Content_Frame.Content = new Dashboard();
            }
            else if (b.Equals(Places_Button))
            {
                Content_Frame.Content = new Places();
            }
            else if (b.Equals(Map_Button))
            {
                Content_Frame.Content = new Map();
            }
            else if (b.Equals(Tags_Button))
            {
                Content_Frame.Content = new Tags(this);
            }
            else if (b.Equals(Web_Button))
            {
                Content_Frame.Content = new Web_users();
            }
            else if (b.Equals(Admin_Button))
            {
                Content_Frame.Content = new Admin(user, this);
            }
            else if (b.Equals(Inbox_Button))
            {
                Content_Frame.Content = new Inbox();
            }
            else if (b.Equals(Basic_info_Button))
            {
                Content_Frame.Content = new Basic_info(user, this);
            }
            else if (b.Equals(Password_Button))
            {
                Content_Frame.Content = new Password(user);
            }
            else if(b.Equals(Log_out_Button))
            {
                var Login = new Login_Window();
                this.Close();
                Login.Show();
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
            adminName.DataContext = name;

            return name;
        }
    }
}
