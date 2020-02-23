﻿using MySql.Data.MySqlClient;
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

        private void Dash_Enter(object sende, System.EventArgs e)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = (Color)ColorConverter.ConvertFromString("#2f4050");
            animation.To = (Color)ColorConverter.ConvertFromString("#243745"); 
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            bDash.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2f4050"));
            bDash.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void Dash_Leave(object sende, System.EventArgs e)
        {

        }
    }
}
