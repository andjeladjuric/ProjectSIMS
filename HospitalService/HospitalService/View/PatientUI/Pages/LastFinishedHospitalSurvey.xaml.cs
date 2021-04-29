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
        public LastFinishedHospitalSurvey(SurveyHospitalPatient shp)
        {
            InitializeComponent();
            this.DataContext = this;
            lbDate.Content = shp.ExecutionTime.ToShortDateString();
            lbProf.Content = shp.StaffExpertise;
            lbCourtesy.Content = shp.StaffCourtesy;
            lbTime.Content = shp.WaitingForReception;
            lbHygiene.Content = shp.RoomHygiene;
            lbService.Content = shp.QualityOfService;
            lbPrise.Content = shp.ServicePrices;

        }
    }
}
