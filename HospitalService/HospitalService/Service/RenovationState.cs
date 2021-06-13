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
            roomService.ChangeRoomAvailability(renovation.RoomId, true);

            if (renovation.SecondRoomId != null) 
            {
                roomService.ChangeRoomAvailability(renovation.SecondRoomId, true);
            }
            
        }
    }
}
