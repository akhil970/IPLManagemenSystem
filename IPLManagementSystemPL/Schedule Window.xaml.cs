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
using IPLManagementSystemBL; //Referrring Business Layer
using IPLManagementSystemDoL; //Referring Domain Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Schedule_Window.xaml
    /// </summary>
    public partial class Schedule_Window : Window
    {
        public Schedule_Window()
        {
            InitializeComponent();
        }
        public void ClearText()
        {
            txt_sheduleId.Clear();
            txt_scheduleMatchId.Clear();
            txt_scheduleDate.Clear();
            txt_scheduleVenueId.Clear();
            txt_scheduleStartTime.Clear();
            txt_scheduleEndTime.Clear();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addSchedule_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_sheduleId.Text) | string.IsNullOrEmpty(txt_scheduleDate.Text) | string.IsNullOrEmpty(txt_scheduleMatchId.Text) |
                string.IsNullOrEmpty(txt_scheduleVenueId.Text) | string.IsNullOrEmpty(txt_scheduleStartTime.Text) | string.IsNullOrEmpty(txt_scheduleEndTime.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Schedule schedule = new Schedule()
                    {
                        Id = int.Parse(txt_sheduleId.Text),
                        Date = DateTime.Parse(txt_scheduleDate.Text),
                        MatchId = int.Parse(txt_scheduleMatchId.Text),
                        VenueId = int.Parse(txt_scheduleVenueId.Text),
                        StartTime = DateTime.Parse(txt_scheduleStartTime.Text),
                        EndTime = DateTime.Parse(txt_scheduleEndTime.Text)
                    };
                    bool answer = businessLayerClass.InsertScheduleBl(schedule);
                    if (answer is true)
                    {
                        MessageBox.Show("Schedule Added Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Schedule already exists");
                    }
                    ClearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Date and Time Properly\nEnter only integers in place of Id's");
                }
                ClearText();
            }
        }

        private void btn_updateSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_sheduleId.Text) | string.IsNullOrEmpty(txt_scheduleDate.Text) | string.IsNullOrEmpty(txt_scheduleMatchId.Text) |
                string.IsNullOrEmpty(txt_scheduleVenueId.Text) | string.IsNullOrEmpty(txt_scheduleStartTime.Text) | string.IsNullOrEmpty(txt_scheduleEndTime.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Schedule schedule = new Schedule()
                    {
                        Id = int.Parse(txt_sheduleId.Text),
                        Date = DateTime.Parse(txt_scheduleDate.Text),
                        MatchId = int.Parse(txt_scheduleMatchId.Text),
                        VenueId = int.Parse(txt_scheduleVenueId.Text),
                        StartTime = DateTime.Parse(txt_scheduleStartTime.Text),
                        EndTime = DateTime.Parse(txt_scheduleEndTime.Text)
                    };
                    bool answer = businessLayerClass.UpdateScheduleBl(schedule);
                    if (answer is true)
                    {
                        MessageBox.Show("Shcedule Updated Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Schedule Id");
                    }
                    ClearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter Date and Time Properly\nEnter only integers in place of Id's");
                }
                ClearText();
            }
        }

        private void btn_deleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_sheduleId.Text))
            {
                MessageBox.Show("Enter Schedule id to delete");
            }
            else
            {
                try
                {
                    int Id = int.Parse(txt_sheduleId.Text);
                    bool answer = businessLayerClass.DeleteScheduleBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Shcedule Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Schedule Id or Schedule does not exists");
                    }
                    ClearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Integer only");
                }
                ClearText();
            }
        }

        private void btn_viewSchedule_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessLayerClass.ViewScheduleBl();
            dg_scheduleDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
