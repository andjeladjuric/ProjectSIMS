using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HospitalService.Model;
using HospitalService.Repositories;
using Model;

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

        public SurveyHospitalPatient getLastFinishedSurvey(Patient surveyor) {

            List<SurveyHospitalPatient> hospitalSurveys = repository.GetAll();
            List<SurveyHospitalPatient> patientSurveys = hospitalSurveys.Where(survey => survey.patient.Jmbg.Equals(surveyor.Jmbg)).ToList();
            patientSurveys.Sort((s1, s2) => DateTime.Compare(s1.ExecutionTime, s2.ExecutionTime));
            SurveyHospitalPatient lastFinishedHospitalSurvey = patientSurveys[patientSurveys.Count - 1];
            return lastFinishedHospitalSurvey;

        }

    }
}
