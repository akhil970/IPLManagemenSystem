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
using System.Text.RegularExpressions;
using IPLManagementSystemBL; //Referrring Business Layer
using IPLManagementSystemDoL; //Referring Domain Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Teams_Window.xaml
    /// </summary>
    public partial class Teams_Window : Window
    {
        public Teams_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            
            txt_teamId.Text = "";
            txt_teamName.Text = "";
            txt_teamHomeGround.Text = "";
            txt_teamLogoImage.Text = "";
            txt_teamFranchiseOwner.Text = "";
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addTeam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_teamId.Text) | string.IsNullOrEmpty(txt_teamName.Text) | string.IsNullOrEmpty(txt_teamHomeGround.Text) | string.IsNullOrEmpty(txt_teamFranchiseOwner.Text) | string.IsNullOrEmpty(txt_teamLogoImage.Text))
            {
                MessageBox.Show("Enter Data in all the fields");
            }
            else
            {
                try
                {
                    Regex filePath = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"); //Regex for file location path only    
                    Regex stringOnly = new Regex(@"^[A-Za-z \.]+$"); //Regex for string format only
                    if(filePath.IsMatch(txt_teamLogoImage.Text) && stringOnly.IsMatch(txt_teamName.Text) && stringOnly.IsMatch(txt_teamName.Text) &&
                        stringOnly.IsMatch(txt_teamName.Text) && stringOnly.IsMatch(txt_teamName.Text))
                    {
                        Team team = new Team()
                        {
                            Id = int.Parse(txt_teamId.Text),
                            Name = txt_teamName.Text,
                            FranchiseOwner = txt_teamFranchiseOwner.Text,
                            LogoImage = txt_teamLogoImage.Text,
                            HomeGround = txt_teamHomeGround.Text
                        };
                        bool answer = businessLayerClass.InsertTeamBl(team);
                        if (answer is true)
                        {
                            MessageBox.Show("Team Details Added");
                        }
                        else
                        {
                            MessageBox.Show("Team Id already exists");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper image file path and enter only strings where required");
                    }
                    clearText();
                }
                catch
                {
                    MessageBox.Show("Enter Proper details");
                }
                clearText();
            }
        }

        private void btn_updateTeam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_teamId.Text) | string.IsNullOrEmpty(txt_teamName.Text) | string.IsNullOrEmpty(txt_teamHomeGround.Text) | string.IsNullOrEmpty(txt_teamFranchiseOwner.Text) | string.IsNullOrEmpty(txt_teamLogoImage.Text))
            {
                MessageBox.Show("Enter Data in all the fields");
            }
            else
            {
                try
                {
                    Regex filePath = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"); //Regex for file location path only    
                    Regex stringOnly = new Regex(@"^[A-Za-z ]+$"); //Regex for string format only
                    if (filePath.IsMatch(txt_teamLogoImage.Text) && stringOnly.IsMatch(txt_teamName.Text) && stringOnly.IsMatch(txt_teamName.Text) &&
                        stringOnly.IsMatch(txt_teamName.Text) && stringOnly.IsMatch(txt_teamName.Text))
                    {
                        Team team = new Team()
                        {
                            Id = int.Parse(txt_teamId.Text),
                            Name = txt_teamName.Text,
                            FranchiseOwner = txt_teamFranchiseOwner.Text,
                            LogoImage = txt_teamLogoImage.Text,
                            HomeGround = txt_teamHomeGround.Text
                        };
                        bool answer = businessLayerClass.UpdateTeamBl(team);
                        if (answer is true)
                        {
                            MessageBox.Show("Team Details Updated");
                        }
                        else
                        {
                            MessageBox.Show("Team Id does not exists");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper image file path and enter only strings where required");
                    }
                    clearText();
                }
                catch
                {
                    MessageBox.Show("Enter Proper details");
                }
                clearText();
            }
        }

        private void btn_deleteTeam_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txt_teamId.Text))
            {
                try
                {
                    int Id = int.Parse(txt_teamId.Text);
                    bool answer = businessLayerClass.DeleteTeamBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Team Details Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Team Id");
                    }
                    clearText();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Enter Proper Details");
                }
                clearText();
            }
            else
            {
                MessageBox.Show("Enter Proper team Id to delete");
            }
        }

        private void btn_viewTeam_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = businessLayerClass.ViewTeamBl();
            dg_teamDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
