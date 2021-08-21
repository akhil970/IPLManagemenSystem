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
    /// Interaction logic for Player_Photo_Window.xaml
    /// </summary>
    public partial class Player_Photo_Window : Window
    {
        public Player_Photo_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_playerPhotoId.Clear();
            txt_playerPhotoPath.Clear();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addPlayerPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_playerPhotoId.Text) | string.IsNullOrEmpty(txt_playerPhotoPath.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Regex filePath = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"); //Regex for file location path only    
                    if(filePath.IsMatch(txt_playerPhotoPath.Text))
                    {
                        PlayerPhoto playerPhoto = new PlayerPhoto()
                        {
                            Id = int.Parse(txt_playerPhotoId.Text),
                            Photo = txt_playerPhotoPath.Text
                        };
                        bool answer = businessLayerClass.InsertPlayerPhotoBl(playerPhoto);
                        if (answer is true)
                        {
                            MessageBox.Show("Player Photo Added successfully");
                        }
                        else
                        {
                            MessageBox.Show("Player photo already exists");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter proper photo path");
                    }
                    clearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Details");
                }
                
            }
        }

        private void btn_updatePlayerPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_playerPhotoId.Text) | string.IsNullOrEmpty(txt_playerPhotoPath.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Regex filePath = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"); //Regex for file location path only    
                    if (filePath.IsMatch(txt_playerPhotoPath.Text))
                    {
                        PlayerPhoto playerPhoto = new PlayerPhoto()
                        {
                            Id = int.Parse(txt_playerPhotoId.Text),
                            Photo = txt_playerPhotoPath.Text
                        };
                        bool answer = businessLayerClass.UpdatePlayerPhotoBl(playerPhoto);
                        if (answer is true)
                        {
                            MessageBox.Show("Player Photo Updated successfully");
                        }
                        else
                        {
                            MessageBox.Show("Player photo already exists or Invalid Photo Id");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter proper photo path");
                    }
                    clearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter Proper Details");
                }
                clearText();
            }
        }

        private void btn_deletePlayerPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_playerPhotoId.Text))
            {
                MessageBox.Show("Enter Id to delete");
            }
            else
            {
                try 
                {
                    int Id = int.Parse(txt_playerPhotoId.Text);
                    bool answer = businessLayerClass.DeletePlayerPhotoBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Player Photo Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Photo ID");
                    }
                    clearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Details");
                }
                clearText();
            }
        }
        private void btn_viewPlayerPhoto_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessLayerClass.ViewPlayerPhotoBl();
            dg_playerPhotoDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
