using birgersbolcher.database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace birgersbolcher
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection connection = null;
        MySqlCommand command;
        private MySqlDataAdapter adapter;
        MySqlDataReader dr;
        private int bolcherPriceIntValue;
        private int bolcherWeightIntValue;


        public MainWindow()
        {
            InitializeComponent();
            ITheme theme = new PaletteHelper().GetTheme();

            theme.SetBaseTheme(Theme.Dark);

            theme.SetPrimaryColor(Color.FromRgb(255, 65, 81));

            theme.SetSecondaryColor(Color.FromRgb(253, 71, 78));

            
            theme.PrimaryMid = new ColorPair(Color.FromRgb(253, 71, 78), Colors.White);

            new PaletteHelper().SetTheme(theme);
            DBConnection db = DBConnection.Instance();
            if (db.IsConnect())
            {
                try
                {
                    connection = db.Connection;
                    
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM color";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    Dictionary<int, string> coloritems = new Dictionary<int, string>();
                    while (dr.Read())
                    {
                        bolcherColor.Items.Add(new Item(dr.GetString("name_color"), dr.GetInt32("id_color")));
                    }

                    dr.Close();

                    command.CommandText = "SELECT * FROM taste_type";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        bolchertype.Items.Add(new Item(dr.GetString("taste_type"), dr.GetInt32("id_taste_type")));
                    }

                    dr.Close();
                    command.CommandText = "SELECT * FROM taste_sourness";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        bolchersourness.Items.Add(new Item(dr.GetString("taste_sourness"),
                            dr.GetInt32("id_taste_sourness")));
                    }

                    dr.Close();
                    command.CommandText = "SELECT * FROM taste_strength";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        bolcherstrenth.Items.Add(new Item(dr.GetString("taste_strength"),
                            dr.GetInt32("id_taste_strength")));
                    }

                    dr.Close();
                }
                catch (Exception e)
                {
                    string messageBoxText = e.Message;
                    string caption = "Birgers Bolcher";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
            else
            {
                string messageBoxText = "Cannot connect to database, please contact application support";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void Dragwindow(object sender, EventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception errr)
            {
                //can you not?
            }
        }

        private void registerBolcher_Click(object sender, RoutedEventArgs e)
        {
            ComboBox[] comboBoxs = {bolcherColor, bolchertype, bolcherstrenth, bolchersourness};
            for (int inti = 0; inti < comboBoxs.Length; inti++)
            {
                if (comboBoxs[inti].SelectedItem == null)
                {
                    MessageBox.Show("Du skal vælge noget");
                    comboBoxs[inti].Focus();
                    return;
                }
            }

            TextBox[] newTextBox = {bolcherName, bolcherprice, bolcherweight};
            for (int inti = 0; inti < newTextBox.Length; inti++)
            {
                if (newTextBox[inti].Text == string.Empty)
                {
                    MessageBox.Show("Please fill the text box");
                    newTextBox[inti].Focus();
                    return;
                }
            }

            Item colorSelectedItem = (Item) bolcherColor.SelectedItem;
            Item tasteTypeSelectedItem = (Item) bolchertype.SelectedItem;
            Item tasteStrengthSelectedItem = (Item) bolcherstrenth.SelectedItem;
            Item tasteSournessSelectedItem = (Item) bolchersourness.SelectedItem;
            String bolcheName = bolcherName.Text;
            try
            {
                bolcherPriceIntValue = int.Parse(bolcherprice.Text);
                bolcherWeightIntValue = int.Parse(bolcherweight.Text);
            }
            catch (Exception err)
            {
                string errmessageBoxText = err.Message;
                string errcaption = "Birgers Bolcher";
                MessageBoxButton errbutton = MessageBoxButton.OK;
                MessageBoxImage erricon = MessageBoxImage.Information;
                MessageBox.Show(errmessageBoxText, errcaption, errbutton, erricon);
            }


            try
            {
                command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO bolcher (name_bolcher,weight,price,color_id_color,taste_id_taste,taste_sourness_id_taste_sourness,taste_type_id_taste_type) VALUES (@name_bolcher,@weight,@price,@color_id_color,@taste_id_taste,@taste_sourness_id_taste_sourness,@taste_type_id_taste_type)";
                command.Parameters.AddWithValue("@name_bolcher", bolcheName);
                command.Parameters.AddWithValue("@weight", bolcherWeightIntValue);
                command.Parameters.AddWithValue("@price", bolcherPriceIntValue);
                command.Parameters.AddWithValue("@color_id_color", colorSelectedItem.Value);
                command.Parameters.AddWithValue("@taste_id_taste", tasteStrengthSelectedItem.Value);
                command.Parameters.AddWithValue("@taste_sourness_id_taste_sourness", tasteSournessSelectedItem.Value);
                command.Parameters.AddWithValue("@taste_type_id_taste_type", tasteTypeSelectedItem.Value);
                command.ExecuteNonQuery();
                //adapter.SelectCommand = command;
                string messageBoxText = "Registeret bolcher";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch (Exception exception)
            {
                string errmessageBoxText = exception.Message;
                string errcaption = "Birgers Bolcher";
                MessageBoxButton errbutton = MessageBoxButton.OK;
                MessageBoxImage erricon = MessageBoxImage.Information;
                MessageBox.Show(errmessageBoxText, errcaption, errbutton, erricon);
            }
        }

        private void register_color_Click(object sender, RoutedEventArgs e)
        {
            if (color_name.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO color (`name_color`) VALUES (@name)";
                command.Parameters.AddWithValue("@name", color_name.Text.ToString());
                command.ExecuteNonQuery();
                //adapter.SelectCommand = command;
                string messageBoxText = "sent query";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                string messageBoxText = "du skal udfylde noget i farve feltet";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void register_taste_type_Click(object sender, RoutedEventArgs e)
        {
            if (taste_type.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO taste_type (`taste_type`) VALUES (@name)";
                command.Parameters.AddWithValue("@name", taste_type.Text.ToString());
                command.ExecuteNonQuery();
                //adapter.SelectCommand = command;
                string messageBoxText = "sent query";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                string messageBoxText = "du skal udfylde noget i smagstype feltet";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void register_taste_sourness_Click(object sender, RoutedEventArgs e)
        {
            if (taste_sourness.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO taste_sourness (`taste_sourness`) VALUES (@name)";
                command.Parameters.AddWithValue("@name", taste_sourness.Text.ToString());
                command.ExecuteNonQuery();
                //adapter.SelectCommand = command;
                string messageBoxText = "sent query";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                string messageBoxText = "du skal udfylde noget i smagssurhed feltet";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void register_taste_strength_Click(object sender, RoutedEventArgs e)
        {
            if (taste_strength.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO taste_strength (`taste_strength`) VALUES (@name)";
                command.Parameters.AddWithValue("@name", taste_strength.Text.ToString());
                command.ExecuteNonQuery();
                //adapter.SelectCommand = command;
                string messageBoxText = "sent query";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else
            {
                string messageBoxText = "du skal udfylde noget i smagsstyrke feltet";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void Notred_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type WHERE name_color <> 'rød'";
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void between10_12_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type WHERE  weight BETWEEN 10 AND  12 ORDER BY name_bolcher DESC " ;
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;
            
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            dr.Close();
        }

        private void under10_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type WHERE weight < 10";
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type  WHERE name_bolcher LIKE @q" ;
            command.Parameters.AddWithValue("@q", "%"+Searchbar.Text+"%");
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            dr.Close();
        }

        private void Showall_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type";
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;
                var color = bolcherTable.Columns[2];
                var tastesourness = bolcherTable.Columns[4];
                var tastestrenth = bolcherTable.Columns[5];
                var tastetype = bolcherTable.Columns[6];
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            dr.Close();
        }

        private void heaviestbolcher_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type ORDER BY weight DESC LIMIT 3" ;
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            dr.Close();
        }

        private void randombutton_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type ORDER BY RAND() LIMIT 1" ;
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void blueandred_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT id_bolcher,name_bolcher,c.name_color,weight,ts.taste_sourness, t.taste_strength,tt.taste_type,price FROM bolcher INNER JOIN color c on bolcher.color_id_color = c.id_color INNER JOIN taste_sourness ts on bolcher.taste_sourness_id_taste_sourness = ts.id_taste_sourness INNER JOIN taste_strength t on bolcher.taste_id_taste = t.id_taste_strength INNER JOIN taste_type tt on bolcher.taste_type_id_taste_type = tt.id_taste_type WHERE name_color = 'rød' OR name_color = 'blå'" ;
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            try
            {
                adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bolcherTable.ItemsSource = table.DefaultView;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}