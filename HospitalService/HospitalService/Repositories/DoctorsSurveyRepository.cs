using HospitalService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class DoctorsSurveyRepository : RepositoryImplementationJson <SurveyDoctorPatient>
    {
        private String FileLocation = @"..\..\..\Data\doctorSurveys.json";
        public List<SurveyDoctorPatient> doctorSurveys { get; set; }


        public DoctorsSurveyRepository()
        {
            doctorSurveys = new List<SurveyDoctorPatient>();
            doctorSurveys = getAll(FileLocation);
        }
        public List<SurveyDoctorPatient> GetAll()
        {
            return getAll(FileLocation);
        }

        public void Save(SurveyDoctorPatient newSurvey)
        {
            doctorSurveys.Add(newSurvey);
            saveAll(doctorSurveys,FileLocation);
        }
    }
}

