using computershop.Services;
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
    /// Interaction logic for UserDatagrid.xaml
    /// </summary>
    public partial class UserDatagrid : Window
    {
        IDatabase database = new Users();


        public UserDatagrid()
        {
            InitializeComponent();
            UsersDataGrid.ItemsSource = database.GetAllData();    


        }
    }
}
