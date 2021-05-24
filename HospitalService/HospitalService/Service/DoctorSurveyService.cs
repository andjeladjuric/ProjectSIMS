using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HospitalService.Model;
using HospitalService.Repositories;
using Model;

namespace HospitalService.Service
{
    
    public  class DoctorSurveyService
    {
        private DoctorsSurveyRepository repository;

        public DoctorSurveyService() {

            repository = new DoctorsSurveyRepository();
        }

        public SurveyDoctorPatient getLastFinishedDoctorSurvey(Doctor doctor, Patient patient) {

            List<SurveyDoctorPatient> surveys = repository.GetAll();
            List<SurveyDoctorPatient> finishedSurveys = surveys.Where(survey => survey.doctor.Jmbg.Equals(doctor.Jmbg) && survey.patient.Jmbg.Equals(patient.Jmbg)).ToList();
            finishedSurveys.Sort((s1, s2) => DateTime.Compare(s1.ExecutionTime, s2.ExecutionTime));

            SurveyDoctorPatient lastFinishedSurvey = finishedSurveys[finishedSurveys.Count - 1];
            return lastFinishedSurvey;
        }


        public void saveDoctorSurvey(SurveyDoctorPatient newSurvey) {

            repository.Save(newSurvey);
        
        }

    }
}
