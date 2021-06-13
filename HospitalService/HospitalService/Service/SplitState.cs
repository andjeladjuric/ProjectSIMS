using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class SplitState : RenovationState
    {
        public override void Renovate()
        {
            RoomService roomService = new RoomService();
            Room firstRoom = roomService.GetOne(renovation.RoomId);
            double firstSize = firstRoom.Size - renovation.NewSize;

            if (new ScheduleService().CheckAppointmentsForDate(renovation.Start, renovation.End, renovation.RoomId))
            {
                roomService.AddRoom(new Room(renovation.NewRoomType, renovation.RoomId, firstRoom.Name, firstSize, firstRoom.Floor, true));
                roomService.AddRoom(new Room(renovation.NewRoomType, renovation.NewRoomId, renovation.NewRoomName, renovation.NewSize,
                    firstRoom.Floor, true));
                roomService.DeleteRoom(firstRoom.Id);
                roomService.SerializeRooms();
            }
        }
    }
}
