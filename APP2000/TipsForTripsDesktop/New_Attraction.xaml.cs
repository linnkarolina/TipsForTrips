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
using System.Windows.Shapes;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for New_Attraction.xaml
    /// </summary>
    public partial class New_Attraction : Window
    {

        

        public New_Attraction()
        {
            InitializeComponent();
            Big_Image();
        }

        /*public string DisplayedImage
        {
            get { return @"C:\Users\tobia\Documents\GitHub\TipsForTrips\APP2000\TipsForTripsDesktop\Images\Logo\tree.png"; }
        }*/

        private void Big_Image()
        {
            try
            {
                if (Panel_Image.Children[0] != null)
                {
                    Image img = (Image)Panel_Image.Children[0];
                    ImageSource imageSource = img.Source;
                    // ImageSource imageSource = new BitmapImage(new Uri("pack:/APP2000,,/Images/"));
                    Showed_Image.Source = imageSource;
                    var bc = new BrushConverter();
                    Image_Background.Background = (Brush)bc.ConvertFrom("#FFFFFF");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hei");
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hade");
        }

        public void Add_Image_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add");
        }

        public void Delete_Image_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete");
        }

    }
}
