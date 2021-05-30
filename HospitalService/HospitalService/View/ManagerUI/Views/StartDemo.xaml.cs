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
    /// Interaction logic for StartDemo.xaml
    /// </summary>
    public partial class StartDemo : UserControl
    {
        StartDemoViewModel currentViewModel;
        public StartDemo()
        {
            InitializeComponent();
            currentViewModel = new StartDemoViewModel(this);
            this.DataContext = currentViewModel;
        }
    }
}
