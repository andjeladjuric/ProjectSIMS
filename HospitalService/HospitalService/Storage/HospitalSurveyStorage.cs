using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HospitalService.Model;
using Newtonsoft.Json;

namespace HospitalService.Storage
{
  public  class HospitalSurveyStorage
    {

        private String FileLocation = @"..\..\..\Data\hospitalSurveys.json";
        public List<SurveyHospitalPatient> hospitalSurveys { get; set; }

        public HospitalSurveyStorage() {

            hospitalSurveys = new List<SurveyHospitalPatient>();
            hospitalSurveys = JsonConvert.DeserializeObject<List<SurveyHospitalPatient>>(File.ReadAllText(FileLocation));


        }

        public void SerializeHospitalSurveys()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(hospitalSurveys));
        }

        public List<SurveyHospitalPatient> GetAll()
        {
            return hospitalSurveys;
        }

        public void Save(SurveyHospitalPatient newSurvey)
        {
            hospitalSurveys.Add(newSurvey);
            SerializeHospitalSurveys();
        }

    }
}
