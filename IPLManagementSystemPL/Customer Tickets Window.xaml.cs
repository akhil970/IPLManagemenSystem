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
using System.Threading;
using IPLManagementSystemBL;
using System.Data;
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Customer_Tickets_Window.xaml
    /// </summary>
    public partial class Customer_Tickets_Window : Window
    {
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        public Customer_Tickets_Window()
        {
            InitializeComponent();
            DataTable selectMatch = businessLayerClass.CustomerSelectMatchBl();
            for (int i = 0; i < selectMatch.Rows.Count; i++)
            {
                cmb_selectMatch.Items.Add(selectMatch.Rows[i]["Match"]);
            }
        }

        private void btn_ticketCategory_Click(object sender, RoutedEventArgs e)
        {
            DataTable ticketCategory = businessLayerClass.CustomerTicketCategoryBl();
            dg_customerTicketsDataGrid.ItemsSource = ticketCategory.DefaultView;
        }

        private void cmb_selectMatch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable selectTicketCategory = businessLayerClass.CustomerTicketCategoryBl();
            cmb_selectTicketCategory.Items.Clear();
            for (int i = 0; i < selectTicketCategory.Rows.Count; i++)
            {
                cmb_selectTicketCategory.Items.Add(selectTicketCategory.Rows[i]["Ticket_Name"]);
            }
        }
        private void btn_bookTicket_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your process has been initialisng, Please Wait!");
            Thread.Sleep(5000);
            MessageBox.Show($"          Your Ticket for Match\n       {cmb_selectMatch.SelectedValue}\n         has been booked succesfully");
        } 
    }
}
