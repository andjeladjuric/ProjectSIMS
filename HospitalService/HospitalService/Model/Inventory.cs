
using System;
using System.ComponentModel;

namespace Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Equipment EquipmentType { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }

        public Inventory()
        {
        }

        public Inventory(int id, string name, Equipment equipmentType, int quantity, string supplier)
        {
            this.Id = id;
            this.Name = name;
            this.EquipmentType = equipmentType;
            this.Quantity = quantity;
            this.Supplier = supplier;
        }
    }
}