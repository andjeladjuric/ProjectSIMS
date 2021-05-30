using System;
using System.Collections.Generic;
using System.Linq;
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
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using Storage;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for Surveys.xaml
    /// </summary>
    public partial class Surveys : Page
    {
        
        private SurveysViewModel viewModel;
        public Surveys(Patient patient)
        {
            InitializeComponent();
            viewModel = new SurveysViewModel(patient,SurveyFrame.NavigationService,this);
            this.DataContext = viewModel;
           
        }

        

    }
}
