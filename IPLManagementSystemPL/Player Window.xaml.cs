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
using System.Data;
using IPLManagementSystemBL; //Referrring Business Layer
using IPLManagementSystemDoL; //Referring Domain Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Player_Window.xaml
    /// </summary>
    public partial class Player_Window : Window
    {
        public Player_Window()
        {
            InitializeComponent();
        }
        public void ClearText()
        {
            txt_playerId.Text = "";
            txt_playerName.Text = "";
            txt_playerAge.Text = "";
            txt_playerTeamId.Text = "";
            txt_playerSpecialityId.Text = "";
            txt_playerPhotoId.Text = "";
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_playerId.Text) | string.IsNullOrEmpty(txt_playerName.Text) | string.IsNullOrEmpty(txt_playerAge.Text) |
                string.IsNullOrEmpty(txt_playerTeamId.Text) | string.IsNullOrEmpty(txt_playerSpecialityId.Text) | string.IsNullOrEmpty(txt_playerPhotoId.Text))
            {
                MessageBox.Show("Invalid Details");
            }
            else
            {
                try
                {
                    Regex stringOnly = new Regex(@"^[a-zA-Z\s]+$"); //Regex for string format only

                    if (stringOnly.Match(txt_playerName.Text).Success)
                    {
                        Player player = new Player()
                        {
                            Id = int.Parse(txt_playerId.Text),
                            Name = txt_playerName.Text,
                            Age = int.Parse(txt_playerAge.Text),
                            TeamId = int.Parse(txt_playerTeamId.Text),
                            SpecialityId = int.Parse(txt_playerSpecialityId.Text),
                            PhotoId = int.Parse(txt_playerPhotoId.Text),
                        };
                        bool answer = businessLayerClass.InsertPlayerBl(player);
                        if (answer is true)
                        {
                            MessageBox.Show("Player Inserted successfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid Player Id or Player already exists");
                        }
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Data");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter proper data");
                }
                ClearText();
            }
        }

        private void btn_updatePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_playerId.Text) | string.IsNullOrEmpty(txt_playerName.Text) | string.IsNullOrEmpty(txt_playerAge.Text) |
                string.IsNullOrEmpty(txt_playerTeamId.Text) | string.IsNullOrEmpty(txt_playerSpecialityId.Text) | string.IsNullOrEmpty(txt_playerPhotoId.Text))
            {
                MessageBox.Show("Invalid Details");
            }
            else
            {
                try
                {
                    Regex stringOnly = new Regex(@"^[a-zA-Z\s]+$"); //Regex for string format only

                    if (stringOnly.Match(txt_playerName.Text).Success)
                    {
                        Player player = new Player()
                        {
                            Id = int.Parse(txt_playerId.Text),
                            Name = txt_playerName.Text,
                            Age = int.Parse(txt_playerAge.Text),
                            TeamId = int.Parse(txt_playerTeamId.Text),
                            SpecialityId = int.Parse(txt_playerSpecialityId.Text),
                            PhotoId = int.Parse(txt_playerPhotoId.Text),
                        };
                        bool answer = businessLayerClass.UpdatePlayerBl(player);
                        if (answer is true)
                        {
                            MessageBox.Show("Player Updated successfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid Player Id or Player already exists");
                        }
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Data");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter proper data");
                }
                ClearText();
            }
        }

        private void btn_deletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_playerId.Text))
            {
                MessageBox.Show("Invalid Details");
            }
            else
            {
                try 
                {
                    int Id = int.Parse(txt_playerId.Text);
                    bool answer = businessLayerClass.DeletePlayerBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Player Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Player does not exists");
                    }
                    ClearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Id");
                }
                ClearText();
            }
        }

        private void btn_viewPlayer_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessLayerClass.ViewPlayerBl();
            dg_playerDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
