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
    /// Interaction logic for ReferralAppointmentDetails.xaml
    /// </summary>
    public partial class ReferralAppointmentDetails : Page
    {
        private ReferralAppointmentDetailsViewModel viewModel;
        public ReferralAppointmentDetails(Patient patient,Referral referral, ReferralAppointment referralAppointment)
        {
            InitializeComponent();
            viewModel = new ReferralAppointmentDetailsViewModel(patient,referral,referralAppointment);
            this.DataContext = viewModel;
        }
    }
}
