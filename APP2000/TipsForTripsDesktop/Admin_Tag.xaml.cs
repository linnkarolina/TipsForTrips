using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows.Controls.Primitives;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Admin_Tag.xaml
    /// </summary>
    public partial class Admin_Tag : Window
    {

        private string searchText;
        private string user;
        private BrushConverter borderBrushConverter = new BrushConverter();

        public Admin_Tag(string username)
        {
            user = username;
            InitializeComponent();
            TagTable();
            searchText = Search_Bar.Text;
        }

        public void TagTable()
        {
            DataTable dt = new DataTable();
            DataColumn tag = new DataColumn("Tag", typeof(string));

            dt.Columns.Add(tag);

            for (int i = 0; i < DatabaseCount("SELECT count(*) FROM tag;"); i++)
            {
                DataRow row = dt.NewRow();
                row[0] = ConnectToDatabase("SELECT tag FROM tag ORDER BY tag LIMIT " + i + ",1;");
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;

                DataGridRow rowData = Table.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                MessageBox.Show(rowData.DataContext.ToString());
                if (rowData != null)
                {
                    DataGridCellsPresenter cellPresenter = GetVisualChild<DataGridCellsPresenter>(rowData);
                    DataGridCell cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(0);
                    if (cell == null)
                    {
                        Table.ScrollIntoView(rowData, Table.Columns[0]);
                        cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(0);
                    }
                    Button test = (Button) cell.Content;
                    MessageBox.Show(test.Content.ToString());
                }

                /*Table.ItemsSource = new List<int> { i };

                if ((string)row[0] == ConnectToDatabase("SELECT tag FROM admin_tag WHERE username = '" + user + "';"))
                {
                    MessageBox.Show("Fant noe");
                    test.Content = "✓";
                    test.Foreground = (Brush)borderBrushConverter.ConvertFrom("#56c596");
                }
                else
                {
                    MessageBox.Show("Fant ikkenoe");
                    test.Content = "✕";
                    test.Foreground = (Brush)borderBrushConverter.ConvertFrom("#FF0000");
                }*/
            }
            if (DatabaseCount("SELECT count(*) FROM tag;") == 0)
            {
                dt.Rows.Clear();
                Table.ItemsSource = dt.DefaultView;
            }
        }

        // AUTO-generated
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = Table.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (row == null)
            {
                Table.UpdateLayout();
                Table.ScrollIntoView(Table.Items[index]);
                row = (DataGridRow) Table.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
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

        private void Search_MouseUp(object sender, RoutedEventArgs e)
        {
            if (Search_Bar.Text.Equals(searchText))
            {
                Search_Bar.Text = "";
            }
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn tag = new DataColumn("Tag", typeof(string));

            dt.Columns.Add(tag);
            if (ConnectToDatabase("SELECT tag FROM tag WHERE tag LIKE '%" + Search_Bar.Text + "%';") != "")
            {
                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM tag WHERE tag LIKE '%" + Search_Bar.Text + "%';"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT tag FROM tag WHERE tag LIKE '%" + Search_Bar.Text + "%' ORDER BY tag LIMIT " + i + ",1;");
                    dt.Rows.Add(row);
                    Table.ItemsSource = dt.DefaultView;
                    searchText = Search_Bar.Text;
                }
            }
            else
            {
                dt.Rows.Clear();
                Table.ItemsSource = dt.DefaultView;
                searchText = Search_Bar.Text;
                MessageBox.Show("No results were found", "Oops...");
            }
        }

        private void Checked_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if ((string) b.Content == "✕")
            {
                b.Content = "✓";
                b.Foreground = (Brush) borderBrushConverter.ConvertFrom("#56c596");
            }
            else if ((string) b.Content == "✓")
            {
                b.Content = "✕";
                b.Foreground = (Brush)borderBrushConverter.ConvertFrom("#FF0000");
            }
            else
            {
                MessageBox.Show("Rip");
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        public int DatabaseCount(string query)
        {
            int total;

            // Azure connection
            /* 
            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");
            */

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=TipsForTrips;UID=root;PASSWORD=");
            MySqlCommand cmd = new MySqlCommand(query, MyCon);
            MyCon.Open();
            var queryResult = cmd.ExecuteScalar(); //Return an object so first check for null
            if (queryResult != null)
            {
                // If we have result, then convert it from object to string.
                total = Convert.ToInt32(queryResult);
            }
            else
            {
                // Else make id = "" so you can later check it.
                total = 0;
            }

            MyCon.Close();

            return total;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            /*Grid grid = sender as Grid;
            Button Button_Checked = FindVisualChildren<Button>(grid).FirstOrDefault(x => x.Name == "Button_Checked");
            if (Button_Checked != null)
            {
                Button_Checked.Content = "new value...";
            }*/
        }

        /*private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }*/
    }
}
