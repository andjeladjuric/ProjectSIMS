using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HospitalService.Model;
using Newtonsoft.Json;

namespace HospitalService.Storage
{
 public  class DoctorSurveyStorage
    {

        private String FileLocation = @"..\..\..\Data\doctorSurveys.json";
        public List<SurveyDoctorPatient> doctorSurveys { get; set; }


        public DoctorSurveyStorage() {

            doctorSurveys = new List<SurveyDoctorPatient>();
            doctorSurveys = JsonConvert.DeserializeObject<List<SurveyDoctorPatient>>(File.ReadAllText(FileLocation));

        }


        public List<SurveyDoctorPatient> GetAll()
        {
            return doctorSurveys;
        }

        public void Save(SurveyDoctorPatient newSurvey)
        {
            doctorSurveys.Add(newSurvey);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(doctorSurveys,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
        }
    }
}
