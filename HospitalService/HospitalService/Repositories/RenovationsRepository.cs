using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class RenovationsRepository
    {
        private string FileLocation = @"..\..\..\Data\renovation.json";
        private List<Renovation> renovations;

        public RenovationsRepository()
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
