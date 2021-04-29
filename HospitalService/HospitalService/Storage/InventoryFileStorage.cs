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
        private List<Inventory> inventoryList;
        RoomFileStorage storage = new RoomFileStorage();
        public List<MovingRequests> requests { get; set; }

        public InventoryFileStorage()
        {
            inventoryList = new List<Inventory>();
            inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(FileLocation));
        }
        public void SerializeInventory()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
        }

        public List<Inventory> DeserializeInventory()
        {
            return JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(FileLocation));
        }

        public List<Inventory> GetAll()
        {
            return inventoryList;
        }

        public void Save(Inventory newItem)
        {
            List<RoomInventory> roomInventories = JsonConvert.DeserializeObject<List<RoomInventory>>(File.ReadAllText(@"..\..\..\Data\roomInventory.json"));

            if (storage.GetAll().Count == 0)
            {
                MessageBox.Show("Ne postoji soba u koju možete ubaciti inventar!");
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.StorageRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    roomInventories.Add(roomInventory);
                    inventoryList.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInventories));
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    return;
                }
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.ExaminationRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    roomInventories.Add(roomInventory);
                    inventoryList.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInventories));
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    return;
                }
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.PatientRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    roomInventories.Add(roomInventory);
                    inventoryList.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInventories));
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    return;
                }
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.OperatingRoom)
                {
                    RoomInventory roomInventory = new RoomInventory(r.Id, newItem.Id, newItem.Quantity);
                    roomInventories.Add(roomInventory);
                    inventoryList.Add(newItem);

                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInventories));
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    return;
                }
            }
        }

        public void Delete(int invId)
        {
            Inventory item;
            for (int i = 0; i < inventoryList.Count; i++)
            {
                item = inventoryList[i];
                if (item.Id.Equals(invId))
                {
                    DeleteInventoryInRoom(invId);
                    DeleteRequests(invId);
                    inventoryList.RemoveAt(i);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    break;
                }
            }
        }

        private void DeleteInventoryInRoom(int itemId)
        {
            RoomInventory r;
            RoomInventoryStorage f = new RoomInventoryStorage();
            for (int j = 0; j < f.GetAll().Count; j++)
            {
                r = f.GetAll()[j];
                if (r.ItemId == itemId)
                {
                    f.GetAll().RemoveAt(j);
                    f.SerializeRoomInventory();
                }
            }
        }

        private void DeleteRequests(int itemId)
        {
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            MovingRequests mr;
            for (int i = 0; i < requests.Count; i++)
            {
                mr = requests[i];
                if (itemId == mr.inventoryId)
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
            for (int i = 0; i < inventoryList.Count; i++)
            {
                item = inventoryList[i];
                if (item.Id.Equals(id))
                { 
                    int temp = 0;

                    if(storage.GetAll().Count == 0)
                    {
                        MessageBox.Show("Ne postoje sobe u kojima inventar može da se izmeni!");
                    }
                     
                    if(item.Quantity < quantity) // dodavanje
                    {
                        temp = quantity - item.Quantity;
                        RoomInventoryStorage f = new RoomInventoryStorage();

                        foreach(Room room in storage.getByType(RoomType.StorageRoom))
                        {
                            RoomInventory r = f.GetRoomInventoryByIds(room.Id, item.Id);
                            r.Quantity += temp;
                            item.Quantity = quantity;
                            f.SerializeRoomInventory();
                            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                            return;
                        }
                    }

                    item.Name = name;
                    item.EquipmentType = type;
                    item.Supplier = supplier;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                }
            }
        }

        public static Inventory getOne(int id)
        {
            List<Inventory> inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(@"..\..\..\Data\inventory.json"));
            return inventoryList.Find(x => x.Id == id);
        }
    }
}