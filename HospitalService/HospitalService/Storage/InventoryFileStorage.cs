using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{
    public class InventoryFileStorage
    {
        private String FileLocation = @"..\..\..\Data\inventory.json";
        private List<Inventory> inventoryList;

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