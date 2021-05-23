﻿using HospitalService.View.DoctorUI.ViewModel;
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
    /// Interaction logic for HospitalTreatmentView.xaml
    /// </summary>
    public partial class HospitalTreatmentView : Window
    {
        public HospitalTreatmentView(MedicalRecordViewModel parent)
        {
            InitializeComponent();
            this.DataContext = new HospitalTreatmentViewModel(parent, this);
        }
    }
}
