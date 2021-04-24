using HospitalService.View.ManagerUI.Logic;
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
            FunctionsForRoomInventory f = new FunctionsForRoomInventory();
            for (int i = 0; i < inventoryList.Count; i++)
            {
                item = inventoryList[i];
                if (item.Id.Equals(invId))
                {
                    RoomInventory r;
                    for ( int j = 0; j < f.GetAll().Count; j++)
                    {
                        r = f.GetAll()[j];
                        if (r.ItemId == invId)
                        {
                            f.GetAll().RemoveAt(j);
                            f.SerializeRoomInventory();
                        }
                    }

                    inventoryList.RemoveAt(i);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    break;
                }
            }
        }

        public void Edit(int id, String name, Equipment type, int quantity)
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
                        FunctionsForRoomInventory f = new FunctionsForRoomInventory();

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
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                }
            }
        }

        public Inventory getOne(int id)
        {
            return inventoryList.Find(x => x.Id == id);
        }

        public List<Inventory> getInventoryForRoom(Room r)
        {
            List<Inventory> roomInventory = new List<Inventory>();

            foreach (var item in r.inventory)
            {
                foreach (Inventory inv in inventoryList)
                {
                    if (item.Key == inv.Id)
                    {
                        roomInventory.Add(new Inventory { Id = item.Key, Quantity = item.Value, 
                            Name = inv.Name, EquipmentType = inv.EquipmentType });
                    }
                }
            }

            return roomInventory;
        }
        public void AnalyzeRequests()
        {
            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            List<Inventory> roomInventory;
            Room moveFromThisRoom = null;
            Room sendToThisRoom = null;

            if (requests != null && requests.Count != 0)
            {
                foreach(MovingRequests movingRequests in requests)
                {
                    if(DateTime.Compare(movingRequests.movingTime, DateTime.Now) <= 0 && movingRequests.isDone == false)
                    {
                        foreach(Room r in storage.GetAll())
                        {
                            if (r.Id.Equals(movingRequests.moveFromThisRoom))
                            {
                                moveFromThisRoom = r;
                                break;
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Id.Equals(movingRequests.sendToThisRoom))
                            {
                                sendToThisRoom = r;
                                break;
                            }
                        }

                        roomInventory = getInventoryForRoom(moveFromThisRoom);

                        foreach(Inventory inv in roomInventory)
                        {
                            if(inv.Id == movingRequests.inventoryId)
                            {
                                if(inv.Quantity == movingRequests.quantity)
                                {
                                    roomInventory.Remove(inv);
                                    moveFromThisRoom.inventory.Remove(inv.Id);

                                    if (sendToThisRoom.inventory.ContainsKey(movingRequests.inventoryId))
                                    {
                                        sendToThisRoom.inventory[movingRequests.inventoryId] += movingRequests.quantity;
                                    }
                                    else
                                    {
                                        sendToThisRoom.inventory.Add(movingRequests.inventoryId, movingRequests.quantity);
                                    }

                                    storage.editRoom(moveFromThisRoom);
                                    storage.editRoom(sendToThisRoom);
                                    movingRequests.isDone = true;
                                    File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                                }
                                else if(inv.Quantity > movingRequests.quantity)
                                {
                                    inv.Quantity = inv.Quantity - movingRequests.quantity;
                                    moveFromThisRoom.inventory[inv.Id] = inv.Quantity;

                                    if (sendToThisRoom.inventory.ContainsKey(movingRequests.inventoryId))
                                    {
                                        sendToThisRoom.inventory[movingRequests.inventoryId] += movingRequests.quantity;
                                    }
                                    else
                                    {
                                        sendToThisRoom.inventory.Add(movingRequests.inventoryId, movingRequests.quantity);
                                    }

                                    storage.editRoom(moveFromThisRoom);
                                    storage.editRoom(sendToThisRoom);
                                    movingRequests.isDone = true;
                                    File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}