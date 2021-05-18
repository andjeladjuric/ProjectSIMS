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

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for DoctorSurvey.xaml
    /// </summary>
    public partial class DoctorSurvey : Page
    {
        private DoctorSurveyService doctorSurveyService;
        public Patient surveyor { get; set; }
        public Doctor surveyedDoctor { get; set; }
        public DoctorSurvey(Patient patient, Doctor doctor)
        {
            InitializeComponent();
            surveyor = patient;
            surveyedDoctor = doctor;
            lbDoctor.Content = surveyedDoctor.Name + " " + doctor.Surname;
            doctorSurveyService = new DoctorSurveyService();
            this.DataContext = this;
            

        }

        private void doDoctorSurvey(object sender, RoutedEventArgs e)
        {
            String ratingForCommunication;
            if (NCommunication.IsChecked == true)
            {
                ratingForCommunication = NCommunication.Content.ToString();
            }
            else if (DZCommunication.IsChecked == true)
            {
                ratingForCommunication = DZCommunication.Content.ToString();
            }
            else
            {
                ratingForCommunication = ZCommunication.Content.ToString();
            }

            String ratingForCourtesy;
            if (NCourtesy.IsChecked == true)
            {
                ratingForCourtesy = NCourtesy.Content.ToString();
            }
            else if (DZCourtesy.IsChecked == true)
            {
                ratingForCourtesy = DZCourtesy.Content.ToString();
            }
            else
            {
                ratingForCourtesy = ZCourtesy.Content.ToString();
            }

            String ratingForProfessionalism;
            if (NProfessionalism.IsChecked == true)
            {
                ratingForProfessionalism = NProfessionalism.Content.ToString();
            }
            else if (DZProfessionalism.IsChecked == true)
            {
                ratingForProfessionalism = DZProfessionalism.Content.ToString();
            }
            else
            {
                ratingForProfessionalism = ZProfessionalism.Content.ToString();
            }

            String ratingForDoctorCare;
            if (NDoctorCare.IsChecked == true)
            {
                ratingForDoctorCare = NDoctorCare.Content.ToString();
            }
            else if (DZDoctorCare.IsChecked == true)
            {
                ratingForDoctorCare = DZDoctorCare.Content.ToString();
            }
            else
            {
                ratingForDoctorCare = ZDoctorCare.Content.ToString();
            }

            String ratingForProvidingInformation;
            if (NProvidingInformation.IsChecked == true)
            {
                ratingForProvidingInformation = NProvidingInformation.Content.ToString();
            }
            else if (DZProvidingInformation.IsChecked == true)
            {
                ratingForProvidingInformation = DZProvidingInformation.Content.ToString();
            }
            else
            {
                ratingForProvidingInformation = ZProvidingInformation.Content.ToString();
            }

            String ratingForDevotedTime;
            if (NDevotedTime.IsChecked == true)
            {
                ratingForDevotedTime = NDevotedTime.Content.ToString();
            }
            else if (DZDevotedTime.IsChecked == true)
            {
                ratingForDevotedTime = DZDevotedTime.Content.ToString();
            }
            else
            {
                ratingForDevotedTime = ZDevotedTime.Content.ToString();
            }   
            
            SurveyDoctorPatient newSurvey = new SurveyDoctorPatient {Communication=ratingForCommunication, Courtesy=ratingForCourtesy, Professionalism=ratingForProfessionalism, CareForPatient=ratingForDoctorCare, ProvidingInformation=ratingForProvidingInformation, DevotedTime=ratingForDevotedTime, doctor= surveyedDoctor, patient=surveyor, ExecutionTime=DateTime.Now };
            doctorSurveyService.saveDoctorSurvey(newSurvey);
            this.NavigationService.Navigate(new Surveys(surveyor));


        }

        

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Surveys(surveyor));
            
        }
    }
}
