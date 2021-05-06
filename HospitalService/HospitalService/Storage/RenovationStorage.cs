using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

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

        public void CheckRenovationRequests()
        {
            renovations = JsonConvert.DeserializeObject<List<Renovation>>(File.ReadAllText(FileLocation));

            if (renovations != null && renovations.Count != 0)
            {
                foreach (Renovation renovation in renovations)
                {
                    if (DateTime.Compare(renovation.Start, DateTime.Now) <= 0)
                    {
                        if (DateTime.Compare(renovation.End, DateTime.Now) > 0)
                        {
                            ChangeRoomAvailability(renovation, false);
                        }
                        else
                        {
                            ChangeRoomAvailability(renovation, true);
                        }
                    }
                }
            }
        }

        private void ChangeRoomAvailability(Renovation renovation, bool IsAvailable)
        {
            RoomFileStorage roomFileStorage = new RoomFileStorage();
            foreach (Room room in roomFileStorage.GetAll())
            {
                if (room.Id.Equals(renovation.RoomId))
                {
                    room.IsFree = IsAvailable;
                    roomFileStorage.SerializeRooms();
                }
            }
        }

        public bool CheckIfPossible(DateTime appointmentStart, DateTime appointmentEnd)
        {
            renovations = JsonConvert.DeserializeObject<List<Renovation>>(File.ReadAllText(FileLocation));

            if (renovations != null && renovations.Count != 0)
            {
                foreach (Renovation renovation in renovations)
                {
                    if(DateTime.Compare(renovation.Start, appointmentStart) <= 0 && DateTime.Compare(renovation.End, appointmentStart) >= 0
                        && DateTime.Compare(renovation.Start, appointmentEnd) <= 0 && DateTime.Compare(renovation.End, appointmentEnd) >= 0)
                        return false;
                    else
                        return true;
                }
            }

            return true;
        }
    }
}
