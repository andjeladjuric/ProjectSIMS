using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
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
    /// Interaction logic for NotesNotification.xaml
    /// </summary>
    public partial class NotesNotification : Page
    {
        public Diagnosis chosenDiagnosis { get; set; }
        public Note noteOfChosenDiagnosis { get; set; }
        
        public NotesNotification(Diagnosis diagnosis, Note note)
        {
            InitializeComponent();
            chosenDiagnosis = diagnosis;
            noteOfChosenDiagnosis = note;
            
            
        }

        private void confirmSettingReminder(object sender, RoutedEventArgs e)
        {
            /*  //int howOftenInMilliseconds = howOften() * 3600 * 1000;
              double howOftenInMilliseconds = 0.0166666667 * 3600 * 1000;

              Timer everyFewHours= new Timer() { Interval = howOftenInMilliseconds };

              String end = tbEnd.Text;
              String dateEnd = dpEnd.Text;
              DateTime endTime = Convert.ToDateTime(dateEnd + " " + end + ":00");
              everyFewHours.Elapsed += (sender, e) =>
              {

                  if (DateTime.Compare(DateTime.Now, endTime) < 0) {

                      MessageBox.Show(Note.noteForPatient);
                  }

              };
              String start = tbStart.Text;
              String dateStart = dpStart.Text;
              DateTime startTime = Convert.ToDateTime(dateStart + " " + start + ":00");
              var span = startTime - DateTime.Now;

              var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
              timer.Elapsed += (sender, e) => { everyFewHours.Start(); };
              timer.Start();

              */
            double howOftenInMilliseconds = 15000;
            DateTime startTime = Convert.ToDateTime(dpStartDate.Text + " " + tbStartTime.Text + ":00");
            DateTime endTime = Convert.ToDateTime(dpEndDate.Text + " " + tbEndTime.Text + ":00");
            var timeToBeginning = startTime - DateTime.Now;
            Timer everyFewHours = new Timer() { Interval = howOftenInMilliseconds };
            everyFewHours.Elapsed += (sender, e) =>
            {

                if (DateTime.Compare(DateTime.Now, endTime) < 0)
                {

                    MessageBox.Show(noteOfChosenDiagnosis.noteForPatient);
                }

            };

            var startTimer = new Timer { Interval = timeToBeginning.TotalMilliseconds, AutoReset = false };
            startTimer.Elapsed += (sender, e) => { 
                everyFewHours.Start();
                MessageBox.Show(noteOfChosenDiagnosis.noteForPatient);

            };
            startTimer.Start();
           
            this.NavigationService.Navigate(new DiagnosisForPatient(chosenDiagnosis,noteOfChosenDiagnosis));
            

        }
        private int howOften() {
            int howOftenInHours = 0;
            switch(cbHowOften.SelectedItem.ToString()){
                case "1 sat":
                    howOftenInHours = 1;
                    break;
                case "2 sata":
                    howOftenInHours = 2;
                    break;
                case "3 sata":
                    howOftenInHours = 3;
                    break;
                case "4 sata":
                    howOftenInHours = 3;
                    break;
                default:
                    howOftenInHours = 1;
                    break;
            }
            return howOftenInHours;
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DiagnosisForPatient(chosenDiagnosis, noteOfChosenDiagnosis));
        }
    }
}
