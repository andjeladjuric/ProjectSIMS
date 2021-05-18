using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.Repositories;

namespace HospitalService.Service
{
  public  class HospitalSurveyService
    {
        private HospitalSurveyRepository repository;

        public HospitalSurveyService() {

            repository = new HospitalSurveyRepository();

        }

        public void saveHospitalSurvey(SurveyHospitalPatient newSurvey) {

            repository.Save(newSurvey);
        
        }

    }
}
