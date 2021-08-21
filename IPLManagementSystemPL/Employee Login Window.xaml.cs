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
using IPLManagementSystemBL; //Referencing Business Layer
using IPLManagementSystemDoL; //Referencing Business Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Employee_Login_Window.xaml
    /// </summary>
    public partial class Employee_Login_Window : Window
    {
        public Employee_Login_Window()
        {
            InitializeComponent();
        }
        private void btn_teamsWindow_Click(object sender, RoutedEventArgs e)
        {
            
            Teams_Window teamsWindow = new Teams_Window();
            teamsWindow.ShowDialog();
        }

        private void btn_playerWindow_Click(object sender, RoutedEventArgs e)
        {
            Player_Window playerWindow = new Player_Window();
            playerWindow.ShowDialog();
        }

        private void btn_matchWindow_Click(object sender, RoutedEventArgs e)
        {

            Match_Window matchWindow = new Match_Window();
            matchWindow.ShowDialog();
        }

        private void btn_scheduleWindow_Click(object sender, RoutedEventArgs e)
        {

            Schedule_Window scheduleWindow = new Schedule_Window();
            scheduleWindow.ShowDialog();
        }

        private void btn_ticketsWindow_Click(object sender, RoutedEventArgs e)
        {

            Ticket_Window ticketWindow = new Ticket_Window();
            ticketWindow.ShowDialog();
        }

        private void btn_ticketCategoryWindow_Click(object sender, RoutedEventArgs e)
        {

            Ticket_Category_Window ticketcateogryWindow = new Ticket_Category_Window();
            ticketcateogryWindow.ShowDialog();
        }

        private void btn_statisticsWindow_Click(object sender, RoutedEventArgs e)
        {

            Statistics_Window statisticsWindow = new Statistics_Window();
            statisticsWindow.ShowDialog();
        }

        private void btn_venuWindow_Click(object sender, RoutedEventArgs e)
        {

            Venue_Window venueWindow = new Venue_Window();
            venueWindow.ShowDialog();
        }

        private void btn_newsWindow_Click(object sender, RoutedEventArgs e)
        {
            News_Window newsWindow = new News_Window();
            newsWindow.ShowDialog();
        }

        private void btn_playerPhotoWindow_Click(object sender, RoutedEventArgs e)
        {
            Player_Photo_Window playerPhotoWindow = new Player_Photo_Window();
            playerPhotoWindow.ShowDialog();
        }
    }
}
