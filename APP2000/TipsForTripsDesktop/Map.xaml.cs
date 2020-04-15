using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Page
    {
        public Map()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("RANDI ER BEST!");
        }

        // private int counter = 0;

        private void Map_Mouse_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(TheMap);
            Location pinLocation = TheMap.ViewportPointToLocation(mousePosition);
            var latitude = pinLocation.Latitude;
            var longitude = pinLocation.Longitude;

            Pushpin pin = new Pushpin();
            // pin.Content = counter += 10;
            pin.Location = pinLocation;
            pin.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
            pin.ToolTip = latitude + ", " + longitude;
            TheMap.Children.Add(pin);
        }

        private void pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Oisann!");
        }
    }
}
