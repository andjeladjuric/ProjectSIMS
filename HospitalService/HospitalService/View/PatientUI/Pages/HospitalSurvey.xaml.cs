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

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for HospitalSurvey.xaml
    /// </summary>
    public partial class HospitalSurvey : Page
    {
        
        private HospitalSurveyViewModel viewModel;
        public HospitalSurvey(Patient patient)
        {
            InitializeComponent();
            viewModel = new HospitalSurveyViewModel(patient, this);
            this.DataContext = viewModel;
            
        }

       
    }
}
