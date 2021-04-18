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
        public string moveFromThisRoom { get; set; }
        public string sendToThisRoom { get; set; }
        public int inventoryId { get; set; }
        public Boolean isDone { get; set; }
    }
}
