using birgersbolcher.database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace birgersbolcher
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection connection = null;
        MySqlCommand command;
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlDataReader dr;

       

        public MainWindow()
        {
          

            InitializeComponent();
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
                    while(dr.Read())
                    {
                        bolcherColor.Items.Add(new Item(dr.GetString("name_color"),dr.GetInt32("id_color")));
                    }
                    dr.Close();
              
                    command.CommandText = "SELECT * FROM taste_type";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    while(dr.Read())
                    {
                        bolchertype.Items.Add(new Item(dr.GetString("taste_type"), dr.GetInt32("id_taste_type")));
                    }
                    dr.Close();
                    command.CommandText = "SELECT * FROM taste_sourness";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    while(dr.Read())
                    {
                        bolchersourness.Items.Add(new Item(dr.GetString("taste_sourness"), dr.GetInt32("id_taste_sourness")));
                    }
                    dr.Close();
                    command.CommandText = "SELECT * FROM taste_strength";
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    while(dr.Read())
                    {
                        bolcherstrenth.Items.Add(new Item(dr.GetString("taste_strength"), dr.GetInt32("id_taste_strength")));
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



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

    
        private void Dragwindow (object sender, EventArgs e)
        { 
            DragMove();
        }

        private void registerBolcher_Click(object sender, RoutedEventArgs e)
        {
            
             Item coloritm = (Item) bolcherColor.SelectedItem;
            

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

        }

        private void between10_12_Click(object sender, RoutedEventArgs e)
        {

        }

        private void under10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Showall_Click(object sender, RoutedEventArgs e)
        {

        }

        private void heaviestbolcher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void randombutton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}