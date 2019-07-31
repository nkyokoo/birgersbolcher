using birgersbolcher.database;
using MySql.Data.MySqlClient;
using System;
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


        public MainWindow()
        {
            DBConnection db = DBConnection.Instance();
            if (db.IsConnect())
            {
                connection = db.Connection;
            }
            else
            {
                string messageBoxText = "Cannot connect to database, please contact application support";
                string caption = "Birgers Bolcher";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
            }

            InitializeComponent();
              
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

    
        private void LeftMouseDown_Event(object sender, EventArgs e)
        {
            this.DragMove();
        }

        private void registerBolcher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void register_color_Click(object sender, RoutedEventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "NSERT INTO color (`id_color`, `name`) VALUES (?)";
            command.Parameters.Add(color_name.Text);
            adapter.SelectCommand = command;

        }

        private void register_taste_type_Click(object sender, RoutedEventArgs e)
        {

        }

        private void register_taste_sourness_Click(object sender, RoutedEventArgs e)
        {

        }

        private void register_taste_strength_Click(object sender, RoutedEventArgs e)
        {

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