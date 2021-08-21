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

namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Admin_Menu_Window.xaml
    /// </summary>
    public partial class Admin_Menu_Window : Window
    {
        public Admin_Menu_Window()
        {
            InitializeComponent();
        }

        private void btn_users_Click(object sender, RoutedEventArgs e)
        {
            User_Details_Window userDetailsWindow = new User_Details_Window();
            userDetailsWindow.ShowDialog();
        }

        private void btn_roles_Click(object sender, RoutedEventArgs e)
        {
            
            Roles_Details_Window rolesDetailsWindow = new Roles_Details_Window();
            rolesDetailsWindow.ShowDialog();
        }

        private void btn_userroles_Click(object sender, RoutedEventArgs e)
        {
            User_Roles_Window userRolesWindow = new User_Roles_Window();
            userRolesWindow.ShowDialog();
        }
    }
}
