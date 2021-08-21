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
using System.Data;
using IPLManagementSystemBL; //Referencing Business Layer
using IPLManagementSystemDoL; //Referencing Business Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for User_Roles_Window.xaml
    /// </summary>
    public partial class User_Roles_Window : Window
    {
        public User_Roles_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_roleId.Text = "";
            txt_userid.Text = "";
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addUserRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userid.Text) | string.IsNullOrEmpty(txt_roleId.Text))
            {
                MessageBox.Show("Enter all the details");
                txt_userid.Focus();
            }
            else
            {
                try
                {
                    UserRoles userRoles = new UserRoles()
                    {
                        UserId = int.Parse(txt_userid.Text),
                        RoleId = int.Parse(txt_roleId.Text)
                    };

                    bool answer = businessLayerClass.InsertUserRolesBl(userRoles);
                    if (answer is true)
                    {
                        MessageBox.Show("UserRole Added");
                    }
                    else
                    {
                        MessageBox.Show("User Does Not Exists");
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Input Format is Incorrect");
                }
                clearText();
            }
            
        }
        private void btn_updateUserRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userid.Text) | string.IsNullOrEmpty(txt_roleId.Text))
            {
                MessageBox.Show("Enter All the Details");
            }
            else
            {
                try
                {
                    UserRoles userRoles = new UserRoles()
                    {
                        UserId = int.Parse(txt_userid.Text),
                        RoleId = int.Parse(txt_roleId.Text)
                    };

                    bool answer = businessLayerClass.UpdateUserRolesBl(userRoles);
                    if (answer is true)
                    {
                        MessageBox.Show("UserRole Updated Succesffully");
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserId or RoleId");
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Input Format is Incorrect");
                }
                clearText();
            }
        }
        private void btn_viewUserRole_Click(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();
            datatable = businessLayerClass.ViewUserRolesBl();
            dg_userRoleDataGrid.ItemsSource = datatable.DefaultView;
        }
    }
}
