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
                            ChangeRoomAvailability(renovation, true);
                            SplitOrMergeRooms(renovation);
                        }
                    }
                }

                DeleteRequestIfFinished(renovationRequests);
            }
        }

        private void SplitOrMergeRooms(Renovation renovation)
        {
            if (renovation.Type.Equals(RenovationType.Merge))
                MergeRooms(renovation);
            else
                SplitRoom(renovation);
        }

        private void MergeRooms(Renovation renovation)
        {
            RoomService roomService = new RoomService();
            Room firstRoom = roomService.GetOne(renovation.RoomId);
            Room secondRoom = roomService.GetOne(renovation.SecondRoomId);
            double newSize = firstRoom.Size + secondRoom.Size;

            if (CheckAppointmentsForDate(renovation.Start, renovation.End, firstRoom.Id) &&
                        CheckAppointmentsForDate(renovation.Start, renovation.End, secondRoom.Id))
            {
                roomService.AddRoom(new Room(renovation.NewRoomType, renovation.RoomId, renovation.NewRoomName, newSize, firstRoom.Floor, true));
                roomService.DeleteRoom(firstRoom.Id);
                roomService.DeleteRoom(secondRoom.Id);
                roomService.SerializeRooms();
            }
        }

        private void SplitRoom(Renovation renovation)
        {
            RoomService roomService = new RoomService();
            Room firstRoom = roomService.GetOne(renovation.RoomId);
            double firstSize = firstRoom.Size - renovation.NewSize;

            if (CheckAppointmentsForDate(renovation.Start, renovation.End, renovation.RoomId))
            {
                roomService.AddRoom(new Room(renovation.NewRoomType, renovation.RoomId, firstRoom.Name, firstSize ,firstRoom.Floor, true));
                roomService.AddRoom(new Room(renovation.NewRoomType, renovation.NewRoomId, renovation.NewRoomName, renovation.NewSize,
                    firstRoom.Floor, true));
                roomService.DeleteRoom(firstRoom.Id);
                roomService.SerializeRooms();
            }
        }

        public bool CheckAppointmentsForDate(DateTime startDate, DateTime endDate, string roomId)
        {
            AppointmentsService appointments = new AppointmentsService();
            foreach (Appointment a in appointments.GetAll())
            {
                if (a.room.Id.Equals(roomId))
                {
                    if (DateTime.Compare(startDate.Date, a.StartTime.Date) <= 0 && 
                        DateTime.Compare(endDate.Date, a.StartTime.Date) >= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
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
