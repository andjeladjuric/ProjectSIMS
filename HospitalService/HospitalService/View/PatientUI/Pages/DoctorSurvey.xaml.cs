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
using Model;
using System.Linq;
using HospitalService.Model;
using HospitalService.Storage;
using HospitalService.Service;
using HospitalService.View.PatientUI.ViewsModel;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for DoctorSurvey.xaml
    /// </summary>
    public partial class DoctorSurvey : Page
    {
       
        private DoctorSurveyViewModel viewModel;
        public DoctorSurvey(Patient patient, Doctor doctor)
        {
            InitializeComponent();
            viewModel = new DoctorSurveyViewModel(patient,doctor,this);
            this.DataContext = viewModel;
            
            

        }

        
    }
}
