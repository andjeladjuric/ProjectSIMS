using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class RoomInventoryRepository
    {
        public List<RoomInventory> roomInventory { get; set; }
        public RoomInventoryRepository()
        {
            roomInventory = new List<RoomInventory>();
            roomInventory = JsonConvert.DeserializeObject<List<RoomInventory>>(File.ReadAllText(@"..\..\..\Data\roomInventory.json"));
        }

        public List<RoomInventory> GetAll()
        {
            return roomInventory;
        }
        public void SerializeRoomInventory()
        {
            File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInventory));
        }

        public void Save(RoomInventory newItem)
        {
            roomInventory.Add(newItem);
            SerializeRoomInventory();
        }

        public RoomInventory GetRoomInventoryByIds(string roomId, int itemId)
        {
            RoomInventory inventoryInRoom = null;

            foreach (RoomInventory r in roomInventory)
            {
                if (r.RoomId.Equals(roomId) && r.ItemId == itemId)
                    inventoryInRoom = r;
            }

            return inventoryInRoom;
        }
    }
}
