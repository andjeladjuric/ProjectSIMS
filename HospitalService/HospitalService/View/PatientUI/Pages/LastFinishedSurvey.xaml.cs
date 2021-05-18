﻿using System;
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
        public LastFinishedSurvey(Doctor surveyedDoctor, SurveyDoctorPatient lastFinishedDoctorSurvey)
        {

            InitializeComponent();
            lbDoctor.Content = surveyedDoctor.Name + " " + surveyedDoctor.Surname;
            lbDate.Content = lastFinishedDoctorSurvey.ExecutionTime.ToShortDateString();
            lbCommunication.Content = lastFinishedDoctorSurvey.Communication;
            lbCourtesy.Content = lastFinishedDoctorSurvey.Courtesy;
            lbProfessionalism.Content = lastFinishedDoctorSurvey.Professionalism;
            lbCareForPatient.Content = lastFinishedDoctorSurvey.CareForPatient;
            lbProvidingInformation.Content = lastFinishedDoctorSurvey.ProvidingInformation;
            lbDevotedTime.Content = lastFinishedDoctorSurvey.DevotedTime;

        }
    }
}
