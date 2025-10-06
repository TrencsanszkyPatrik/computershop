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
using computershop.Services;

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

    
        IDatabase users = new Services.Users();

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

        bool res = users.GetData(username, password);

        if(res)
        {

            MessageBox.Show("Sikeres bejelentkezés");
            UserDatagrid userwind = new UserDatagrid();
            userwind.Show();
            this.Close();
        }
        else
        {
            MessageBox.Show("Sikertelen bejelentkezés!");
        }
        

    }

    

}