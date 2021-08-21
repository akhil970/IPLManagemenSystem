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
using IPLManagementSystemBL; //Referring Business Layer
using IPLManagementSystemDoL; //Referring Domain Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Customer_Window.xaml
    /// </summary>
    public partial class Customer_Window : Window
    {
        public Customer_Window()
        {
            InitializeComponent();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_customerTeams_Click(object sender, RoutedEventArgs e)
        {
            Customer_Teams_Window customerTeamsWindow = new Customer_Teams_Window();
            customerTeamsWindow.ShowDialog();
        }

        private void btn_customerTickets_Click(object sender, RoutedEventArgs e)
        {
            Customer_Tickets_Window customerTicketsWindow = new Customer_Tickets_Window();
            customerTicketsWindow.ShowDialog();
        }

        private void btn_customerNews_Click(object sender, RoutedEventArgs e)
        {
            DataTable customerViewNews = new DataTable();
            customerViewNews = businessLayerClass.CustomerNewsBl();
            dg_customerDataGrid_News.ItemsSource = customerViewNews.DefaultView;
        }

        private void btn_customerStatistics_Click(object sender, RoutedEventArgs e)
        {
            DataTable customerviewMatchStatistics = businessLayerClass.CustomerStatisticsBl();
            dg_customerDataGrid_Statistics.ItemsSource = customerviewMatchStatistics.DefaultView;
        }

    }
}
