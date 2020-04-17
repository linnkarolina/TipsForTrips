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
        private MainWindow mainWindow;
        private Pushpin pinStart = null;
        private Pushpin pinEnd = null;
        
        private double startLatitude = 0;
        private double startLongitude = 0;
        private double endLatitude = 0;
        private double endLongitude = 0;

        public Map(MainWindow mw)
        {
            mainWindow = mw;
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
            mainWindow.Content_Frame.Content = new Places();
        }

        private void Mode_Click(object sender, RoutedEventArgs e)
        {
            if (TheMap.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.RoadMode")
            {
                //Set the map mode to Aerial with labels
                TheMap.Mode = new AerialMode(true);
            }
            else if (TheMap.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.AerialMode")
            {
                //Set the map mode to RoadMode
                TheMap.Mode = new RoadMode();
            }
        }

        private void Add_Pin_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (pinStart == null)
                {
                    startLongitude = Convert.ToDouble(Longitude.Text);
                    startLatitude = Convert.ToDouble(Latitude.Text);
                    Location pinLocation = new Microsoft.Maps.MapControl.WPF.Location(startLatitude, startLongitude);

                    // pin.Content = counter += 10;
                    pinStart = new Pushpin();
                    pinStart.Location = pinLocation;
                    pinStart.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
                    pinStart.ToolTip = startLatitude + ", " + startLongitude;
                    pinStart.Background = new SolidColorBrush(Color.FromRgb(86, 197, 150));
                    pinStart.FontSize = 10;
                    pinStart.Content = "Start";
                    TheMap.Children.Add(pinStart);
                }
                else if (pinEnd == null)
                {
                    endLongitude = Convert.ToDouble(Longitude.Text);
                    endLatitude = Convert.ToDouble(Latitude.Text);
                    Location pinLocation = new Microsoft.Maps.MapControl.WPF.Location(endLatitude, endLongitude);

                    // pin.Content = counter += 10;
                    pinEnd = new Pushpin();
                    pinEnd.Location = pinLocation;
                    pinEnd.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
                    pinEnd.ToolTip = endLatitude + ", " + endLongitude;
                    pinEnd.Background = new SolidColorBrush(Color.FromRgb(86, 197, 150));
                    pinEnd.FontSize = 10;
                    pinEnd.Content = "End";
                    TheMap.Children.Add(pinEnd);
                }
                else
                {
                    // Add if you want
                }
                if (pinStart != null && pinEnd != null)
                {
                    RouteFinder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Longitude or latitude is invalid.","Oops...");
            }
        }

        // private int counter = 0;

        private void Map_Mouse_Click(object sender, MouseButtonEventArgs e)
        {
            if(pinStart == null)
            {
                e.Handled = true;

                Point mousePosition = e.GetPosition(TheMap);
                Location pinLocation = TheMap.ViewportPointToLocation(mousePosition);
                var latitude = pinLocation.Latitude;
                var longitude = pinLocation.Longitude;

                // pin.Content = counter += 10;
                pinStart = new Pushpin();
                pinStart.Location = pinLocation;
                pinStart.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
                pinStart.ToolTip = latitude + ", " + longitude;
                pinStart.Background = new SolidColorBrush(Color.FromRgb(86, 197, 150));
                pinStart.FontSize = 10;
                pinStart.Content = "Start";
                TheMap.Children.Add(pinStart);
            }
            else if (pinEnd == null)
            {
                e.Handled = true;

                Point mousePosition = e.GetPosition(TheMap);
                Location pinLocation = TheMap.ViewportPointToLocation(mousePosition);
                var latitude = pinLocation.Latitude;
                var longitude = pinLocation.Longitude;

                // pin.Content = counter += 10;
                pinEnd = new Pushpin();
                pinEnd.Location = pinLocation;
                pinEnd.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
                pinEnd.ToolTip = latitude + ", " + longitude;
                pinEnd.Background = new SolidColorBrush(Color.FromRgb(86, 197, 150));
                pinEnd.FontSize = 10;
                pinEnd.Content = "End";
                TheMap.Children.Add(pinEnd);
            }
            else
            {
                // Add if you want
            }
            if (pinStart != null && pinEnd != null)
            {
                RouteFinder();
            }
        }

        private void RouteFinder()
        {
            /*var routeColor = Colors.Blue;
            var routeBrush = new SolidColorBrush(routeColor);

            var routeLine = new MapPolyline()
            {
                Locations = new LocationCollection(),
                Stroke = routeBrush,
                Opacity = 0.65,
                StrokeThickness = 5.0,
            };

            for (int i=0;i<2;i++)
            {
                if(i==0)
                {
                    routeLine.Locations.Add(new GeoCoordinate(location.Latitude, location.Longitude));
                }
            }
            TheMap.Children.Add(routeLine);*/
        }

        private void pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this pin?", "Delete pin", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        //Pushpin pin = (Pushpin)sender;
                        TheMap.Children.Remove((Pushpin)sender);
                        if (sender == pinStart)
                        {
                            pinStart = null;
                        }
                        if (sender == pinEnd)
                        {
                            pinEnd = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    break;
                case MessageBoxResult.No:   
                    break;
            }
        }
    }
}
