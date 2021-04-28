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
            

        }
    }
}
