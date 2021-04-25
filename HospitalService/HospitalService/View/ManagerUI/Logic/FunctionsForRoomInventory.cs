using HospitalService.Storage;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.View.ManagerUI.Logic
{
    public class FunctionsForRoomInventory
    {
        public List<RoomInventory> roomInventories { get; set; }
        public List<MovingRequests> requests { get; set; }
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

        public void AnalyzeRequests(MovingRequests mr)
        {
            RoomInventory moveFromHere = GetRoomInventoryByIds(mr.moveFromThisRoom, mr.inventoryId);
            RoomInventory sendHere = GetRoomInventoryByIds(mr.sendToThisRoom, mr.inventoryId);

            if(moveFromHere.Quantity == mr.quantity)
            {
                RoomInventory r;
                for (int i = 0; i < roomInventories.Count; i++)
                {
                    r = roomInventories[i];
                    if (r.Equals(moveFromHere))
                    {
                        roomInventories.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                moveFromHere.Quantity -= mr.quantity;
            }

            if(sendHere == null)
            {
                RoomInventory ri = new RoomInventory();
                ri.RoomId = mr.sendToThisRoom;
                ri.ItemId = mr.inventoryId;
                ri.Quantity = mr.quantity;
                roomInventories.Add(ri);
            }
            else
            {
                sendHere.Quantity += mr.quantity;
            }

            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            mr.isDone = true;
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
            SerializeRoomInventory();
        }

        public void CheckRequests()
        {
            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            if (requests.Count != 0)
            {
                MovingRequests mr;
                for(int i = 0; i < requests.Count; i++)
                {
                    mr = requests[i];
                    if(DateTime.Compare(mr.movingTime, DateTime.Now) <= 0 && mr.isDone == false)
                    {
                        AnalyzeRequests(mr);
                    }
                    else
                    {
                        Task t = new Task(() => mr.RunThread());
                        t.Start();
                    }

                    if (mr.isDone == true)
                        requests.RemoveAt(i);
                }
            }
        }

        /*public void RunTask(MovingRequests mr)
        {
            Task t = new Task(() => mr.RunThread());
            t.Start();
        }*/

        public void StartMoving(MovingRequests mr)
        {
            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            requests.Add(mr);
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
            Task t = new Task(() => mr.RunThread());
            t.Start();
        }

        /*public void RunThread(MovingRequests mr)
        {
            TimeSpan time = mr.movingTime.Subtract(DateTime.Now);

            if(time > new TimeSpan(0,0,0))
                Thread.Sleep(time);
            
            AnalyzeRequests(mr);
        }*/
    }
}
