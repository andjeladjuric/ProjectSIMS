using HospitalService.Repositories;
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
        List<MovingRequests> movingRequests;

        public RoomInventoryService()
        {
            roomInventoryRepository = new RoomInventoryRepository();
            movingRequests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
        }

        public List<MovingRequests> LoadRequests()
        {
            return movingRequests;
        }

        public void SerializeRequests()
        {
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(movingRequests));
        }

        public void AnalyzeRequests(MovingRequests request)
        {
            RoomService roomService = new RoomService();
            InventoryService inventoryService = new InventoryService();

            if (roomService.GetOne(request.moveFromThisRoom) != null && roomService.GetOne(request.sendToThisRoom) != null
                && inventoryService.GetOne(request.inventoryId) != null)

            {
                RoomInventory moveFromHere = GetRoomInventoryByIds(request.moveFromThisRoom, request.inventoryId);
                RoomInventory sendHere = GetRoomInventoryByIds(request.sendToThisRoom, request.inventoryId);

                if (moveFromHere.Quantity == request.quantity)
                {
                    RemoveItemFromRoom(moveFromHere);
                }
                else
                {
                    moveFromHere.Quantity -= request.quantity;
                }

                if (sendHere == null)
                {
                    roomInventoryRepository.GetAll().Add(new RoomInventory(request.sendToThisRoom, request.inventoryId, request.quantity));
                }
                else
                {
                    sendHere.Quantity += request.quantity;
                }

                roomInventoryRepository.SerializeRoomInventory();
                RemoveRequests(request);
            }
        }

        private void RemoveItemFromRoom(RoomInventory moveFromHere)
        {
            RoomInventory item;
            for (int i = 0; i < roomInventoryRepository.GetAll().Count; i++)
            {
                item = roomInventoryRepository.GetAll()[i];
                if (item.Equals(moveFromHere))
                {
                    roomInventoryRepository.GetAll().RemoveAt(i);
                    break;
                }
            }
        }

        private void RemoveRequests(MovingRequests mr)
        {
            movingRequests = LoadRequests();

            for (int i = 0; i < movingRequests.Count; i++)
            {
                if (movingRequests[i].movingTime == mr.movingTime)
                {
                    movingRequests.RemoveAt(i);
                }
            }

            SerializeRequests();
        }

        public void CheckRequests()
        {
            movingRequests = LoadRequests();
            if (movingRequests.Count != 0)
            {
                MovingRequests request;
                for (int i = 0; i < movingRequests.Count; i++)
                {
                    request = movingRequests[i];
                    if (DateTime.Compare(request.movingTime, DateTime.Now) <= 0)
                    {
                        AnalyzeRequests(request);
                    }
                }
            }
        }

        public void StartMoving(MovingRequests movingRequest)
        {
            movingRequests = LoadRequests();
            movingRequests.Add(movingRequest);
            SerializeRequests();
            CheckRequests();
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
