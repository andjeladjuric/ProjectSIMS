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

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for DoctorSurvey.xaml
    /// </summary>
    public partial class DoctorSurvey : Page
    {
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
        public DoctorSurvey(Patient p,Doctor d)
        {
            InitializeComponent();
            patient = p;
            doctor = d;
            String s = doctor.Name + " " + d.Surname;
            lbDoctor.Content = s;
            this.DataContext = this;
            

        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            String communicationSurvey;
            if (NKomunikacija.IsChecked == true)
            {
                communicationSurvey = NKomunikacija.Content.ToString();
            }
            else if (DZKomunikacija.IsChecked == true)
            {
                communicationSurvey = DZKomunikacija.Content.ToString();
            }
            else
            {
                communicationSurvey = ZKomunikacija.Content.ToString();
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

            String professionalismSurvey;
            if (NStrucnost.IsChecked == true)
            {
                professionalismSurvey = NStrucnost.Content.ToString();
            }
            else if (DZStrucnost.IsChecked == true)
            {
                professionalismSurvey = DZStrucnost.Content.ToString();
            }
            else
            {
                professionalismSurvey = ZStrucnost.Content.ToString();
            }

            String doctorCareSurvey;
            if (NBriga.IsChecked == true)
            {
                doctorCareSurvey = NBriga.Content.ToString();
            }
            else if (DZBriga.IsChecked == true)
            {
                doctorCareSurvey = DZBriga.Content.ToString();
            }
            else
            {
                doctorCareSurvey = ZBriga.Content.ToString();
            }

            String infoSurvey;
            if (NInformisanost.IsChecked == true)
            {
                infoSurvey = NInformisanost.Content.ToString();
            }
            else if (DZInformisanost.IsChecked == true)
            {
                infoSurvey = DZInformisanost.Content.ToString();
            }
            else
            {
                infoSurvey = ZInformisanost.Content.ToString();
            }

            String timeSurvey;
            if (NVrijeme.IsChecked == true)
            {
                timeSurvey = NVrijeme.Content.ToString();
            }
            else if (DZVrijeme.IsChecked == true)
            {
                timeSurvey = DZVrijeme.Content.ToString();
            }
            else
            {
                timeSurvey = ZVrijeme.Content.ToString();
            }

            
            
            
            SurveyDoctorPatient a = new SurveyDoctorPatient {Communication=communicationSurvey, Courtesy=courtesySurvey, Professionalism=professionalismSurvey, CareForPatient=doctorCareSurvey, ProvidingInformation=infoSurvey, DevotedTime=timeSurvey, doctor= doctor, patient=patient, ExecutionTime=DateTime.Now };
            DoctorSurveyStorage dss = new DoctorSurveyStorage();
            dss.Save(a);
            this.NavigationService.Navigate(new Surveys(patient));


        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Surveys(patient));
            
        }
    }
}
