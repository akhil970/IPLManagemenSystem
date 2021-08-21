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
using IPLManagementSystemBL; //Referencing Business Layer
using IPLManagementSystemDoL; //Referencing Domain Layer
namespace IPLManagementSystemPL
{
    /// <summary>
    /// Interaction logic for News_Window.xaml
    /// </summary>
    public partial class News_Window : Window
    {
        public News_Window()
        {
            InitializeComponent();
        }
        BusinessLayerClass businessLayerClass = new BusinessLayerClass();
        private void btn_addNews_Click(object sender, RoutedEventArgs e)
        {
            if(txt_newsId is null)
            {
                MessageBox.Show("Enter Id");
            }
            else
            {
                News news = new News()
                {
                    Id = int.Parse(txt_newsId.Text),
                    NewsDate = DateTime.Parse(txt_newsDate.Text),
                    MatchId = int.Parse(txt_newsMatchId.Text),
                    Description = txt_newsDescription.Text
                };
                bool answer = businessLayerClass.InsertNewsBl(news);
                if(answer==true)
                {
                    MessageBox.Show("News inserted Successfully");
                }
                else
                {
                    MessageBox.Show("News already exists");
                }
            }

        }

        private void btn_updateNews_Click(object sender, RoutedEventArgs e)
        {
            if (txt_newsId is null)
            {
                MessageBox.Show("Enter Id");
            }
            else
            {
                News news = new News()
                {
                    Id = int.Parse(txt_newsId.Text),
                    NewsDate = DateTime.Parse(txt_newsDate.Text),
                    MatchId = int.Parse(txt_newsMatchId.Text),
                    Description = txt_newsDescription.Text
                };
                bool answer = businessLayerClass.UpdateNewsBl(news);
                if (answer is true)
                {
                    MessageBox.Show("News Updated Successfully");
                }
                else
                {
                    MessageBox.Show("News already exists");
                }
            }
        }

        private void btn_deleteNews_Click(object sender, RoutedEventArgs e)
        {
            if(txt_newsId is null)
            {
                MessageBox.Show("Enter Id");
            }
            else
            {
                int Id = int.Parse(txt_newsId.Text);
                bool answer = businessLayerClass.DeleteNewsBl(Id);
                if(answer == true)
                {
                    MessageBox.Show("News Deleted");
                }
                else
                {
                    MessageBox.Show("News Does not exist");
                }
            }
        }

        private void btn_viewNews_Click(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();
            datatable = businessLayerClass.ViewNewsBl();
            dg_newsDataGrid.ItemsSource = datatable.DefaultView;
        }
    }
}
