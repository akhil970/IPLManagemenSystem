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
using IPLManagementSystemDoL; ////Referring Domain Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Statistics_Window.xaml
    /// </summary>
    public partial class Statistics_Window : Window
    {
        public Statistics_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_againstTeamId.Clear();
            txt_statisticsId.Clear();
            txt_TeamId.Clear();
            txt_Won.Clear();
            txt_Tied.Clear();
            txt_points.Clear();
            txt_Played.Clear();
            txt_N_R.Clear();
            txt_NetRR.Clear();
            txt_Lost.Clear();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_statisticsId.Text) && string.IsNullOrEmpty(txt_Won.Text) && string.IsNullOrEmpty(txt_NetRR.Text) &&
                string.IsNullOrEmpty(txt_againstTeamId.Text) && string.IsNullOrEmpty(txt_Tied.Text) && string.IsNullOrEmpty(txt_N_R.Text) &&
                string.IsNullOrEmpty(txt_TeamId.Text) && string.IsNullOrEmpty(txt_points.Text) && string.IsNullOrEmpty(txt_Played.Text) &&
                string.IsNullOrEmpty(txt_Lost.Text))
            {
                MessageBox.Show("Enter all the data");
            }
            else
            {
                try
                {
                    Statistics addStatistics = new Statistics()
                    {
                        Id = int.Parse(txt_statisticsId.Text),
                        TeamId = int.Parse(txt_TeamId.Text),
                        Played = int.Parse(txt_Played.Text),
                        Won = int.Parse(txt_Won.Text),
                        Lost = int.Parse(txt_Lost.Text),
                        Tied = int.Parse(txt_Tied.Text),
                        NoResult = int.Parse(txt_N_R.Text),
                        AgainstTeamId = int.Parse(txt_againstTeamId.Text),
                        Points = int.Parse(txt_points.Text),
                        NetRR = double.Parse(txt_NetRR.Text),
                    };
                    bool answer = businessLayerClass.InsertStatisticsBl(addStatistics);
                    if (answer == true)
                    {
                        MessageBox.Show("Statistics Inserted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Statistics already exists");
                    }
                    clearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Input Format\\s incorrect!");
                }
                clearText();
            }
        }

        private void btn_updateStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_statisticsId.Text) && string.IsNullOrEmpty(txt_Won.Text) && string.IsNullOrEmpty(txt_NetRR.Text) &&
                string.IsNullOrEmpty(txt_againstTeamId.Text) && string.IsNullOrEmpty(txt_Tied.Text) && string.IsNullOrEmpty(txt_N_R.Text) &&
                string.IsNullOrEmpty(txt_TeamId.Text) && string.IsNullOrEmpty(txt_points.Text) && string.IsNullOrEmpty(txt_Played.Text) &&
                string.IsNullOrEmpty(txt_Lost.Text))
            {
                MessageBox.Show("Enter all the data");
            }
            else
            {
                try
                {
                    Statistics addStatistics = new Statistics()
                    {
                        Id = int.Parse(txt_statisticsId.Text),
                        TeamId = int.Parse(txt_TeamId.Text),
                        Played = int.Parse(txt_Played.Text),
                        Won = int.Parse(txt_Won.Text),
                        Lost = int.Parse(txt_Lost.Text),
                        Tied = int.Parse(txt_Tied.Text),
                        NoResult = int.Parse(txt_N_R.Text),
                        AgainstTeamId = int.Parse(txt_againstTeamId.Text),
                        Points = int.Parse(txt_points.Text),
                        NetRR = double.Parse(txt_NetRR.Text),
                    };
                    bool answer = businessLayerClass.UpdateStatisticsBl(addStatistics);
                    if (answer == true)
                    {
                        MessageBox.Show("Statistics Updated Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Id of Statistics");
                    }
                    clearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Input Format\\s incorrect!");
                }
                clearText();
            }
        }

        private void btn_deleteStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (txt_statisticsId is null)
            {
                MessageBox.Show("Enter Id to delete statistics");
            }
            else
            {
                try
                {
                    int Id = int.Parse(txt_statisticsId.Text);
                    bool answer = businessLayerClass.DeleteStatisticsBl(Id);
                    if (answer == true)
                    {
                        MessageBox.Show("Statistics Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Statistics Does not exists");
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
        //To be updated with data grid
        private void btn_viewStatistics_Click(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();
            datatable = businessLayerClass.ViewStatisticsBl();
            dg_statisticsDataGrid.ItemsSource = datatable.DefaultView;
        }
    }
}
