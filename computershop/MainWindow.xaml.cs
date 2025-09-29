using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace computershop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    

    private void registerPage(object sender, RoutedEventArgs e)
    {
        RegisterWindow regWin = new RegisterWindow();
        regWin.Show();
        this.Hide();
    }

    private void login(object sender, RoutedEventArgs e)
    {
        string username = UserName.Text;
        string password = Pass.Password;

        if (CheckLogin(username, password))
        {
            MessageBox.Show("Sikeres belépés!");
            Adminpage adminpage = new Adminpage();
            adminpage.Show();
            this.Close();
        }
        else
        {
            MessageBox.Show("Hibás felhasználónév vagy jelszó!");
        }
    }

    private bool CheckLogin(string username, string password)
    {

        Connect db = new Connect("users");

        db.Connection.Open(); 

        MySqlCommand cmd = new MySqlCommand(
            "SELECT COUNT(*) FROM Users WHERE Username=@username AND Password=@password",
            db.Connection
        );

        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);

        int count = Convert.ToInt32(cmd.ExecuteScalar());
        bool valid = count > 0;

        db.Connection.Close();

        return valid;
    }

}