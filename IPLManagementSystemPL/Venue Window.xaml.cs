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
    /// Interaction logic for Venue_Window.xaml
    /// </summary>
    public partial class Venue_Window : Window
    {
        public Venue_Window()
        {
            InitializeComponent();
        }
        public void ClearText()
        {
            txt_venueId.Clear();
            txt_venueLocation.Clear();
            txt_venueDescription.Clear();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addVenue_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_venueId.Text) | string.IsNullOrEmpty(txt_venueLocation.Text) | string.IsNullOrEmpty(txt_venueDescription.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Regex stringOnly = new Regex(@"^[a-zA-Z\s]+$"); //Regex for string, one whitespace, one special characterformat only
                    if (stringOnly.IsMatch(txt_venueLocation.Text) && stringOnly.IsMatch(txt_venueDescription.Text))
                    {
                        Venue venue = new Venue()
                        {
                            Id = int.Parse(txt_venueId.Text),
                            Location = txt_venueLocation.Text,
                            Description = txt_venueDescription.Text
                        };
                        bool answer = businessLayerClass.InsertVenueBl(venue);
                        if (answer is true)
                        {
                            MessageBox.Show("Venue Added Succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Venue Already Exists");
                        }
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper data");
                    }
                    ClearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter proper data");
                }
                ClearText();
            }
        }

        private void btn_updateVenue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_venueId.Text) | string.IsNullOrEmpty(txt_venueLocation.Text) | string.IsNullOrEmpty(txt_venueDescription.Text))
            {
                MessageBox.Show("Invalid Details");
            }
            else
            {
                try
                {
                    Regex stringOnly = new Regex(@"^[a-zA-Z\s]+$"); //Regex for string format only
                    if (stringOnly.IsMatch(txt_venueLocation.Text) && stringOnly.IsMatch(txt_venueDescription.Text))
                    {
                        Venue venue = new Venue()
                        {
                            Id = int.Parse(txt_venueId.Text),
                            Location = txt_venueLocation.Text,
                            Description = txt_venueDescription.Text
                        };
                        bool answer = businessLayerClass.UpdateVenueBl(venue);
                        if (answer is true)
                        {
                            MessageBox.Show("Venue Updated Succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Venue ID Does not Exists");
                        }
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Details");
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter proper data");
                }
                ClearText();
            }
        }

        private void btn_deleteVenue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_venueId.Text))
            {
                MessageBox.Show("Enter Id to delete");
            }
            else
            {
                try
                {
                    int Id = int.Parse(txt_venueId.Text);
                    bool answer = businessLayerClass.DeleteVenueBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Venue Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Venue Does not Exists");
                    }
                    ClearText();
                }
                catch
                {
                    MessageBox.Show("Enter proper details");
                }
                ClearText();
            }
        }

        private void btn_viewVenue_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessLayerClass.ViewVenueBl();
            dg_venueDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
