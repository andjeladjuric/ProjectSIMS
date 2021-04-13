using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public InventoryFileStorage()
        {
            inventoryList = new List<Inventory>();
            inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(FileLocation));
        }
        public List<Inventory> GetAll()
        {
            return inventoryList;
        }

        public void Save(Inventory newItem)
        {
            inventoryList.Add(newItem);

            foreach(Room r in storage.GetAll())
            {
                r.inventory = new Dictionary<int, int>();

                if(r.Type == RoomType.StorageRoom)
                {
                    r.inventory.Add(newItem.Id, newItem.Quantity);
                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                    break;
                }
            }

            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
        }

        public void Delete(int invId)
        {
            Inventory item;
            for (int i = 0; i < inventoryList.Count; i++)
            {
                item = inventoryList[i];
                if (item.Id.Equals(invId))
                {
                   
                    foreach (Room r in storage.GetAll())
                    {
                        if (r.inventory != null)
                        {
                            r.inventory.Remove(item.Id);
                            File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                             
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
                    item.Name = name;
                    item.EquipmentType = type;
                    int temp = 0;
                    int leftover = 0;

                    List<Room> storageRooms = storage.getByType(RoomType.StorageRoom);
                    List<Room> examinationRooms = storage.getByType(RoomType.ExaminationRoom);
                    List<Room> patientRooms = storage.getByType(RoomType.PatientRoom);
                    List<Room> operationRooms = storage.getByType(RoomType.OperatingRoom);

                    foreach(Room r in storageRooms)
                    {
                        if (r.inventory != null && r.inventory.Count != 0)
                        {
                            int value = r.inventory[item.Id];
                            if (item.Quantity > quantity)
                            {
                                temp = item.Quantity - quantity;

                                if (temp > value)
                                {
                                    r.inventory.Remove(item.Id);
                                    leftover = temp - value;

                                    while (leftover > 0)
                                    {
                                        foreach (Room re in examinationRooms)
                                        {
                                            if (re.inventory != null && re.inventory.Count != 0)
                                            {
                                                value = re.inventory[item.Id];
                                                if (leftover > value)
                                                {
                                                    re.inventory.Remove(item.Id);
                                                    leftover = leftover - value;
                                                    continue;
                                                }

                                                value = value - leftover;
                                                re.inventory[item.Id] = value;
                                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                                leftover = 0;
                                                break;
                                            }
                                        }

                                        foreach (Room ro in operationRooms)
                                        {
                                            if (ro.inventory != null && ro.inventory.Count != 0)
                                            {
                                                value = ro.inventory[item.Id];
                                                if (leftover > value)
                                                {
                                                    ro.inventory.Remove(item.Id);
                                                    leftover = leftover - value;
                                                    continue;
                                                }

                                                value = value - leftover;
                                                ro.inventory[item.Id] = value;
                                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                                leftover = 0;
                                                break;
                                            }
                                        }

                                        foreach (Room rp in patientRooms)
                                        {
                                            if (rp.inventory != null && rp.inventory.Count != 0)
                                            {
                                                value = rp.inventory[item.Id];
                                                if (leftover > value)
                                                {
                                                    rp.inventory.Remove(item.Id);
                                                    leftover = leftover - value;
                                                    continue;
                                                }

                                                value = value - leftover;
                                                rp.inventory[item.Id] = value;
                                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                                leftover = 0;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (value > temp)
                                {

                                    value = value - temp;
                                    r.inventory[item.Id] = value;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    break;
                                }

                            }
                            else if (item.Quantity < quantity)
                            {
                                temp = quantity - item.Quantity;
                                value = value + temp;
                                r.inventory[item.Id] = value;
                                File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                            }
                        }
                    }

                    item.Quantity = quantity;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    break;
                }
            }
            
        }

        public Inventory getOne(int id)
        {
            return inventoryList.Find(x => x.Id == id);
        }
    }
}