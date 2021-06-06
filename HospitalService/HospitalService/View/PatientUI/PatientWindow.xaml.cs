using HospitalService.Service;
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using System;
using System.Timers;
using System.Windows;
using System.Collections.Generic;


namespace HospitalService.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
       
       
        public PatientWindowViewModel viewModel { get; set; }
        public PatientWindow(Patient patient)
        {
            InitializeComponent();
          
            this.viewModel = new PatientWindowViewModel(this.Main.NavigationService,patient,this);
            this.DataContext = this.viewModel;

            

        }

        


    }
}
