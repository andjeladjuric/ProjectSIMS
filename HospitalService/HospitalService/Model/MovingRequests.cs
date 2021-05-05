using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Model
{
    public class MovingRequests
    {
        public DateTime movingTime { get; set; }
        public int quantity { get; set; }
        public string moveFromThisRoom { get; set; }
        public string sendToThisRoom { get; set; }
        public int inventoryId { get; set; }

        public MovingRequests()
        {
        }

        public MovingRequests(DateTime movingTime, int quantity, string moveFromThisRoom, string sendToThisRoom, int inventoryId)
        {
            this.movingTime = movingTime;
            this.quantity = quantity;
            this.moveFromThisRoom = moveFromThisRoom;
            this.sendToThisRoom = sendToThisRoom;
            this.inventoryId = inventoryId;
        }

    }


}
