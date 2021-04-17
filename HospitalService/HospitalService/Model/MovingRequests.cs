using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MovingRequests
    {
        public DateTime movingTime { get; set; }
        public int quantity { get; set; }
        public Room moveFromThisRoom { get; set; }
        public Room sendToThisRoom { get; set; }
        public int inventoryId { get; set; }
    }
}
