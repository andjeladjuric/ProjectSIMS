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
using HospitalService.Service;
using HospitalService.Storage;
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordForPatient.xaml
    /// </summary>
    public partial class MedicalRecordForPatient : Page
    {
       

        private MedicalRecordForPatientViewModel viewModel;
        public MedicalRecordForPatient(Patient patient)
        {
            InitializeComponent();
            viewModel = new MedicalRecordForPatientViewModel(patient, RecordPage.NavigationService);
            this.DataContext = viewModel;
           
        }

        

        
    }
}
