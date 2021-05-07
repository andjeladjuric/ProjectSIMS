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

        public bool CheckExistingRenovations(string roomId, DateTime start, DateTime end)
        {
            renovations = JsonConvert.DeserializeObject<List<Renovation>>(File.ReadAllText(FileLocation));
            bool returnValue = true;

            if (renovations != null && renovations.Count != 0)
            {
                foreach(Renovation r in renovations)
                {
                    if (r.RoomId.Equals(roomId))
                    {
                        TimeSpan t = end - start;
                        if (DateTime.Compare(start, r.Start) <= 0 && DateTime.Compare(end, r.End) >= 0)
                        {
                            returnValue = false;
                            break;
                        }
                        else if (DateTime.Compare(start, r.Start) <= 0 && DateTime.Compare(end, r.End) <= 0 &&
                            DateTime.Compare(end, r.Start) >= 0)
                        {
                            returnValue = false;
                            break;
                        }
                        else if (DateTime.Compare(start, r.Start) >= 0 && DateTime.Compare(end, r.End) <= 0)
                        {
                            returnValue = false;
                            break;
                        }
                        else if (DateTime.Compare(start, r.Start) >= 0 && DateTime.Compare(end, r.End) >= 0 &&
                            DateTime.Compare(start, r.End) <= 0)
                        {
                            returnValue = false;
                            break;
                        }
                    }
                }
            }

            if (!returnValue)
                MessageBox.Show("Vec postoji zakazano renoviranje u datom periodu!");

            return returnValue;
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
                        if (DateTime.Compare(renovation.End, DateTime.Now) >= 0)
                        {
                            ChangeRoomAvailability(renovation, false);
                        }
                        else
                        {
                            ChangeRoomAvailability(renovation, true);
                        }
                    }
                }

                DeleteRequestIfFinished(renovations);
            }
        }

        private void DeleteRequestIfFinished(List<Renovation> renovations)
        {
            Renovation renovation;
            for (int i = 0; i < renovations.Count; i++)
            {
                renovation = renovations[i];
                if (DateTime.Compare(renovation.End, DateTime.Now) < 0)
                {
                    renovations.RemoveAt(i);
                    SerializeRenovations();
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

        public bool IsRoomReservationPossible(DateTime appointmentStart, DateTime appointmentEnd)
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
