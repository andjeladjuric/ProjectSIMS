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
    /// Interaction logic for TransferInventory.xaml
    /// </summary>
    public partial class TransferItemView : Page
    {
        TransferItemViewModel currentViewModel;
        public TransferItemView(string SelectedFirst, string SelectedSecond, bool DemoOn)
        {
            InitializeComponent();
            currentViewModel = new TransferItemViewModel(newFrame, SelectedFirst, SelectedSecond, DemoOn);
            this.DataContext = currentViewModel;
        }
    }
}
