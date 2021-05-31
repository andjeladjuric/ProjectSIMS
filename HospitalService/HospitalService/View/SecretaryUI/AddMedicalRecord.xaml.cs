using HospitalService.Repositories;
using HospitalService.Service;
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

namespace HospitalService.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for AddMedicalRecord.xaml
    /// </summary>
    public partial class AddMedicalRecord : Window
    {
        public Patient patient { get; set; }
        public MedicalRecordService store { get; set; }
        public PatientService stor { get; set; }
        public AddMedicalRecord(MedicalRecordService mrs, Patient p, PatientService ps)
        {
            InitializeComponent();
            store = mrs;
            patient = p;
            stor = ps;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String Id = id.Text;
            new MedicalRecordsRepository().Save(new MedicalRecord{ Id = Id, Patient = patient });
            new PatientsRepository().addRecord(patient.Jmbg, Id);
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
