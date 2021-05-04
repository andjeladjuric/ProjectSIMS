using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.Storage
{
    class RoomInventoryStorage
    {
        public List<RoomInventory> roomInventory { get; set; }
        public List<MovingRequests> requests { get; set; }
        public RoomInventoryStorage()
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

        public void AnalyzeRequests(MovingRequests mr)
        {
            RoomFileStorage roomStorage = new RoomFileStorage();
            InventoryFileStorage inventoryStorage = new InventoryFileStorage();

            if (roomStorage.getOne(mr.moveFromThisRoom) != null && roomStorage.getOne(mr.sendToThisRoom) != null
                && inventoryStorage.getOne(mr.inventoryId) != null)

            {
                RoomInventory moveFromHere = GetRoomInventoryByIds(mr.moveFromThisRoom, mr.inventoryId);
                RoomInventory sendHere = GetRoomInventoryByIds(mr.sendToThisRoom, mr.inventoryId);

                if (moveFromHere.Quantity == mr.quantity)
                {
                    RoomInventory r;
                    for (int i = 0; i < roomInventory.Count; i++)
                    {
                        r = roomInventory[i];
                        if (r.Equals(moveFromHere))
                        {
                            roomInventory.RemoveAt(i);
                            break;
                        }
                    }
                }
                else
                {
                    moveFromHere.Quantity -= mr.quantity;
                }

                if (sendHere == null)
                {
                    roomInventory.Add(new RoomInventory(mr.sendToThisRoom, mr.inventoryId, mr.quantity));
                }
                else
                {
                    sendHere.Quantity += mr.quantity;
                }

                SerializeRoomInventory();
                requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));

                for (int i = 0; i < requests.Count; i++)
                {
                    if (requests[i].movingTime == mr.movingTime)
                    {
                        requests.RemoveAt(i);
                    }
                }

                File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
            }
        }

        public void CheckRequests()
        {
            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            if (requests.Count != 0)
            {
                MovingRequests mr;
                for (int i = 0; i < requests.Count; i++)
                {
                    mr = requests[i];
                    if (DateTime.Compare(mr.movingTime, DateTime.Now) <= 0)
                    {
                        AnalyzeRequests(mr);
                    }
                    else
                    {
                        Task t = new Task(() => RunThread(mr));
                        t.Start();
                    }
                }
            }
        }
        public void RunThread(MovingRequests mr)
        {
            TimeSpan time = mr.movingTime.Subtract(DateTime.Now);

            if (time > new TimeSpan(0, 0, 0))
                Thread.Sleep(time);

            AnalyzeRequests(mr);
        }

        public void StartMoving(MovingRequests mr)
        {
            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            requests.Add(mr);
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
            Task t = new Task(() => RunThread(mr));
            t.Start();
        }
    }
}

