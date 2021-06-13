using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class OtherRenovationState : RenovationState
    {
        public override void Renovate()
        {
            RoomService roomService = new RoomService();
            Room room = roomService.GetOne(renovation.RoomId);
            room.IsFree = true;
            roomService.UpdateRoom(room);
        }
    }
}
