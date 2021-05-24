using HospitalService.View.DoctorUI.Commands;
using HospitalService.View.DoctorUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class NewsViewModel : ViewModelClass
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public NewsView Window { get; set; }
        public RelayCommand OkCommand { get; set; }
        public RelayCommand KeyUpCommandWithKey { get; set; }


        public NewsViewModel(News news, NewsView window)
        {
            this.Title = news.Title;
            this.Date = news.PublishingDate;
            this.Content = news.Content;
            this.Window = window;
            KeyUpCommandWithKey = new RelayCommand(Executed_KeyDownCommandWithKey);
            OkCommand = new RelayCommand(Executed_OkCommand,
              CanExecute_OkCommand);
        }
        public bool CanExecute_OkCommand(object obj)
        {
            return true;
        }
        public void Executed_OkCommand(object obj)
        {
            this.Window.Close();
        }
        private void Executed_KeyDownCommandWithKey(object obj)
        {
        }
    }
}
