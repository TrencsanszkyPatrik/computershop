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
using System.Windows.Shapes;

namespace computershop
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string fullname = fullnameBox.Text;
            string email = emailBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullname))
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt!");
                return;
            }

            Connect db = new Connect("users");

            try
            {
                db.Connection.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Users (Username,Fullname,  Email, Password) VALUES (@username, @FullName,  @email, @password)",
                    db.Connection
                );

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@FullName", fullname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password); 

                cmd.ExecuteNonQuery();

                MessageBox.Show("Sikeres regisztráció!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            finally
            {
                db.Connection.Close();
            }
        }
    }
}
