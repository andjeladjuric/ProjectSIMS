using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.View.ManagerUI.Logic
{
    public class FunctionsForRoomInventory
    {
        List<RoomInventory> roomInventories { get; set; }
        public FunctionsForRoomInventory()
        {
            roomInventories = new List<RoomInventory>();
            roomInventories = JsonConvert.DeserializeObject<List<RoomInventory>>(File.ReadAllText(@"..\..\..\Data\roomInventory.json"));
        }

        public List<RoomInventory> GetAll()
        {
            return roomInventories;
        }
        public void SerializeRoomInventory()
        {
            File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInventories));
        }
        public List<RoomInventory> DeserializeRoomInventory()
        {
            roomInventories = new List<RoomInventory>();
            roomInventories = JsonConvert.DeserializeObject<List<RoomInventory>>(File.ReadAllText(@"..\..\..\Data\roomInventory.json"));
            return roomInventories;
        }

        public RoomInventory GetRoomInventoryByIds(string roomId, int itemId)
        {
            RoomInventory roomInventory = new RoomInventory();

            foreach(RoomInventory r in roomInventories)
            {
                if (r.RoomId.Equals(roomId) && r.ItemId == itemId)
                    roomInventory = r;
            }

            return roomInventory;
        }
    }
}
