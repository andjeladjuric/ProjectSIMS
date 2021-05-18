using HospitalService.View.DoctorUI.ViewModel;
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

namespace HospitalService.View.DoctorUI.Views
{
    /// <summary>
    /// Interaction logic for DoctorWindowView.xaml
    /// </summary>
    public partial class DoctorWindowView : Window
    {
        public DoctorWindowViewModel context { get; set; }
        public DoctorWindowView(Doctor loggedDoctor)
        {
            InitializeComponent();
            context = new DoctorWindowViewModel(loggedDoctor, this);
            this.DataContext = context;
        }
      
    }
}
