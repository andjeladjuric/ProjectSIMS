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
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for MoveAppointment.xaml
    /// </summary>
    public partial class MoveAppointment : Page
    {

        
        private MoveAppointmentViewModel viewModel;
        public MoveAppointment(Appointment appointment, Patient patient)
        {
            InitializeComponent();
            viewModel = new MoveAppointmentViewModel(patient,appointment,this);
            this.DataContext = viewModel;
            

        }

       
    }
}
