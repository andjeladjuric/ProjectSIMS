﻿using HospitalService.Model;
using HospitalService.View.DoctorUI.ViewModel;
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

namespace HospitalService.View.DoctorUI.Views
{
    /// <summary>
    /// Interaction logic for AboutTreatmentView.xaml
    /// </summary>
    public partial class AboutTreatmentView : Page
    {
        public AboutTreatmentView(HospitalTreatment treatment)
        {
            InitializeComponent();
            this.DataContext = new AboutTreatmentViewModel(treatment);
        }
    }
}