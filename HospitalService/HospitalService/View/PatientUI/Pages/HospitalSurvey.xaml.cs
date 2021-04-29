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
using HospitalService.Storage;
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for HospitalSurvey.xaml
    /// </summary>
    public partial class HospitalSurvey : Page
    {

        public Patient patient { get; set; }
        
        public HospitalSurvey(Patient p)
        {
            InitializeComponent();
            patient = p;
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            String profSurvey;
            if (NStrucnost.IsChecked == true)
            {
                profSurvey = NStrucnost.Content.ToString();
            }
            else if (DZStrucnost.IsChecked == true)
            {
                profSurvey = DZStrucnost.Content.ToString();
            }
            else
            {
                profSurvey = ZStrucnost.Content.ToString();
            }

            String courtesySurvey;
            if (NLjubaznost.IsChecked == true)
            {
                courtesySurvey = NLjubaznost.Content.ToString();
            }
            else if (DZLjubaznost.IsChecked == true)
            {
                courtesySurvey = DZLjubaznost.Content.ToString();
            }
            else
            {
                courtesySurvey = ZLjubaznost.Content.ToString();
            }

            String timeSurvey;
            if (NCekanje.IsChecked == true)
            {
                timeSurvey = NCekanje.Content.ToString();
            }
            else if (DZCekanje.IsChecked == true)
            {
                timeSurvey = DZCekanje.Content.ToString();
            }
            else
            {
                timeSurvey = ZCekanje.Content.ToString();
            }

            String hygieneSurvey;
            if (NHigijena.IsChecked == true)
            {
                hygieneSurvey = NHigijena.Content.ToString();
            }
            else if (DZHigijena.IsChecked == true)
            {
                hygieneSurvey = DZHigijena.Content.ToString();
            }
            else
            {
                hygieneSurvey = ZHigijena.Content.ToString();
            }

            String serviceSurvey;
            if (NKvalitet.IsChecked == true)
            {
                serviceSurvey = NKvalitet.Content.ToString();
            }
            else if (DZKvalitet.IsChecked == true)
            {
                serviceSurvey = DZKvalitet.Content.ToString();
            }
            else
            {
                serviceSurvey = ZKvalitet.Content.ToString();
            }

            String priseSurvey;
            if (NCijene.IsChecked == true)
            {
                priseSurvey = NCijene.Content.ToString();
            }
            else if (DZCijene.IsChecked == true)
            {
                priseSurvey = DZCijene.Content.ToString();
            }
            else
            {
                priseSurvey = ZCijene.Content.ToString();
            }

            SurveyHospitalPatient a = new SurveyHospitalPatient {StaffExpertise=profSurvey, StaffCourtesy=courtesySurvey, WaitingForReception=timeSurvey, RoomHygiene=hygieneSurvey, QualityOfService=serviceSurvey, ServicePrices=priseSurvey, patient=patient, ExecutionTime=DateTime.Now };
            HospitalSurveyStorage hss = new HospitalSurveyStorage();
            hss.Save(a);
            this.NavigationService.Navigate(new Surveys(patient));
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Surveys(patient));
        }
    }
}
