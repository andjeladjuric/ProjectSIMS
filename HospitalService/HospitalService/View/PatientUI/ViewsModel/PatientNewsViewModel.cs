using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;
using HospitalService.Service;
using HospitalService.View.PatientUI.Pages;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class PatientNewsViewModel:ViewModelPatientClass
    {

        private News selectedPatientNews;
        private ObservableCollection<News> patientNews;
        public ObservableCollection<News> PatientNews
        {
            get { return patientNews; }
            set
            {
                patientNews = value;
                OnPropertyChanged();
            }
        }


        public News SelectedPatientNews
        {
            get { return selectedPatientNews; }
            set
            {
                selectedPatientNews = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand showNews { get; set; }
        
        private NavigationService navigationService;
        private NewsService newsService;
        private void Execute_ShowNews(object obj) {

            navigationService.Navigate(new NewsDetails(SelectedPatientNews));
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        public PatientNewsViewModel(NavigationService navigationService) {
           
            this.navigationService = navigationService;
            newsService = new NewsService();
            List<News> newsForPatient = newsService.GetForRole(Role.pacijenti);
            PatientNews = new ObservableCollection<News>();
            newsForPatient.ForEach(PatientNews.Add);
            showNews = new RelayCommand(Execute_ShowNews,CanExecute_Command);
        }
    }
}
