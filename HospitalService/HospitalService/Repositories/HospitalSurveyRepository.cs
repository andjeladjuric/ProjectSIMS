using HospitalService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class HospitalSurveyRepository
    {

        private String FileLocation = @"..\..\..\Data\hospitalSurveys.json";
        public List<SurveyHospitalPatient> hospitalSurveys { get; set; }

        public HospitalSurveyRepository()
        {

            hospitalSurveys = new List<SurveyHospitalPatient>();
            hospitalSurveys = JsonConvert.DeserializeObject<List<SurveyHospitalPatient>>(File.ReadAllText(FileLocation));

        }

        public List<SurveyHospitalPatient> GetAll()
        {
            return hospitalSurveys;
        }

        public void Save(SurveyHospitalPatient newSurvey)
        {
            hospitalSurveys.Add(newSurvey);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(hospitalSurveys,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
        }
    }
}
