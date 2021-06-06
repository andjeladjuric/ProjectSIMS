using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.DoctorUI.ViewModel
{
    class TutorialViewModel
    {
        public TutorialView Window { get; set; }
        public MediaElement mePlayer { get; set; }
        public RelayCommand PlayCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand StopCommand { get; set; }

        public TutorialViewModel(MediaElement video, TutorialView view) {
            this.Window = view;
            this.mePlayer = video;
            PlayCommand = new RelayCommand(Executed_PlayCommand,
             CanExecute_PlayCommand);
            PauseCommand = new RelayCommand(Executed_PauseCommand,
             CanExecute_PlayCommand);
            CloseCommand = new RelayCommand(Executed_CloseCommand,
             CanExecute_PlayCommand);
            StopCommand = new RelayCommand(Executed_StopCommand,
            CanExecute_PlayCommand);
        }

        public bool CanExecute_PlayCommand(object obj)
        {
            return true;
        }

        public void Executed_PlayCommand(object obj)
        {
            mePlayer.Play();

        }

        public void Executed_StopCommand(object obj)
        {
            mePlayer.Stop();

        }

        public void Executed_PauseCommand(object obj)
        {
            mePlayer.Pause();
        }

        public void Executed_CloseCommand(object obj)
        {
            this.Window.Close();
        }
    }
}
