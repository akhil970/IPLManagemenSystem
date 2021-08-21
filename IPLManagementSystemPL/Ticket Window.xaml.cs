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
    /// Interaction logic for Ticket_Window.xaml
    /// </summary>
    public partial class Ticket_Window : Window
    {
        public Ticket_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_ticketId.Clear();
            txt_ticketMatchId.Clear();
            txt_ticketCategoryId.Clear();
            txt_ticketPrice.Clear();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addTicket_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ticketId.Text) | string.IsNullOrEmpty(txt_ticketMatchId.Text)
                | string.IsNullOrEmpty(txt_ticketCategoryId.Text) | string.IsNullOrEmpty(txt_ticketPrice.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = int.Parse(txt_ticketId.Text),
                        MatchId = int.Parse(txt_ticketMatchId.Text),
                        CategoryId = int.Parse(txt_ticketCategoryId.Text),
                        Price = decimal.Parse(txt_ticketPrice.Text)
                    };
                    bool answer = businessLayerClass.InsertTicketBl(ticket);
                    if (answer is true)
                    {
                        MessageBox.Show("Ticket added successfully");
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Data");
                    }
                    clearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Data");
                }
                clearText();
            }
        }
        private void btn_updateTicket_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ticketId.Text) | string.IsNullOrEmpty(txt_ticketMatchId.Text)
                | string.IsNullOrEmpty(txt_ticketCategoryId.Text) | string.IsNullOrEmpty(txt_ticketPrice.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = int.Parse(txt_ticketId.Text),
                        MatchId = int.Parse(txt_ticketMatchId.Text),
                        CategoryId = int.Parse(txt_ticketCategoryId.Text),
                        Price = decimal.Parse(txt_ticketPrice.Text)
                    };
                    bool answer = businessLayerClass.UpdateTicketBl(ticket);
                    if (answer is true)
                    {
                        MessageBox.Show("Ticket Updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Enter Proper Data");
                    }
                    clearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter Proper Data");
                }
                clearText();
            }
        }

        private void btn_deleteTicket_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ticketId.Text))
            {
                MessageBox.Show("Enter Ticket Id to delete");
            }
            else
            {
                try
                {
                    int Id = int.Parse(txt_ticketId.Text);
                    bool answer = businessLayerClass.DeleteTicketBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Ticket Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Ticket Does not exists");
                    }
                    clearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper ID");
                }
                clearText();
            }
        }

        private void btn_viewTicket_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessLayerClass.ViewTicketBl();
            dg_ticketDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
