using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.Service
{
    public class RoomRenovationService
    {
        RenovationsRepository renovations;

        public RoomRenovationService()
        {
            renovations = new RenovationsRepository();
        }

        public bool CheckExistingRenovations(string roomId, DateTime startReno, DateTime endReno)
        {
            List<Renovation> renovationRequests = renovations.GetAll();
            bool returnValue = true;

            if (renovations != null && renovationRequests.Count != 0)
            {
                foreach (Renovation renovation in renovationRequests)
                {
                    if (renovation.RoomId.Equals(roomId))
                    {
                        if (DateTime.Compare(startReno, renovation.Start) <= 0 && DateTime.Compare(endReno, renovation.End) >= 0)
                        {
                            returnValue = false;
                            break;
                        }
                        else if (DateTime.Compare(startReno, renovation.Start) <= 0 && DateTime.Compare(endReno, renovation.End) <= 0 &&
                            DateTime.Compare(endReno, renovation.Start) >= 0)
                        {
                            returnValue = false;
                            break;
                        }
                        else if (DateTime.Compare(startReno, renovation.Start) >= 0 && DateTime.Compare(endReno, renovation.End) <= 0)
                        {
                            returnValue = false;
                            break;
                        }
                        else if (DateTime.Compare(startReno, renovation.Start) >= 0 && DateTime.Compare(endReno, renovation.End) >= 0 &&
                            DateTime.Compare(startReno, renovation.End) <= 0)
                        {
                            returnValue = false;
                            break;
                        }
                    }
                }
            }

            if (!returnValue)
                MessageBox.Show("Već postoji zakazano renoviranje u datom periodu!");

            return returnValue;
        }

        public void CheckRenovationRequests()
        {
            List<Renovation> renovationRequests = renovations.GetAll();

            if (renovations != null && renovationRequests.Count != 0)
            {
                foreach (Renovation renovation in renovationRequests)
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

                DeleteRequestIfFinished(renovationRequests);
            }
        }

        private void DeleteRequestIfFinished(List<Renovation> renovationRequests)
        {
            Renovation renovation;
            for (int i = 0; i < renovationRequests.Count; i++)
            {
                renovation = renovationRequests[i];
                if (DateTime.Compare(renovation.End, DateTime.Now) < 0)
                {
                    renovationRequests.RemoveAt(i);
                    renovations.SerializeRenovations();
                }
            }
        }

        private void ChangeRoomAvailability(Renovation renovation, bool IsAvailable)
        {
            RoomService roomService = new RoomService();
            foreach (Room room in roomService.GetAll())
            {
                if (room.Id.Equals(renovation.RoomId))
                {
                    room.IsFree = IsAvailable;
                    roomService.UpdateRoom(room);
                    break;
                }
            }
        }

        public bool IsRoomReservationPossible(DateTime appointmentStart, DateTime appointmentEnd)
        {
            List<Renovation> renovationRequests = renovations.GetAll();

            if (renovations != null && renovationRequests.Count != 0)
            {
                foreach (Renovation renovation in renovationRequests)
                {
                    if (DateTime.Compare(renovation.Start, appointmentStart) <= 0 && DateTime.Compare(renovation.End, appointmentStart) >= 0
                        && DateTime.Compare(renovation.Start, appointmentEnd) <= 0 && DateTime.Compare(renovation.End, appointmentEnd) >= 0)
                        return false;
                    else
                        return true;
                }
            }

            return true;
        }

        public void Save(Renovation reno) => renovations.Save(reno);
        public List<Renovation> GetAll() => renovations.GetAll();
        public void SerializeRenovations() => renovations.SerializeRenovations();
    }
}
