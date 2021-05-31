using Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HospitalService.Service;
using HospitalService.Repositories;

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for NewsWindow.xaml
    /// </summary>
    public partial class NewsWindow : Window
    {
        private NewsRepository storage = NewsRepository.getInstance();
        private static NewsWindow instance = null;

        public static NewsWindow getInstance()
        {
            if (instance == null)
            {
                instance = new NewsWindow();

            }
            return instance;
        }
        public NewsWindow()
        {
            InitializeComponent();
            
            this.refreshListViewData();

        }

        public News getSelectedNews()
        {
            return (News)lvNews.SelectedItem;
        }



        public void refreshListViewData()
        {
            lvNews.ItemsSource = null;
            lvNews.ItemsSource = NewsRepository.getInstance().GetAll();
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            AddNews addNewsWindow = new AddNews();
            addNewsWindow.Show();
            
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            News newsForDeleting = getSelectedNews();

            if (newsForDeleting == null)
            {
                MessageBox.Show("News must be selected.");
            }
            else
            {
                storage.Delete(newsForDeleting.Title);
                
                refreshListViewData();
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            News selectedNews = getSelectedNews();

            if (selectedNews == null)
            {
                MessageBox.Show("News must be selected.");
                return;
            }
            else
            {
                EditNews editNewsWindow = new EditNews(selectedNews);
                editNewsWindow.Show();
            }
        }

        private void NewsWindow_Closing(object sender, EventArgs e)
        {
           
            
                instance = null;
            
        }


        private void read_Click(object sender, RoutedEventArgs e)
        {
            News selectedNews = getSelectedNews();

            if (selectedNews != null)
            {
                ViewNews viewNewsWindow = new ViewNews(selectedNews);
                viewNewsWindow.Show();
            }
            else
            {
                MessageBox.Show("News must be selected.");
            }
        }

       
    }
}
