using Microsoft.Win32;
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
    /// Interaction logic for Edit_Attraction.xaml
    /// </summary>
    public partial class Edit_Attraction : Window
    {

        private string trip_ID;
        private Attractions attractions;

        public Edit_Attraction(string ID, Attractions a)
        {
            trip_ID = ID;
            attractions = a;
            InitializeComponent();
            SetTextBoxContent();
        }

        private void SetTextBoxContent() 
        {
            try
            {
                string query = "SELECT startLongitude FROM map_coordinates WHERE trip_ID = '" + trip_ID + "';";
                string startLon = ConnectToDatabase(query);
                StartLon.Text = startLon;

                query = "SELECT startLatitude FROM map_coordinates WHERE trip_ID = '" + trip_ID + "';";
                string startLat = ConnectToDatabase(query);
                StartLat.Text = startLat;

                query = "SELECT endLongitude FROM map_coordinates WHERE trip_ID = '" + trip_ID + "';";
                string endLon = ConnectToDatabase(query);
                EndLon.Text = endLon;

                query = "SELECT endLatitude FROM map_coordinates WHERE trip_ID = '" + trip_ID + "';";
                string endLat = ConnectToDatabase(query);
                EndLat.Text = endLat;

                Show_Cities();

                Show_Type();

                query = "SELECT length FROM trip WHERE trip_ID = '" + trip_ID + "';";
                string length = ConnectToDatabase(query);
                Length.Text = length;

                query = "SELECT difficulty FROM trip WHERE trip_ID = '" + trip_ID + "';";
                string difficulty = ConnectToDatabase(query);
                Difficulty.Text = difficulty;

                query = "SELECT trip_name FROM trip WHERE trip_ID = '" + trip_ID + "';";
                string name = ConnectToDatabase(query);
                Name.Text = name;

                query = "SELECT description FROM trip WHERE trip_ID = '" + trip_ID + "';";
                string description = ConnectToDatabase(query);
                Description.Text = description;

                query = "SELECT imagw FROM image WHERE trip_ID = '" + trip_ID + "';";
                string image = ConnectToDatabase(query);
                Description.Text = description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Show_Cities()
        {
            MySqlConnection Con = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=TipsForTrips;UID=root;PASSWORD=");
            try
            {
                Con.Open();
                string query = "SELECT * FROM location;";
                MySqlCommand cmd = new MySqlCommand(query, Con);
                MySqlDataReader dr = cmd.ExecuteReader();
                int i = 0;

                while (dr.Read())
                {
                    string city = dr.GetString(0);
                    City.Items.Add(city);

                    if (city.Equals(ConnectToDatabase("SELECT city FROM trip WHERE trip_ID ='" + trip_ID + "';")))
                    {
                        City.SelectedIndex = i;
                    }
                    i++;
                }

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Show_Type()
        {
            MySqlConnection Con = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=TipsForTrips;UID=root;PASSWORD=");
            try
            {
                Con.Open();
                string query = "SELECT * FROM type_of_trip;";
                MySqlCommand cmd = new MySqlCommand(query, Con);
                MySqlDataReader dr = cmd.ExecuteReader();
                int i = 0;

                while (dr.Read())
                {
                    string type = dr.GetString(0);
                    Type_Of_Trip.Items.Add(type);

                    if (type.Equals(ConnectToDatabase("SELECT type_of_trip FROM trip_with_type WHERE trip_ID ='" + trip_ID + "';")))
                    {
                        Type_Of_Trip.SelectedIndex = i;
                    }
                    i++;
                }

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Big_Image()
        {
            try
            {
                if (Panel_Image.Children.Count != 0)
                {
                    Image img = (Image)Panel_Image.Children[0];
                    ImageSource imageSource = img.Source;
                    Showed_Image.Source = imageSource;
                    var bc = new BrushConverter();
                    Image_Background.Background = (Brush)bc.ConvertFrom("#FFFFFF");
                }
                else
                {
                    var bc = new BrushConverter();
                    Image_Background.Background = (Brush)bc.ConvertFrom("#EEEEEE");
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
            try
            {
                string name = Name.Text;
                string desctiption = Description.Text;
                string startLon = StartLon.Text;
                string startLat = StartLat.Text;
                string endLon = EndLon.Text;
                string endLat = EndLat.Text;
                string city = City.Text;
                string type_of_trip = Type_Of_Trip.Text;
                string length = Length.Text;
                string difficulty = Difficulty.Text;
                string website = Website.Text;
                Image[] img = new Image[Panel_Image.Children.Count];
                object[] imageInBits = new object[Panel_Image.Children.Count];

                for (int i = 0; i < Panel_Image.Children.Count; i++)
                {
                    img[i] = (Image)Panel_Image.Children[i];
                    try
                    {
                        var bmp = img[i].Source as BitmapImage;

                        int height = bmp.PixelHeight;
                        int width = bmp.PixelWidth;
                        int stride = width * ((bmp.Format.BitsPerPixel + 7) / 8);

                        byte[] bits = new byte[height * stride];
                        bmp.CopyPixels(bits, stride, 0);
                        imageInBits[i] = bmp;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                // Validation
                if (name == "" || desctiption == "" || startLon == "" || startLat == "" || city == "Select city" || type_of_trip == "Select type" || length == "" || difficulty == "")
                {
                    MessageBox.Show("You need to fill out all the required fields (marked with *).", "Oops...");
                }
                else
                {
                    if (website == "")
                    {
                        ConnectToDatabase("INSERT INTO trip VALUES (null,'" + name + "','" + length + "','" + difficulty + "','" + desctiption + "','" + city + "',null);");
                    }
                    else
                    {
                        ConnectToDatabase("INSERT INTO trip VALUES (null,'" + name + "','" + length + "','" + difficulty + "','" + desctiption + "','" + city + "','" + website + "');");
                    }
                    string trip_ID = ConnectToDatabase("SELECT MAX(trip_ID) FROM trip;");
                    if (endLon == "" || endLat == "")
                    {
                        ConnectToDatabase("INSERT INTO map_coordinates VALUES ('" + trip_ID + "','" + startLat + "','" + startLon + "',null,null);");
                    }
                    else
                    {
                        ConnectToDatabase("INSERT INTO map_coordinates VALUES ('" + trip_ID + "','" + startLat + "','" + startLon + "','" + endLat + "','" + endLon + "');");
                    }
                    for (int i = 0; i < Panel_Image.Children.Count; i++)
                    {
                        if (imageInBits[i].ToString() == ConnectToDatabase("SELECT image FROM image WHERE trip_ID ='" + trip_ID + "' ORDER BY image LIMIT " + i + ",1;"))
                        {
                            MessageBox.Show("Bilde lagres.");
                            ConnectToDatabase("INSERT INTO image VALUES (null,'" + trip_ID + "','" + imageInBits[i] + "');");
                        }
                    }
                    ConnectToDatabase("INSERT INTO trip_with_type VALUES ('" + trip_ID + "','" + type_of_trip + "');");
                    attractions.AttractionTable();
                    this.Close();
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

        private void Image_Clicked(object s)
        {
            Image image = (Image)s;
            Showed_Image.Source = image.Source;
        }

        public void Add_Image_Click(object sender, RoutedEventArgs e)
        {
            Image img = new Image();
            img.Margin = new Thickness(0, 0, 5, 0);
            img.MouseUp += (s, evt) => {
                Image_Clicked(s);
            };
            Panel_Image.Children.Add(img);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                Big_Image();
            }
        }

        public void Delete_Image_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this image?", "Delete image", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        for (int i = 0; i < Panel_Image.Children.Count; i++)
                        {
                            Image img = (Image)Panel_Image.Children[i];
                            if (img.Source == Showed_Image.Source)
                            {
                                Panel_Image.Children.RemoveAt(i);
                            }
                        }
                        Showed_Image.Source = null;
                        Big_Image();
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

        public string ConnectToDatabase(string query)
        {
            string name;

            // Azure connection
            /* 
            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");
            */

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=tipsfortrips;UID=root;PASSWORD=");
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
