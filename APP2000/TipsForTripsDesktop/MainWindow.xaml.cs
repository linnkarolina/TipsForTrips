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
        string username = "Test";
        string password = "Test123";

        string sqlUsername = "Test";
        string sqlPassword = "Test123";

        public MainWindow()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            MessageBox.Show("Log in");
            if (username.Equals(getUsername(sqlUsername)) && password.Equals(getPassword(sqlPassword)))
            {
                MessageBox.Show("Hei, Randi!");
            }
            else
            {
                MessageBox.Show("ERROR#3144 - Wrong username or password. Please try again!");
            }
        }

        private string getUsername(string checkUsername)
        {
            return checkUsername;
        }

        private String getPassword(string checkPassword)
        {
            return checkPassword;
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hei, Randi!");
        }
    }
}
