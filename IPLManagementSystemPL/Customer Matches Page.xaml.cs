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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using IPLManagementSystemBL;
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for Customer_Matches_Page.xaml
    /// </summary>
    public partial class Customer_Matches_Page : Page
    {
        public Customer_Matches_Page()
        {
            InitializeComponent();
            BusinessLayerClass businessLayerClass = new BusinessLayerClass();
            DataTable customerViewMatches = businessLayerClass.CustomerMatchesBl();
            dg_customerDataGrid1.ItemsSource = customerViewMatches.DefaultView;
        }

    }
}
