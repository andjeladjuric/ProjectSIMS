﻿using Model;
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

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for MedicalRecordDoctorWindow.xaml
    /// </summary>
    public partial class MedicalRecordDoctorWindow : Window
    {
        public MedicalRecord Karton { get; set; }

        public MedicalRecordDoctorWindow(MedicalRecord md)
        {
            InitializeComponent();
            Karton = md;
            MedicalRecordNum.Text = md.Id;
            IstorijaList.ItemsSource = md.Diagnoses;
            TerapijaList.ItemsSource = md.Prescriptions;
            NameLbl.Content = md.Patient.Name + " " + md.Patient.Surname;
            DateOfBirthTB.Text = md.Patient.DateOfBirth.HasValue ? md.Patient.DateOfBirth.Value.ToString("MM/dd/yyyy") : " ";
            JmbgTB.Text = md.Patient.Jmbg;
            AddressTB.Text = md.Patient.Address;
            ContactTB.Text = md.Patient.Phone;

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow(this);
            historyWindow.ShowDialog();

        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
             PrescriptionWindow prescriptionWindow = new PrescriptionWindow(this);
             prescriptionWindow.ShowDialog();
        }

        public void Refresh()
        {
            IstorijaList.ItemsSource = Karton.Diagnoses;
            TerapijaList.ItemsSource = Karton.Prescriptions;
            IstorijaList.Items.Refresh();
            TerapijaList.Items.Refresh();
        }
    }
}
