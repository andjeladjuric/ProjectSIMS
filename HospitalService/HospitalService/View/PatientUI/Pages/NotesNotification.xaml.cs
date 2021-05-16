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
        public Diagnosis Diagnosis { get; set; }
        public Note Note { get; set; }
        public Button Reminder { get; set; }
        public DiagnosisForPatient diagnosisForPatient { get; set; }
        public NotesNotification(Diagnosis diagnosis, Note note, Button reminder,DiagnosisForPatient dfp)
        {
            InitializeComponent();
            Diagnosis = diagnosis;
            Note = note;
            Reminder = reminder;
            diagnosisForPatient = dfp;
        }

        private void confirmClick(object sender, RoutedEventArgs e)
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

            Timer everyFewHours = new Timer() { Interval = howOftenInMilliseconds };
            String end = tbEnd.Text;
            String dateEnd = dpEnd.Text;
            DateTime endTime = Convert.ToDateTime(dateEnd + " " + end + ":00");

            everyFewHours.Elapsed += (sender, e) =>
            {

                if (DateTime.Compare(DateTime.Now, endTime) < 0)
                {

                    MessageBox.Show(Note.noteForPatient);
                }

            };

            String start = tbStart.Text;
            String dateStart = dpStart.Text;
            DateTime startTime = Convert.ToDateTime(dateStart + " " + start + ":00");
            var span = startTime - DateTime.Now;

            var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            timer.Elapsed += (sender, e) => { 
                everyFewHours.Start();
                MessageBox.Show(Note.noteForPatient);

            };
            timer.Start();
           
            this.NavigationService.Navigate(new DiagnosisForPatient(Diagnosis,Note));
            

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
            this.NavigationService.Navigate(new DiagnosisForPatient(Diagnosis, Note));
        }
    }
}
