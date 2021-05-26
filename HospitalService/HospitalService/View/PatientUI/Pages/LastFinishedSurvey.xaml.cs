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
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for LastFinishedSurvey.xaml
    /// </summary>
    public partial class LastFinishedSurvey : Page
    {
        private LastFinishedSurveyViewModel viewModel;
        public LastFinishedSurvey(Doctor surveyedDoctor, SurveyDoctorPatient lastFinishedDoctorSurvey)
        {

            InitializeComponent();
            viewModel = new LastFinishedSurveyViewModel(surveyedDoctor, lastFinishedDoctorSurvey);
            this.DataContext = viewModel;
            

        }
    }
}
