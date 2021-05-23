using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace HospitalService.Repositories
{
    public class InventoryRepository
    {
        private string FileLocation = @"..\..\..\Data\inventory.json";
        private List<Inventory> inventory;

        public InventoryRepository()
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

        public Inventory GetOne(int id)
        {
            List<Inventory> inventory = JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText(@"..\..\..\Data\inventory.json"));
            return inventory.Find(x => x.Id == id);
        }

        public void Save(Inventory newItem)
        {
            inventory.Add(newItem);
            SerializeInventory();
        }

        public void Delete(int Id)
        {
            Inventory item;
            for (int i = 0; i < inventory.Count; i++)
            {
                item = inventory[i];
                if (item.Id == Id)
                {
                    inventory.RemoveAt(i);
                    SerializeInventory();
                    break;
                }
            }
        }

        public void EditItem(Inventory inv)
        {
            for (int i = 0; i < GetAll().Count; i++)
            {
                if (inv.Id == inventory[i].Id)
                {
                    inventory[i] = inv;
                    break;
                }
            }
            SerializeInventory();
        }
    }
}
