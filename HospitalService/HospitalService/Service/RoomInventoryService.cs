﻿using HospitalService.Repositories;
using Model;
using Newtonsoft.Json;
using Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.Service
{
    public class RoomInventoryService
    {
        RoomInventoryRepository roomInventoryRepository;

        public RoomInventoryService()
        {
            roomInventoryRepository = new RoomInventoryRepository();
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
                    for (int i = 0; i < roomInventoryRepository.GetAll().Count; i++)
                    {
                        r = roomInventoryRepository.GetAll()[i];
                        if (r.Equals(moveFromHere))
                        {
                            roomInventoryRepository.GetAll().RemoveAt(i);
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
                    roomInventoryRepository.GetAll().Add(new RoomInventory(mr.sendToThisRoom, mr.inventoryId, mr.quantity));
                }
                else
                {
                    sendHere.Quantity += mr.quantity;
                }

                roomInventoryRepository.SerializeRoomInventory();
                List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));

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
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
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
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            requests.Add(mr);
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
            Task t = new Task(() => RunThread(mr));
            t.Start();
        }

        public int GetNextAvailableBed(string roomId)
        {
            RoomInventory roomInventory = GetRoomInventoryByIds(roomId, 321);
            int takenBeds = new MedicalRecordService().TakenBeds(roomId);
            if (roomInventory.Quantity == takenBeds)
                return 0;
            else
                return ++takenBeds;
        }

        public bool Filter(object obj, string FilterId, string FilterName, string FilterSupplier, int SelectedType)
        {
            var data = obj as Inventory;
            if (data != null)
            {
                if (!FilterId.Equals(string.Empty) || !FilterName.Equals(string.Empty) ||
                    !FilterSupplier.Equals(string.Empty))
                {
                    return data.Id.ToString().Trim().Contains(FilterId) && data.Name.ToLower().Trim().Contains(FilterName) &&
                        data.Supplier.ToLower().Trim().Contains(FilterSupplier);
                }

                if (SelectedType != -1)
                {
                    if (SelectedType == 0)
                        return true;

                    if (SelectedType == 1)
                        return data.EquipmentType.Equals(Equipment.Static);

                    if (SelectedType == 2)
                        return data.EquipmentType.Equals(Equipment.Dynamic);

                    return true;
                }

                return true;
            }
            return false;
        }

        public ObservableCollection<Inventory> LoadRoomInventory(Room room)
        {
            ObservableCollection<Inventory> Inventory = new ObservableCollection<Inventory>();
            RoomInventoryService service = new RoomInventoryService();
            InventoryService inventoryService = new InventoryService();

            foreach (RoomInventory item in service.GetAll())
            {
                if (item.RoomId.Equals(room.Id))
                {
                    foreach (Inventory i in inventoryService.GetAll())
                    {
                        if (item.ItemId == i.Id)
                        {
                            Inventory.Add(new Inventory(item.ItemId, i.Name, i.EquipmentType, item.Quantity, i.Supplier));
                            break;
                        }
                    }
                }
            }

            return Inventory;
        }

        public List<RoomInventory> GetAll() => roomInventoryRepository.GetAll();
        public RoomInventory GetRoomInventoryByIds(string roomId, int itemId) => roomInventoryRepository.GetRoomInventoryByIds(roomId, itemId);
        public void EditItem(RoomInventory r) => roomInventoryRepository.EditItem(r);
        public RoomInventory GetInventoryForRoom(string roomId) => roomInventoryRepository.GetInventoryForRoom(roomId);
    }
}
