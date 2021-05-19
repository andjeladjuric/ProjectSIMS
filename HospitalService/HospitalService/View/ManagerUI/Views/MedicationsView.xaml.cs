using HospitalService.View.ManagerUI.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MedicationsView.xaml
    /// </summary>
    public partial class MedicationsView : Page
    {
        MedicationsViewModel currentViewModel;
        public MedicationsView()
        {
            InitializeComponent();
            currentViewModel = new MedicationsViewModel(newFrame);
            this.DataContext = currentViewModel;
        }

    }
}
