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
using HospitalService.View.PatientUI.ViewsModel;
using Model;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for ReferralAppointment.xaml
    /// </summary>
    public partial class ReferralAppointment : Page
    {
        private ReferralAppointmentViewModel viewModel;
        public ReferralAppointment(Patient patient)
        {
            InitializeComponent();
            viewModel = new ReferralAppointmentViewModel(patient,ReferralPage.NavigationService, this);
            this.DataContext = viewModel;
        }
    }
}
