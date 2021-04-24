using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{
    public class RoomInventory
    {
        public string RoomId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public RoomInventory(string roomId, int itemId, int quantity)
        {
            this.RoomId = roomId;
            this.ItemId = itemId;
            this.Quantity = quantity;
        }

        public RoomInventory()
        {
        }
    }
}
