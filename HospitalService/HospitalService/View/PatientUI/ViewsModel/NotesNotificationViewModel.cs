using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using HospitalService.Model;
using HospitalService.View.PatientUI.Pages;

namespace HospitalService.View.PatientUI.ViewsModel
{
    public class NotesNotificationViewModel:ViewModelPatientClass
    {

        public RelayCommand confirmSetReminder { get; set; }
        public RelayCommand cancelSetReminder { get; set; }

        private DateTime startDateReminder { get; set; }
        public DateTime StartDateReminder
        {
            get { return startDateReminder; }
            set
            {
                startDateReminder = value;
                OnPropertyChanged();
            }
        }

        private DateTime endDateReminder { get; set; }
        public DateTime EndDateReminder
        {
            get { return endDateReminder; }
            set
            {
                endDateReminder = value;
                OnPropertyChanged();
            }
        }

        public String StartTimeReminder { get; set; }
        public String EndTimeReminder { get; set; }
        public String HowOftenReminder { get; set; }
        private Diagnosis diagnosis;
        private Note note;
        private NotesNotification notesNotification;

        private void Execute_ConfirmSetReminder(object obj) {

            double howOftenInMilliseconds = 15000;
            DateTime startTime = Convert.ToDateTime(StartDateReminder.ToShortDateString() + " " + StartTimeReminder + ":00");
            DateTime endTime = Convert.ToDateTime(EndDateReminder.ToShortDateString() + " " + EndTimeReminder + ":00");
            var timeToBeginning = startTime - DateTime.Now;
            Timer everyFewHours = new Timer() { Interval = howOftenInMilliseconds };
            everyFewHours.Elapsed += (sender, e) =>
            {

                if (DateTime.Compare(DateTime.Now, endTime) < 0)
                {

                    MessageBox.Show(note.noteForPatient);
                }

            };

            var startTimer = new Timer { Interval = timeToBeginning.TotalMilliseconds, AutoReset = false };
            startTimer.Elapsed += (sender, e) => {
                everyFewHours.Start();
                MessageBox.Show(note.noteForPatient);

            };
            startTimer.Start();

            notesNotification.NavigationService.Navigate(new DiagnosisForPatient(diagnosis, note));

        }

        private int howOften()
        {
            int howOftenInHours = 0;
            switch (HowOftenReminder)
            {
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
        private void Execute_CancelSetReminder(object obj) {

            notesNotification.NavigationService.Navigate(new DiagnosisForPatient(diagnosis, note));

        }
        private bool CanExecute_Command(object obj) {
            return true;
        }
        public NotesNotificationViewModel(Diagnosis diagnosis, Note note, NotesNotification notesNotification) {

            this.diagnosis = diagnosis;
            this.note = note;
            this.notesNotification = notesNotification;
            StartDateReminder = DateTime.Now;
            EndDateReminder = DateTime.Now;
            confirmSetReminder = new RelayCommand(Execute_ConfirmSetReminder,CanExecute_Command);
            cancelSetReminder = new RelayCommand(Execute_CancelSetReminder, CanExecute_Command);
        }
    }
}
