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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Media.Animation;

namespace TipsForTripsDesktop
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Page
    {

        private string searchText;
        private MainWindow mainWindow;

        public Inbox(MainWindow mw)
        {
            mainWindow = mw;
            InitializeComponent();
            InboxTable();
            searchText = Search_Bar.Text;
        }

        public void InboxTable()
        {
            DataTable dt = new DataTable();
            DataColumn ID = new DataColumn("ID", typeof(string));
            DataColumn from = new DataColumn("From", typeof(string));
            DataColumn subject = new DataColumn("Subject", typeof(string));
            DataColumn Received = new DataColumn("Received", typeof(string));
            DataColumn Answered = new DataColumn("Answered", typeof(string));

            dt.Columns.Add(ID);
            dt.Columns.Add(from);
            dt.Columns.Add(subject);
            dt.Columns.Add(Received);
            dt.Columns.Add(Answered);

            for (int i = 0; i < DatabaseCount("SELECT count(*) FROM admin_inbox;"); i++)
            {
                DataRow row = dt.NewRow();
                row[0] = ConnectToDatabase("SELECT message_ID FROM admin_inbox ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                row[1] = ConnectToDatabase("SELECT user_username FROM admin_inbox ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                row[2] = ConnectToDatabase("SELECT subject FROM admin_inbox ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                row[3] = ConnectToDatabase("SELECT time_sent FROM admin_inbox ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                if (ConnectToDatabase("SELECT isanswered FROM admin_inbox ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;") == "False")
                {
                    row[4] = "No";
                }
                else if (ConnectToDatabase("SELECT isanswered FROM admin_inbox ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;") == "True")
                {
                    row[4] = "Yes";
                }
                dt.Rows.Add(row);
                Table.ItemsSource = dt.DefaultView;
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

        public void New_Message_Click(object sender, RoutedEventArgs e)
        {
            New_Message nm = new New_Message(mainWindow);
            nm.Show();
            nm.Topmost = true;
        }

        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            InboxTable();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn ID = new DataColumn("ID", typeof(string));
            DataColumn from = new DataColumn("From", typeof(string));
            DataColumn subject = new DataColumn("Subject", typeof(string));
            DataColumn Received = new DataColumn("Received", typeof(string));
            DataColumn Answered = new DataColumn("Answered", typeof(string));

            dt.Columns.Add(ID);
            dt.Columns.Add(from);
            dt.Columns.Add(subject);
            dt.Columns.Add(Received);
            dt.Columns.Add(Answered);

            if (ConnectToDatabase("SELECT * FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%';") != "")
            {
                for (int i = 0; i < DatabaseCount("SELECT count(*) FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%';"); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = ConnectToDatabase("SELECT message_ID FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%' ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                    row[1] = ConnectToDatabase("SELECT user_username FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%' ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                    row[2] = ConnectToDatabase("SELECT subject FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%' ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                    row[3] = ConnectToDatabase("SELECT time_sent FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%' ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;");
                    if (ConnectToDatabase("SELECT isanswered FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%' ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;") == "False")
                    {
                        row[4] = "No";
                    }
                    else if (ConnectToDatabase("SELECT isanswered FROM admin_inbox WHERE user_username LIKE '%" + Search_Bar.Text + "%' OR subject LIKE '%" + Search_Bar.Text + "%' ORDER BY isanswered, time_sent desc LIMIT " + i + ",1;") == "True")
                    {
                        row[4] = "Yes";
                    }
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

        public void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
                string message_ID = drv[0].ToString();
                Open_Message om = new Open_Message(message_ID, mainWindow);
                om.Show();
                om.Topmost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            string message_ID = drv[0].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this message?", "Delete message", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        MessageBox.Show("Message " + message_ID + " was deleted.", "Delete message");
                        ConnectToDatabase("DELETE FROM admin_inbox WHERE message_ID = '" + message_ID + "';");
                        InboxTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Okay then.", "Delete message");
                    break;
            }
        }

            private void Search_MouseUp(object sender, RoutedEventArgs e)
        {
            if (Search_Bar.Text.Equals(searchText))
            {
                Search_Bar.Text = "";
            }
        }

        public string ConnectToDatabase(string query)
        {
            string name;

            // Azure connection
            /* 
            MySqlConnection MyCon = new MySqlConnection("SERVER=app2000.mysql.database.azure.com;DATABASE=app2000;UID=trygve@app2000;PASSWORD=Ostekake123");
            */

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=app2000;UID=root;PASSWORD=");
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

            MySqlConnection MyCon = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=app2000;UID=root;PASSWORD=");
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

        private void Table_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InboxTable();
        }
    }
}
