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

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for LastFinishedHospitalSurvey.xaml
    /// </summary>
    public partial class LastFinishedHospitalSurvey : Page
    {
        public LastFinishedHospitalSurvey(SurveyHospitalPatient lastFinishedHospitalSurvey)
        {
            InitializeComponent();
            this.DataContext = this;
            lbExecutionDate.Content = lastFinishedHospitalSurvey.ExecutionTime.ToShortDateString();
            lbStaffExpertise.Content = lastFinishedHospitalSurvey.StaffExpertise;
            lbStaffCourtesy.Content = lastFinishedHospitalSurvey.StaffCourtesy;
            lbWaitingForReception.Content = lastFinishedHospitalSurvey.WaitingForReception;
            lbHygiene.Content = lastFinishedHospitalSurvey.RoomHygiene;
            lbQualityOfService.Content = lastFinishedHospitalSurvey.QualityOfService;
            lbServicePrices.Content = lastFinishedHospitalSurvey.ServicePrices;

        }
    }
}
