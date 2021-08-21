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
using EncryptionDecryption;
using System.Text.RegularExpressions;
using System.Data;
//using System.Text;
using System.Security.Cryptography;
using IPLManagementSystemBL; //Referring Business layer
using IPLManagementSystemDoL; //Referring Domain layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for User_Details_Window.xaml
    /// </summary>
    public partial class User_Details_Window : Window
    {
        public User_Details_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_userid.Text = "";
            txt_userName.Text = "";
            txt_password.Password = "";
            txt_lastName.Text = "";
            txt_firstname.Text = "";
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userid.Text) | string.IsNullOrEmpty(txt_password.Password) |
    string.IsNullOrEmpty(txt_lastName.Text) | string.IsNullOrEmpty(txt_firstname.Text) | string.IsNullOrEmpty(txt_lastName.Text))

            {
                MessageBox.Show("Invalid Data");
            }
            else
            {
                try
                {
                    Regex stringOnly = new Regex(@"^[a-zA-Z]+$"); //Regex for string format only
                    Regex email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"); //Regex for email
                    Regex password = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$"); //Regex for for password for atleast 8 characters
                    if (password.IsMatch(txt_password.Password) && email.IsMatch(txt_userName.Text) && stringOnly.IsMatch(txt_firstname.Text) && stringOnly.IsMatch(txt_lastName.Text))
                    {
                        User user = new User()
                        {
                            UserId = int.Parse(txt_userid.Text),
                            Username = txt_userName.Text,
                            Password = txt_password.Password,
                            Firstname = txt_firstname.Text,
                            Lastname = txt_lastName.Text,
                        };
                        bool answer = businessLayerClass.InsertUserBl(user);
                        if (answer is true)
                        {
                            MessageBox.Show("User Added Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid User");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter correct information and folow password and email rules\npassword should be atleast 6 characters with one special character and a number\nEmail should be correct format");
                    }
                    clearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Input Format is Incorrect");
                }
                clearText();
            }
        }
        private void btn_updateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userid.Text) | string.IsNullOrEmpty(txt_password.Password) |
    string.IsNullOrEmpty(txt_lastName.Text) | string.IsNullOrEmpty(txt_firstname.Text) | string.IsNullOrEmpty(txt_lastName.Text))

            {
                MessageBox.Show("Invalid Data");
            }
            else
            {
                try
                {
                    Regex stringOnly = new Regex(@"^[a-zA-Z]+$"); //Regex for string format only
                    Regex email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Regex password = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                    if (password.IsMatch(txt_password.Password) && email.IsMatch(txt_userName.Text) && stringOnly.IsMatch(txt_firstname.Text) && stringOnly.IsMatch(txt_lastName.Text))
                    {
                        User user = new User()
                        {
                            UserId = int.Parse(txt_userid.Text),
                            Username = txt_userName.Text,
                            Password = txt_password.Password,
                            Firstname = txt_firstname.Text,
                            Lastname = txt_lastName.Text,
                        };
                        bool answer = businessLayerClass.UpdateUserBl(user);
                        if (answer is true)
                        {
                            MessageBox.Show("User Update Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid User");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter correct information and folow password and email rules\npassword should be atleast 8 characters with one special character and a number\nEmail should be correct format");
                    }
                    clearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Input Format is Incorrect");
                }
                clearText();
            }
        }

        private void btn_viewUser_Click(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();
            datatable = businessLayerClass.ViewUserBl(); //data table in presentation layer
            dg_userDataGrid.ItemsSource = datatable.DefaultView;
        }
    }
}
