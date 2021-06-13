using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public abstract class RenovationState
    {
        protected Renovation renovation;

        public void SetContext(Renovation renovation)
        {
            this.renovation = renovation;
        }

        public abstract void Renovate();
        public void ChangeRoomAvailability()
        {
            RoomService roomService = new RoomService();
            Room room = roomService.GetOne(renovation.RoomId);
            room.IsFree = true;
            roomService.UpdateRoom(room);
        }
    }
}
