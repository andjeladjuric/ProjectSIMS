using HospitalService.View.ManagerUI.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalService.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for DoctorsView.xaml
    /// </summary>
    public partial class DoctorsView : Page
    {
        DoctorsViewModel currentViewModel;
        public DoctorsView()
        {
            InitializeComponent();
            currentViewModel = new DoctorsViewModel(newFrame);
            this.DataContext = currentViewModel;
        }
    }
}
