using HospitalService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class HospitalSurveyRepository: RepositoryImplementationJson <SurveyHospitalPatient>
    {

        private String FileLocation = @"..\..\..\Data\hospitalSurveys.json";
        public List<SurveyHospitalPatient> hospitalSurveys { get; set; }

        public HospitalSurveyRepository()
        {
            hospitalSurveys = new List<SurveyHospitalPatient>();
            hospitalSurveys = getAll(FileLocation);
        }

        public List<SurveyHospitalPatient> GetAll()
        {
            return getAll(FileLocation);
        }

        public void Save(SurveyHospitalPatient newSurvey)
        {
            hospitalSurveys.Add(newSurvey);
            saveAll(hospitalSurveys, FileLocation);
        }
    }
}
