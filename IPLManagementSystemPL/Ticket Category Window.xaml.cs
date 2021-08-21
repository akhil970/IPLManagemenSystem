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
    /// Interaction logic for Ticket_Category_Window.xaml
    /// </summary>
    public partial class Ticket_Category_Window : Window
    {
        public Ticket_Category_Window()
        {
            InitializeComponent();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        public void ClearText()
        {
            txt_ticketCategoryId.Clear();
            txt_ticketCategoryName.Clear();
        }
        private void btn_addTicketCategory_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_ticketCategoryId.Text) | string.IsNullOrEmpty(txt_ticketCategoryName.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                Regex stringOnly = new Regex(@"^[A-Za-z ]+$"); //Regex for string format and space only
                try
                {
                    if (stringOnly.IsMatch(txt_ticketCategoryName.Text))
                    {
                        TicketCategory ticketCategory = new TicketCategory()
                        {
                            Id = int.Parse(txt_ticketCategoryId.Text),
                            Name = txt_ticketCategoryName.Text
                        };
                        bool answer = businessLayerClass.InsertTicketCategoryBl(ticketCategory);
                        if (answer is true)
                        {
                            MessageBox.Show("Ticket Category Added Succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Ticket category already exists");
                        }
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Data");
                    }
                    ClearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Data");
                }
                ClearText();
            }    
        }

        private void btn_updateTicketCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ticketCategoryId.Text) | string.IsNullOrEmpty(txt_ticketCategoryName.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                Regex stringOnly = new Regex(@"^[A-Za-z ]+$"); //Regex for string format and space only
                try
                {
                    if (stringOnly.IsMatch(txt_ticketCategoryName.Text))
                    {
                        TicketCategory ticketCategory = new TicketCategory()
                        {
                            Id = int.Parse(txt_ticketCategoryId.Text),
                            Name = txt_ticketCategoryName.Text
                        };
                        bool answer = businessLayerClass.UpdateTicketCategoryBl(ticketCategory);
                        if (answer is true)
                        {
                            MessageBox.Show("Ticket Category Updated Succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Invalid Ticket category Id");
                        }
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Data");
                    }
                    ClearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter Proper Data");
                }
                ClearText();
            }
        }
        private void btn_deleteTicketCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ticketCategoryId.Text))
            {
                MessageBox.Show("Enter Ticket Category ID to delete");
            }
            else
            {
                try
                {
                    int Id = int.Parse(txt_ticketCategoryId.Text);
                    bool answer = businessLayerClass.DeleteTicketCategoryBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Ticket Category Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Ticket category does not exists");
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

        private void btn_viewTicketCategory_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessLayerClass.ViewTicketCategoryBl();
            dg_ticketCategoryDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
