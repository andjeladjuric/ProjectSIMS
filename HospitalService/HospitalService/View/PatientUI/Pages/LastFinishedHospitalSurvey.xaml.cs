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
using HospitalService.View.PatientUI.ViewsModel;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for LastFinishedHospitalSurvey.xaml
    /// </summary>
    public partial class LastFinishedHospitalSurvey : Page
    {
        private LastFinishedHospitalSurveyViewModel viewModel;
        public LastFinishedHospitalSurvey(SurveyHospitalPatient lastFinishedHospitalSurvey)
        {
            InitializeComponent();
            viewModel = new LastFinishedHospitalSurveyViewModel(lastFinishedHospitalSurvey);
            this.DataContext = viewModel;
            

        }
    }
}
