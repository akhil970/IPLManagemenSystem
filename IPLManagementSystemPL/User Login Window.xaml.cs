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
using IPLManagementSystemBL; //Referring Business layer
using IPLManagementSystemDoL; //Referring Domian layer

//This is Presentation Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for User_Login_Window.xaml
    /// </summary>
    public partial class User_Login_Window : Window
    {
        public User_Login_Window()
        {
            InitializeComponent();
        }

        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userName.Text) | string.IsNullOrEmpty(txt_password.Password))
            {
                MessageBox.Show("Invalid Credentials");
                txt_userName.Focus();
            }
            else
            {
                UserLogin userlogin = new UserLogin()
                {
                    userName = txt_userName.Text,
                    password = txt_password.Password
                };
                object solutionFromBusinesslayer = businessLayerClass.UserLoginBl(userlogin);
                if (solutionFromBusinesslayer.GetType() == typeof(int))
                {
                    switch ((int)solutionFromBusinesslayer)
                    {
                        case 1: //Login as Admin
                            Admin_Menu_Window adminMenuWindow = new Admin_Menu_Window();
                            adminMenuWindow.Show();
                            this.Hide();
                            break;
                        case 2: //Login as Employee
                            Employee_Login_Window employeeWindow = new Employee_Login_Window();
                            employeeWindow.Show();
                            this.Hide();
                            break;
                        case 3: //Login as Customer
                            Customer_Window customerWindow = new Customer_Window();
                            customerWindow.Show();
                            this.Hide();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show($"{(string)solutionFromBusinesslayer}");
                }
            }
        }
    }
}
