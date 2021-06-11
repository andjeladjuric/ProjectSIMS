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

        public RoomInventoryService()
        {
            roomInventoryRepository = new RoomInventoryRepository();
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
            InventoryService inventoryService = new InventoryService();

            foreach (RoomInventory item in GetAll())
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
        public void SerializeRoomInventory() => roomInventoryRepository.SerializeRoomInventory();
    }
}
