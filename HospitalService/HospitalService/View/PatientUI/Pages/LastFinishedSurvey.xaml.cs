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
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for LastFinishedSurvey.xaml
    /// </summary>
    public partial class LastFinishedSurvey : Page
    {
        public LastFinishedSurvey(Doctor d, SurveyDoctorPatient sdp)
        {

            InitializeComponent();
            String s = d.Name + " " + d.Surname;
            lbDoctor.Content = s;
            lbDate.Content = sdp.ExecutionTime.ToShortDateString();
            lbKomunik.Content = sdp.Communication;
            lbLjubaznost.Content = sdp.Courtesy;
            lbStrucnost.Content = sdp.Professionalism;
            lbBriga.Content = sdp.CareForPatient;
            lbInfo.Content = sdp.ProvidingInformation;
            lbVrijeme.Content = sdp.DevotedTime;

        }
    }
}
