using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class NewsDetailsViewModel:ViewModelPatientClass
    {
        public String TitleNews { get; set; }
        public String PublishingDateNews { get; set; }
        public String NewsContent { get; set; }
        public NewsDetailsViewModel(News news) {
            TitleNews = news.Title;
            PublishingDateNews = news.PublishingDate.ToShortDateString();
            NewsContent = news.Content;
        
        }
    }
}
