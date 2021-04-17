﻿using System;
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
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for PreferencesForAppointment.xaml
    /// </summary>
    public partial class PreferencesForAppointment : Page
    {

        public Patient pac { get; set; }
        public PreferencesForAppointment(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            pac = patient;
        }

        private void PreferenceClick(object sender, RoutedEventArgs e)
        {
            if (No.IsChecked == true)
            {
                AddAppointmentToPatient dodajProzor = new AddAppointmentToPatient(pac);
                dodajProzor.Show();
            }

            if (Yes.IsChecked == true)
            {
                UrgentAppointment hzt = new UrgentAppointment(pac);
                hzt.Show();
            }
        }
    }
}