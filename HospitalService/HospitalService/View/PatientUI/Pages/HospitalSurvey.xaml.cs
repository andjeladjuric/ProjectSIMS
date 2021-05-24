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
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for HospitalSurvey.xaml
    /// </summary>
    public partial class HospitalSurvey : Page
    {
        private HospitalSurveyService hospitalSurveyService;
        public Patient surveyor { get; set; }       
        public HospitalSurvey(Patient patient)
        {
            InitializeComponent();
            surveyor = patient;
            hospitalSurveyService = new HospitalSurveyService();
        }

        private void doHospitalSurvey(object sender, RoutedEventArgs e)
        {
            String ratingForStaffExpertise;
            if (NStaffExpertise.IsChecked == true)
            {
                ratingForStaffExpertise = NStaffExpertise.Content.ToString();
            }
            else if (DZStaffExpertise.IsChecked == true)
            {
                ratingForStaffExpertise = DZStaffExpertise.Content.ToString();
            }
            else
            {
                ratingForStaffExpertise = ZStaffExpertise.Content.ToString();
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

            String ratingForTimeliness;
            if (NTimeliness.IsChecked == true)
            {
                ratingForTimeliness = NTimeliness.Content.ToString();
            }
            else if (DZTimeliness.IsChecked == true)
            {
                ratingForTimeliness = DZTimeliness.Content.ToString();
            }
            else
            {
                ratingForTimeliness = ZTimeliness.Content.ToString();
            }

            String ratingForHygiene;
            if (NHygiene.IsChecked == true)
            {
                ratingForHygiene = NHygiene.Content.ToString();
            }
            else if (DZHygiene.IsChecked == true)
            {
                ratingForHygiene = DZHygiene.Content.ToString();
            }
            else
            {
                ratingForHygiene = ZHygiene.Content.ToString();
            }

            String ratingForQualityOfService;
            if (NQualityOfService.IsChecked == true)
            {
                ratingForQualityOfService = NQualityOfService.Content.ToString();
            }
            else if (DZQualityOfService.IsChecked == true)
            {
                ratingForQualityOfService = DZQualityOfService.Content.ToString();
            }
            else
            {
                ratingForQualityOfService = ZQualityOfService.Content.ToString();
            }

            String ratingForServicePrices;
            if (NServicePrices.IsChecked == true)
            {
                ratingForServicePrices = NServicePrices.Content.ToString();
            }
            else if (DZServicePrices.IsChecked == true)
            {
                ratingForServicePrices = DZServicePrices.Content.ToString();
            }
            else
            {
                ratingForServicePrices = ZServicePrices.Content.ToString();
            }

            SurveyHospitalPatient newSurvey = new SurveyHospitalPatient {StaffExpertise=ratingForStaffExpertise, StaffCourtesy=ratingForCourtesy, WaitingForReception=ratingForTimeliness, RoomHygiene=ratingForHygiene, QualityOfService=ratingForQualityOfService, ServicePrices=ratingForServicePrices, patient=surveyor, ExecutionTime=DateTime.Now };
            hospitalSurveyService.saveHospitalSurvey(newSurvey);
            this.NavigationService.Navigate(new Surveys(surveyor));
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Surveys(surveyor));
        }
    }
}
