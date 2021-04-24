
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
    }
}