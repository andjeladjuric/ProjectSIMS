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
        public List<RoomInventory> RoomInventory { get; set; }
        public List<MovingRequests> Requests { get; set; }
        public RoomInventoryStorage()
        {
            RoomInventory = new List<RoomInventory>();
            RoomInventory = JsonConvert.DeserializeObject<List<RoomInventory>>(File.ReadAllText(@"..\..\..\Data\roomInventory.json"));
        }

        public List<RoomInventory> GetAll()
        {
            return RoomInventory;
        }
        public void SerializeRoomInventory()
        {
            File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(RoomInventory));
        }

        public RoomInventory GetRoomInventoryByIds(string roomId, int itemId)
        {
            RoomInventory inventoryInRoom = null;

            foreach (RoomInventory r in RoomInventory)
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
                    for (int i = 0; i < RoomInventory.Count; i++)
                    {
                        r = RoomInventory[i];
                        if (r.Equals(moveFromHere))
                        {
                            RoomInventory.RemoveAt(i);
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
                    RoomInventory.Add(new RoomInventory(mr.sendToThisRoom, mr.inventoryId, mr.quantity));
                }
                else
                {
                    sendHere.Quantity += mr.quantity;
                }

                SerializeRoomInventory();
                Requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));

                for (int i = 0; i < Requests.Count; i++)
                {
                    if (Requests[i].movingTime == mr.movingTime)
                    {
                        Requests.RemoveAt(i);
                    }
                }

                File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(Requests));
            }
        }

        public void CheckMovingRequests()
        {
            Requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            if (Requests.Count != 0)
            {
                MovingRequests mr;
                for (int i = 0; i < Requests.Count; i++)
                {
                    mr = Requests[i];
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
            Requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            Requests.Add(mr);
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(Requests));
            Task t = new Task(() => RunThread(mr));
            t.Start();
        }
    }
}

