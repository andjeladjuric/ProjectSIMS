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
            if (storage.GetAll().Count == 0)
            {
                MessageBox.Show("Ne postoji soba u koju možete ubaciti inventar!");
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.StorageRoom)
                {
                    r.inventory.Add(newItem.Id, newItem.Quantity);
                    inventoryList.Add(newItem);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                    return;
                }
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.ExaminationRoom)
                {
                    r.inventory.Add(newItem.Id, newItem.Quantity);
                    inventoryList.Add(newItem);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                    return;
                }
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.PatientRoom)
                {
                    r.inventory.Add(newItem.Id, newItem.Quantity);
                    inventoryList.Add(newItem);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                    return;
                }
            }

            foreach (Room r in storage.GetAll())
            {
                if (r.Type == RoomType.OperatingRoom)
                {
                    r.inventory.Add(newItem.Id, newItem.Quantity);
                    inventoryList.Add(newItem);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
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
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));

                    int value = 0;
                    int temp = 0;

                    if(storage.GetAll().Count == 0)
                    {
                        MessageBox.Show("Ne postoje sobe u kojima inventar može da se izmeni!");
                    }
                     
                    if (item.Quantity > quantity) //oduzimanje
                    {
                        temp = item.Quantity - quantity;
                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.StorageRoom)
                            {
                                if (!r.inventory.ContainsKey(item.Id))
                                    continue;

                                value = r.inventory[item.Id];
                                if (value - temp > 0) //ima dovoljno u skladistu
                                {
                                    value -= temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }

                                temp -= value;
                                r.inventory.Remove(item.Id);
                                if(temp == 0)
                                {
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.ExaminationRoom)
                            {
                                if (!r.inventory.ContainsKey(item.Id))
                                    continue;

                                value = r.inventory[item.Id];
                                if (value - temp > 0) //ima dovoljno u skladistu
                                {
                                    value -= temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }

                                temp -= value;
                                r.inventory.Remove(item.Id);
                                if (temp == 0)
                                {
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.OperatingRoom)
                            {
                                if (!r.inventory.ContainsKey(item.Id))
                                    continue;

                                value = r.inventory[item.Id];
                                if (value - temp > 0) //ima dovoljno u skladistu
                                {
                                    value -= temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }

                                temp -= value;
                                r.inventory.Remove(item.Id);
                                if (temp == 0)
                                {
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.PatientRoom)
                            {
                                if (!r.inventory.ContainsKey(item.Id))
                                    continue;

                                value = r.inventory[item.Id];
                                if (value - temp > 0) //ima dovoljno u skladistu
                                {
                                    value -= temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }

                                temp -= value;
                                r.inventory.Remove(item.Id);
                                if (temp == 0)
                                {
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }
                    }else if(item.Quantity < quantity) // dodavanje
                    {
                        temp = quantity - item.Quantity;
                        foreach(Room r in storage.GetAll()) 
                        {
                            if (r.Type == RoomType.StorageRoom)
                            {
                                if (r.inventory.ContainsKey(item.Id))
                                {
                                    value = r.inventory[item.Id];

                                    value += temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                                else
                                {
                                    value = temp;
                                    r.inventory.Add(item.Id, value);
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.ExaminationRoom)
                            {
                                if (r.inventory.ContainsKey(item.Id))
                                {
                                    value = r.inventory[item.Id];

                                    value += temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                                else
                                {
                                    value = temp;
                                    r.inventory.Add(item.Id, value);
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.OperatingRoom)
                            {
                                if (r.inventory.ContainsKey(item.Id))
                                {
                                    value = r.inventory[item.Id];

                                    value += temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                                else
                                {
                                    value = temp;
                                    r.inventory.Add(item.Id, value);
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }

                        foreach (Room r in storage.GetAll())
                        {
                            if (r.Type == RoomType.PatientRoom)
                            {
                                if (r.inventory.ContainsKey(item.Id))
                                {
                                    value = r.inventory[item.Id];

                                    value += temp;
                                    r.inventory[item.Id] = value;
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                                else
                                {
                                    value = temp;
                                    r.inventory.Add(item.Id, value);
                                    item.Quantity = quantity;
                                    File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(storage.GetAll()));
                                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(inventoryList));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public Inventory getOne(int id)
        {
            return inventoryList.Find(x => x.Id == id);
        }
    }
}