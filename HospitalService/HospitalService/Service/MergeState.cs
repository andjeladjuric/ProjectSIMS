using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class MergeState : RenovationState
    {
        public override void Renovate()
        {
            RoomService roomService = new RoomService();
            Room firstRoom = roomService.GetOne(renovation.RoomId);
            Room secondRoom = roomService.GetOne(renovation.SecondRoomId);
            double newSize = firstRoom.Size + secondRoom.Size;

            if (new ScheduleService().CheckAppointmentsForDate(renovation.Start, renovation.End, firstRoom.Id) &&
                        new ScheduleService().CheckAppointmentsForDate(renovation.Start, renovation.End, secondRoom.Id))
            {
                roomService.AddRoom(new Room(renovation.NewRoomType, renovation.RoomId, renovation.NewRoomName, newSize, firstRoom.Floor, true));
                roomService.DeleteRoom(firstRoom.Id);
                roomService.DeleteRoom(secondRoom.Id);
                roomService.SerializeRooms();
            }
        }
    }
}
