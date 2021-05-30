using HospitalService.Repositories;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace HospitalService.Service
{
    public class InventoryService
    {
        #region Fields

        private InventoryRepository inventory;
        private RoomInventoryRepository roomInventory;
        private RoomsRepository rooms;

        #endregion

        #region Constructors

        public InventoryService()
        {
            inventory = new InventoryRepository();
            roomInventory = new RoomInventoryRepository();
            rooms = new RoomsRepository();
        }

        #endregion

        #region Functions
        public void AddInventoryItem(Inventory newItem)
        {
            Room room = rooms.FindRoomByPriority();
            RoomInventory ri = new RoomInventory(room.Id, newItem.Id, newItem.Quantity);
            roomInventory.GetAll().Add(ri);
            inventory.Save(newItem);
        }

        public void Delete(int itemId)
        {
            if (itemId != 321 && GetAllTakenBeds() > 0)
            {
                MessageBox.Show("Postoje zauzeti kreveti!");
            }
            else
            {
                DeleteInventoryInRoom(itemId);
                DeleteRequests(itemId);
                inventory.Delete(itemId);
            }
        }

        private void DeleteInventoryInRoom(int itemId)
        {
            RoomInventory r;
            for (int j = 0; j < roomInventory.GetAll().Count; j++)
            {
                r = roomInventory.GetAll()[j];
                if (r.ItemId == itemId)
                {
                    roomInventory.GetAll().RemoveAt(j);
                    roomInventory.SerializeRoomInventory();
                }
            }
        }

        private void DeleteRequests(int itemId)
        {
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            MovingRequests movingRequest;
            for (int i = 0; i < requests.Count; i++)
            {
                movingRequest = requests[i];
                if (itemId == movingRequest.inventoryId)
                {
                    requests.RemoveAt(i);
                    File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                    continue;
                }
            }
        }

        private int GetAllTakenBeds()
        {
            RoomService roomService = new RoomService();
            int takenBeds = 0;
            foreach (Room r in roomService.GetAll())
            {
                takenBeds += new MedicalRecordService().TakenBeds(r.Id);
            }

            return takenBeds;
        }

        public void Edit(int id, String name, Equipment type, int quantity, string supplier)
        {
            Inventory item;
            for (int i = 0; i < inventory.GetAll().Count; i++)
            {
                item = inventory.GetAll()[i];
                if (item.Id.Equals(id))
                {

                    if (rooms.GetAll().Count == 0)
                    {
                        MessageBox.Show("Ne postoje sobe u kojima inventar može da se izmeni!");
                    }

                    EnlargeQuantity(item, quantity);
                    item.Name = name;
                    item.EquipmentType = type;
                    item.Supplier = supplier;
                    inventory.SerializeInventory();
                }
            }
        }

        private void EnlargeQuantity(Inventory item, int quantity)
        {
            int temp;

            if (rooms.GetByType(RoomType.StorageRoom).Count == 0)
            {
                MessageBox.Show("Količina opreme se može mijenjati samo unutar skladišta!");
            }
            else
            {
                if (item.Quantity < quantity)
                {
                    temp = quantity - item.Quantity;

                    foreach (Room room in rooms.GetByType(RoomType.StorageRoom))
                    {
                        RoomInventory r = roomInventory.GetRoomInventoryByIds(room.Id, item.Id);
                        r.Quantity += temp;
                        item.Quantity = quantity;
                        roomInventory.SerializeRoomInventory();
                        return;
                    }
                }
            }
        }

        public void ReduceQuantity(int enteredQuantity, Inventory SelectedItem, Room SelectedRoom)
        {
            Inventory item = GetOne(SelectedItem.Id);

            if (item.Quantity == enteredQuantity)
            {
                GetAll().Remove(item);
            }
            else
            {
                item.Quantity -= enteredQuantity;
            }

            EditItem(item);

            RoomInventoryService roomInventoryService = new RoomInventoryService();
            RoomInventory inventoryInRoom = roomInventoryService.GetRoomInventoryByIds(SelectedRoom.Id, SelectedItem.Id);

            if (inventoryInRoom.Quantity == enteredQuantity)
            {
                roomInventoryService.GetAll().Remove(inventoryInRoom);
            }
            else
            {
                inventoryInRoom.Quantity -= enteredQuantity;
            }

            roomInventoryService.EditItem(inventoryInRoom);
        }

        public List<Inventory> GetAll() => inventory.GetAll();
        public Inventory GetOne(int id) => inventory.GetOne(id);
        public void EditItem(Inventory i) => inventory.EditItem(i);

        #endregion
    }
}
