using HospitalService.Repositories;
using Model;
using Storage;
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
                            SetState(renovation);
                        }
                    }
                }

                DeleteRequestIfFinished(renovationRequests);
            }
        }

        private void SetState(Renovation renovation)
        {
            if (renovation.Type.Equals(RenovationType.Merge))
                renovation.SetRenovationState(new MergeState());
            else if (renovation.Type.Equals(RenovationType.Split))
                renovation.SetRenovationState(new SplitState());
            else
                renovation.SetRenovationState(new OtherRenovationState());
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
            Room room = roomService.GetOne(renovation.RoomId);
            room.IsFree = IsAvailable;
            roomService.UpdateRoom(room);
        }

        public void Save(Renovation reno) => renovations.Save(reno);
        public List<Renovation> GetAll() => renovations.GetAll();
        public void SerializeRenovations() => renovations.SerializeRenovations();
    }
}
