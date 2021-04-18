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
        List<MovingRequests> requests = new List<MovingRequests>();

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

        public void  moveDynamicInventory(Room sendToThisRoom, Room moveFromThisRoom, int quantity, int inventoryId , ObservableCollection<Inventory> roomInventory, RoomFileStorage roomStorage)
        {
            foreach (Inventory stavka in roomInventory)
            {
                if (stavka.Id == inventoryId)
                {
                    if (stavka.Quantity == quantity)
                    {
                        roomInventory.Remove(stavka);
                        moveFromThisRoom.inventory.Remove(stavka.Id);
                        storage.editRoom(moveFromThisRoom);

                        if (sendToThisRoom.inventory.ContainsKey(stavka.Id))
                        {
                            sendToThisRoom.inventory[stavka.Id] += quantity;
                            File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                        }
                        else
                        {
                            sendToThisRoom.inventory.Add(stavka.Id, quantity);
                            File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                        }
                    }
                    else if (quantity > stavka.Quantity)
                        MessageBox.Show("Ne postoji dovoljno stavki za premeštanje!");
                    else
                    {
                        stavka.Quantity = stavka.Quantity - quantity;
                        moveFromThisRoom.inventory[stavka.Id] = stavka.Quantity;
                        storage.editRoom(moveFromThisRoom);

                        if (sendToThisRoom.inventory.ContainsKey(stavka.Id))
                        {
                            sendToThisRoom.inventory[stavka.Id] += quantity;
                            File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                        }
                        else
                        {
                            sendToThisRoom.inventory.Add(stavka.Id, quantity);
                            File.WriteAllText(@"..\..\..\Data\rooms.json", JsonConvert.SerializeObject(roomStorage.GetAll()));
                        }
                    }
                    break;
                }
            }
        }

        public void analyzeRequests()
        {
            requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            ObservableCollection<Inventory> roomInventory = new ObservableCollection<Inventory>();
            Room moveToThisRoom = null;
            Room sentToThisRoom = null;

            if (requests != null && requests.Count != 0)
            {
                foreach (MovingRequests m in requests)
                {
                    foreach (Room r in storage.GetAll())
                    {
                        if (r.Id.Equals(m.moveFromThisRoom))
                        {
                            moveToThisRoom = r;
                            break;
                        }
                    }

                    foreach (Room r in storage.GetAll())
                    {
                        if (r.Id.Equals(m.sendToThisRoom))
                        {
                            sentToThisRoom = r;
                            break;
                        }
                    }

                    if (DateTime.Compare(m.movingTime, DateTime.Now) <= 0 && m.isDone == false)
                    {
                        foreach (int i in moveToThisRoom.inventory.Keys)
                        {
                            foreach (Inventory inv in inventoryList)
                            {
                                if (i == inv.Id)
                                {
                                    inv.Quantity = moveToThisRoom.inventory[i];
                                    roomInventory.Add(inv);
                                }
                            }
                        }

                        foreach (Inventory stavka in roomInventory)
                        {
                            if (stavka.Id == m.inventoryId)
                            {
                                if (stavka.Quantity == m.quantity)
                                {
                                    roomInventory.Remove(stavka);
                                    moveToThisRoom.inventory.Remove(stavka.Id);
                                    storage.editRoom(moveToThisRoom);

                                    if (sentToThisRoom.inventory.ContainsKey(stavka.Id))
                                    {
                                        sentToThisRoom.inventory[stavka.Id] += m.quantity;
                                        storage.editRoom(sentToThisRoom);
                                    }
                                    else
                                    {
                                        sentToThisRoom.inventory.Add(stavka.Id, m.quantity);
                                        storage.editRoom(sentToThisRoom);
                                    }
                                }
                                else if (m.quantity > stavka.Quantity)
                                    MessageBox.Show("Ne postoji dovoljno stavki za premeštanje!");
                                else
                                {
                                    stavka.Quantity = stavka.Quantity - m.quantity;
                                    moveToThisRoom.inventory[stavka.Id] = stavka.Quantity;
                                    storage.editRoom(moveToThisRoom);

                                    if (sentToThisRoom.inventory.ContainsKey(stavka.Id))
                                    {
                                        sentToThisRoom.inventory[stavka.Id] += m.quantity;
                                        storage.editRoom(sentToThisRoom);
                                    }
                                    else
                                    {
                                        sentToThisRoom.inventory.Add(stavka.Id, m.quantity);
                                        storage.editRoom(sentToThisRoom);
                                    }
                                }
                                break;
                            }
                        }

                        m.isDone = true;
                        File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                    }
                }

            }
        }
    }
}