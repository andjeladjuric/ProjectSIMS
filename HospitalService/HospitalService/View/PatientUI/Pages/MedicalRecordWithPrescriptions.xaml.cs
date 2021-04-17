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
using HospitalService.Model;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordWithPrescriptions.xaml
    /// </summary>
    public partial class MedicalRecordWithPrescriptions : Page
    {

        public Patient pacijent { get; set; }

        public List<Prescription> prescriptions { get; set; }

        public MedicalRecordStorage baza;
        public MedicalRecordWithPrescriptions(Patient p)
        {
            InitializeComponent();
            this.DataContext = this;
            pacijent = p;
            baza = new MedicalRecordStorage();
            prescriptions = baza.getByPatient(pacijent);
            tablePrescriptions.ItemsSource = prescriptions;
        }
    }
}
