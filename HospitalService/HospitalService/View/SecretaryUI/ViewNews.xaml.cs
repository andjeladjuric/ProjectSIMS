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
using HospitalService.Repositories;


namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for ViewNews.xaml
    /// </summary>
    public partial class ViewNews : Window
    {
        public ViewNews(News selectedNews)
        {
            InitializeComponent();

            readNewsToShow(selectedNews);
        }

        private void readNewsToShow(News selectedNews)
        {
            titleText.Content = selectedNews.Title;
            publishingDate.Content = selectedNews.PublishingDate;
            if (selectedNews.Roles == Role.specificniPacijent)
            {
                role.Content = selectedNews.Roles + " (" + selectedNews.specificPatient.Name + selectedNews.specificPatient.Surname + ")";
            }
            else
            {
                role.Content = selectedNews.Roles;
            }
            newsText.Text = selectedNews.Content;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
