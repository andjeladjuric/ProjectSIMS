using HospitalService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class DoctorsSurveyRepository
    {
        private String FileLocation = @"..\..\..\Data\doctorSurveys.json";
        public List<SurveyDoctorPatient> doctorSurveys { get; set; }


        public DoctorsSurveyRepository()
        {

            doctorSurveys = new List<SurveyDoctorPatient>();
            doctorSurveys = JsonConvert.DeserializeObject<List<SurveyDoctorPatient>>(File.ReadAllText(FileLocation));

        }

        public void SerializeDoctorsSurveys()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(doctorSurveys));
        }


        public List<SurveyDoctorPatient> GetAll()
        {
            return doctorSurveys;
        }

        public void Save(SurveyDoctorPatient newSurvey)
        {
            doctorSurveys.Add(newSurvey);
            SerializeDoctorsSurveys();
        }
    }
}

