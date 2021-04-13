using System;
using System.Collections.Generic;

namespace Model
{
    public class Room
    {
        public RoomType Type { get; set; }
        public String Id { get; set; }
        public String Name { get; set; }
        public Boolean IsFree { get; set; }

        public Dictionary<int, int> inventory { get; set; }

        /*public System.Collections.ArrayList GetInventory()
        {
            if (inventory == null)
                inventory = new System.Collections.ArrayList();
            return inventory;
        }

        public void SetInventory(System.Collections.ArrayList newInventory)
        {
            RemoveAllInventory();
            foreach (Inventory oInventory in newInventory)
                AddInventory(oInventory);
        }

        public void AddInventory(Inventory newInventory)
        {
            if (newInventory == null)
                return;
            if (this.inventory == null)
                this.inventory = new System.Collections.ArrayList();
            if (!this.inventory.Contains(newInventory))
                this.inventory.Add(newInventory);
        }

        public void RemoveInventory(Inventory oldInventory)
        {
            if (oldInventory == null)
                return;
            if (this.inventory != null)
                if (this.inventory.Contains(oldInventory))
                    this.inventory.Remove(oldInventory);
        }

        public void RemoveAllInventory()
        {
            if (inventory != null)
                inventory.Clear();
        }*/

    }
}