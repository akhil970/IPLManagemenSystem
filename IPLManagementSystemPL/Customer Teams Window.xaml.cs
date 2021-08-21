using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
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
    /// Interaction logic for Customer_Teams_Window.xaml
    /// </summary>
    public partial class Customer_Teams_Window : Window
    {
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        public Customer_Teams_Window()
        {
            InitializeComponent();
            DataTable answer = businessLayerClass.CustomerSelectteamBl();
            for (int i = 0; i < answer.Rows.Count; i++)
            {
                cmb_selectTeam.Items.Add(answer.Rows[i]["Name"]);
            }
        }

        private void cmb_selectTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
            DataTable answer2 = businessLayerClass.CustomerSelectPlayerBl(Convert.ToString(cmb_selectTeam.SelectedValue));
            //Idi chudu, work avthadi ankuntunna
            cmb_selectPlayer.Items.Clear();
            for (int i = 0; i < answer2.Rows.Count; i++)
            {
                cmb_selectPlayer.Items.Add(answer2.Rows[i]["Name"]);
            }
        }

        private void cmb_selectPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable playerInformation = businessLayerClass.CustomerPlayerDetailsBl(Convert.ToString(cmb_selectPlayer.SelectedValue));
            dg_customerTeamsDataGrid.ItemsSource = playerInformation.DefaultView;
        }

        
    }
}
