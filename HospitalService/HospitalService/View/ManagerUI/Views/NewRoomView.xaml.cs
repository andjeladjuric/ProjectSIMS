using HospitalService.Service;
using HospitalService.View.ManagerUI.Validations;
using HospitalService.View.ManagerUI.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for NewRoomView.xaml
    /// </summary>
    public partial class NewRoomView : Page
    {
        NewRoomViewModel currentViewModel;
        public NewRoomView(bool demoOn)
        {
            InitializeComponent();
            currentViewModel = new NewRoomViewModel(newFrame, demoOn);
            this.DataContext = currentViewModel;
        }
    }
}


