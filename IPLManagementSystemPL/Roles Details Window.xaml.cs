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
using IPLManagementSystemBL; //Referring Business layer
using IPLManagementSystemDoL; //Referring Domain layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Roles_Details_Window.xaml
    /// </summary>
    public partial class Roles_Details_Window : Window
    {
        public Roles_Details_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_roleId.Text = "";
            txt_roleName.Text = "";
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_roleId.Text) | string.IsNullOrEmpty(txt_roleName.Text))
            {
                MessageBox.Show("Invalid Credentials");
                txt_roleName.Focus();
                txt_roleId.Focus();
            }
            Roles roles = new Roles()
            {
                RoleId = int.Parse(txt_roleId.Text),
                Name = txt_roleName.Text
            };
            bool answer = businessLayerClass.InsertRoleBl(roles);
            if(answer is true)
            {
                MessageBox.Show("Role Added Successfully");
            }
            else
            {
                MessageBox.Show("Role Id already exists");
            }
            clearText();
        }

        
        private void btn_updateRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_roleId.Text) | string.IsNullOrEmpty(txt_roleName.Text))
            {
                MessageBox.Show("Invalid Credentials");
                txt_roleName.Focus();
                txt_roleId.Focus();
            }
            
            Roles roles = new Roles()
            {
                RoleId = int.Parse(txt_roleId.Text),
                Name = txt_roleName.Text
            };
            bool answer = businessLayerClass.UpdateRoleBl(roles);
            if (answer is true)
            {
                MessageBox.Show("Role Update Successfully");
            }
            else
            {
                MessageBox.Show("Role Id already exists");
            }
            clearText();
        }

        private void btn_viewRole_Click(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();
            datatable = businessLayerClass.ViewRoleBl();
            dg_roleDataGrid.ItemsSource = datatable.DefaultView;
        }

    }
}
