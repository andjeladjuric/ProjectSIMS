using HospitalService.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace Model
{
    public class InventoryFileStorage
    {
        private String FileLocation = @"..\..\..\Data\inventory.json";
        private List<Inventory> inventory;
        private RoomFileStorage roomStorage = new RoomFileStorage();
        public List<MovingRequests> requests { get; set; }

        public InventoryFileStorage()
        {
            inventory = new List<Inventory>();
            inventory = JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(FileLocation));
        }
        public void SerializeInventory()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventory));
        }

        public List<Inventory> GetAll()
        {
            return inventory;
        }

        public void Save(Inventory newItem)
        {
            List<RoomInventory> inventoryInRooms = JsonConvert.DeserializeObject<List<RoomInventory>>(File.ReadAllText(@"..\..\..\Data\roomInventory.json"));

            if (roomStorage.GetAll().Count == 0)
            {
                MessageBox.Show("Ne postoji soba u koju možete ubaciti inventar!");
            }

            foreach (Room r in roomStorage.GetAll())
            {
                if (r.Type == RoomType.StorageRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    inventoryInRooms.Add(roomInventory);
                    inventory.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(inventoryInRooms));
                    SerializeInventory();
                    return;
                }
            }

            foreach (Room r in roomStorage.GetAll())
            {
                if (r.Type == RoomType.ExaminationRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    inventoryInRooms.Add(roomInventory);
                    inventory.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(inventoryInRooms));
                    SerializeInventory();
                    return;
                }
            }

            foreach (Room r in roomStorage.GetAll())
            {
                if (r.Type == RoomType.PatientRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    inventoryInRooms.Add(roomInventory);
                    inventory.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(inventoryInRooms));
                    SerializeInventory();
                    return;
                }
            }

            foreach (Room r in roomStorage.GetAll())
            {
                if (r.Type == RoomType.OperatingRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    inventoryInRooms.Add(roomInventory);
                    inventory.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(inventoryInRooms));
                    SerializeInventory();
                    return;
                }
            }
        }

        public void Delete(int invId)
        {
            Inventory item;
            for (int i = 0; i < inventory.Count; i++)
            {
                item = inventory[i];
                if (item.Id.Equals(invId))
                {
                    DeleteInventoryInRoom(invId);
                    DeleteRequests(invId);
                    inventory.RemoveAt(i);
                    SerializeInventory();
                    break;
                }
            }
        }

        private void DeleteInventoryInRoom(int itemId)
        {
            RoomInventory r;
            RoomInventoryStorage roomInventoryStorage = new RoomInventoryStorage();
            for (int j = 0; j < roomInventoryStorage.GetAll().Count; j++)
            {
                r = roomInventoryStorage.GetAll()[j];
                if (r.ItemId == itemId)
                {
                    roomInventoryStorage.GetAll().RemoveAt(j);
                    roomInventoryStorage.SerializeRoomInventory();
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

        public void Edit(int id, String name, Equipment type, int quantity, string supplier)
        {
            Inventory item;
            for (int i = 0; i < inventory.Count; i++)
            {
                item = inventory[i];
                if (item.Id.Equals(id))
                { 
                    int temp = 0;

                    if(roomStorage.GetAll().Count == 0)
                    {
                        MessageBox.Show("Ne postoje sobe u kojima inventar može da se izmeni!");
                    }
                     
                    if(item.Quantity < quantity)
                    {
                        temp = quantity - item.Quantity;
                        RoomInventoryStorage roomInventoryStorage = new RoomInventoryStorage();

                        foreach(Room room in roomStorage.getByType(RoomType.StorageRoom))
                        {
                            RoomInventory r = roomInventoryStorage.GetRoomInventoryByIds(room.Id, item.Id);
                            r.Quantity += temp;
                            item.Quantity = quantity;
                            roomInventoryStorage.SerializeRoomInventory();
                            SerializeInventory();
                            return;
                        }
                    }

                    item.Name = name;
                    item.EquipmentType = type;
                    item.Supplier = supplier;
                    SerializeInventory();
                }
            }
        }

        public Inventory getOne(int id)
        {
            List<Inventory> inventory= JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(@"..\..\..\Data\inventory.json"));
            return inventory.Find(x => x.Id == id);
        }
    }
}