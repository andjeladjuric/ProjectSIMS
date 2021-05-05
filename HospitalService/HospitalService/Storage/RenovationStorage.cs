using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Storage
{
    public class RenovationStorage
    {
        private string FileLocation = @"..\..\..\Data\renovation.json";
        private List<Renovation> renovations;

        public RenovationStorage()
        {
            renovations = new List<Renovation>();
            renovations = JsonConvert.DeserializeObject<List<Renovation>>(File.ReadAllText(FileLocation));
        }

        public void SerializeRenovations()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(renovations));
        }

        public List<Renovation> GetAll()
        {
            return renovations;
        }

        public void Save(Renovation newReno)
        {
            renovations.Add(newReno);
            SerializeRenovations();
        }
    }
}
