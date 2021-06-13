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
        RenovationState State;
        RoomService roomService = new RoomService();
        public RoomRenovationService()
        {
            renovations = new RenovationsRepository();
        }

        public void SetRenovationState(RenovationState state, Renovation renovation)
        {
            this.State = state;
            this.State.SetContext(renovation);
            this.State.ChangeRoomAvailability();
            this.State.Renovate();
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
                            roomService.ChangeRoomAvailability(renovation.RoomId, false);

                            if(renovation.Type.Equals(RenovationType.Merge))
                                roomService.ChangeRoomAvailability(renovation.SecondRoomId, false);
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
                SetRenovationState(new MergeState(), renovation);
            else if (renovation.Type.Equals(RenovationType.Split))
                SetRenovationState(new SplitState(), renovation);
            else
                SetRenovationState(new OtherRenovationState(), renovation);
        }

        public void Save(Renovation reno) => renovations.Save(reno);
        public List<Renovation> GetAll() => renovations.GetAll();
        public void DeleteRequestIfFinished(List<Renovation> reno) => renovations.DeleteRequestIfFinished(reno);
        public void Delete(string roomId) => renovations.Delete(roomId);
        public void SerializeRenovations() => renovations.SerializeRenovations();
    }
}
