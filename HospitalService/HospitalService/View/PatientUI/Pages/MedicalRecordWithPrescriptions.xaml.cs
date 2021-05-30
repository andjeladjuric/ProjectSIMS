using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
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
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MedicalRecordWithPrescriptions.xaml
    /// </summary>
    public partial class MedicalRecordWithPrescriptions : Page
    {

       
        private PatientPrescriptionsViewModel viewModel;
        public MedicalRecordWithPrescriptions(Patient patient)
        {
            InitializeComponent();
            viewModel = new PatientPrescriptionsViewModel(patient);
            this.DataContext = viewModel;
           
        }

       
    }
}
