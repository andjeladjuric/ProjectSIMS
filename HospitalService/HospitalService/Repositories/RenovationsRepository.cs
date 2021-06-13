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

        public void Delete(string roomId)
        {
            Renovation renovationRequest;
            for (int i = 0; i < renovations.Count; i++)
            {
                renovationRequest = renovations[i];
                if (roomId.Equals(renovationRequest.RoomId) || roomId.Equals(renovationRequest.SecondRoomId))
                {
                    renovations.RemoveAt(i);
                    SerializeRenovations();
                    continue;
                }
            }
        }

        public void DeleteRequestIfFinished(List<Renovation> renovationRequests)
        {
            Renovation renovation;
            for (int i = 0; i < renovationRequests.Count; i++)
            {
                renovation = renovationRequests[i];
                if (DateTime.Compare(renovation.End, DateTime.Now) < 0)
                {
                    renovationRequests.RemoveAt(i);
                    SerializeRenovations();
                }
            }
        }
    }
}
