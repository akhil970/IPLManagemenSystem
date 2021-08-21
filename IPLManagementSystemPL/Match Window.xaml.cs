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
using IPLManagementSystemBL; //Referring Business layer
using IPLManagementSystemDoL; //Referring Domain layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Match_Window.xaml
    /// </summary>
    public partial class Match_Window : Window
    {
        public Match_Window()
        {
            InitializeComponent();
        }
        public void clearText()
        {
            txt_ID.Clear();
            txt_TeamTwoID.Clear();
            txt_TeamOneID.Clear();
            txt_matchPhoto.Clear();
            txt_VenueID.Clear();
            txt_ScheduleID.Clear();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addMatch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ID.Text) | string.IsNullOrEmpty(txt_TeamOneID.Text) | string.IsNullOrEmpty(txt_TeamTwoID.Text) |
                string.IsNullOrEmpty(txt_ScheduleID.Text) | string.IsNullOrEmpty(txt_matchPhoto.Text) | string.IsNullOrEmpty(txt_VenueID.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try 
                {
                    Regex filePath = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"); //Regex for file location path only    
                    if (filePath.Match(txt_matchPhoto.Text).Success)
                    {
                        Matches match = new Matches()
                        {
                            Id = int.Parse(txt_ID.Text),
                            TeamOneId = int.Parse(txt_TeamOneID.Text),
                            TeamTwoId = int.Parse(txt_TeamTwoID.Text),
                            ScheduledId = int.Parse(txt_VenueID.Text),
                            VenueId = int.Parse(txt_VenueID.Text),
                            MatchPhoto = txt_matchPhoto.Text
                        };
                        bool answer = businessLayerClass.InsertMatchBl(match);
                        if (answer == true)
                        {
                            MessageBox.Show("Match Details Added");
                        }
                        else
                        {
                            MessageBox.Show("Match Details Exists already");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter proper match photo path");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Data");
                }
                clearText(); 
            }
        }

        private void btn_updateMatch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ID.Text) | string.IsNullOrEmpty(txt_TeamOneID.Text) | string.IsNullOrEmpty(txt_TeamTwoID.Text) |
    string.IsNullOrEmpty(txt_ScheduleID.Text) | string.IsNullOrEmpty(txt_matchPhoto.Text) | string.IsNullOrEmpty(txt_VenueID.Text))
            {
                MessageBox.Show("Enter all the details");
            }
            else
            {
                try
                {
                    Regex filePath = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"); //Regex for file location path only    
                    if (filePath.Match(txt_matchPhoto.Text).Success)
                    {
                        Matches match = new Matches()
                        {
                            Id = int.Parse(txt_ID.Text),
                            TeamOneId = int.Parse(txt_TeamOneID.Text),
                            TeamTwoId = int.Parse(txt_TeamTwoID.Text),
                            ScheduledId = int.Parse(txt_VenueID.Text),
                            VenueId = int.Parse(txt_VenueID.Text),
                            MatchPhoto = txt_matchPhoto.Text
                        };
                        bool answer = businessLayerClass.UpdateMatchBl(match);
                        if (answer == true)
                        {
                            MessageBox.Show("Match Details Updated");
                        }
                        else
                        {
                            MessageBox.Show("Match Details Exists already");
                        }
                        clearText();
                    }
                    else
                    {
                        MessageBox.Show("Enter proper match photo path");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter Proper Data");
                }
                clearText();
            }
        }

        private void btn_deleteMatch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ID.Text))
            {
                MessageBox.Show("Enter Id");
            }
            else            
            {
                try
                {
                    int Id = int.Parse(txt_ID.Text);
                    bool answer = businessLayerClass.DeleteMatchBl(Id);
                    if (answer is true)
                    {
                        MessageBox.Show("Match Details Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Match Details does not exist");
                    }
                    clearText();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Proper Id");
                }
                clearText();
            }
        }

        private void btn_viewMatch_Click(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();
            datatable = businessLayerClass.ViewMatchBl();
            dg_matchDataGrid.ItemsSource = datatable.DefaultView;
        }
    }
}
