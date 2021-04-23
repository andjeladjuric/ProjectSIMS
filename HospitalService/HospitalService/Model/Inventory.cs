
using System;
using System.ComponentModel;

namespace Model
{
    public class Inventory : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int _itemId;
        private string _itemName;
        private Equipment _itemType;
        private int _quantity;

        public int Id
        {
            get { return _itemId; }
            set
            {
                if (value != _itemId)
                {
                    _itemId = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Name
        {
            get { return _itemName; }
            set
            {
                if (value != _itemName)
                {
                    _itemName = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public Equipment EquipmentType
        {
            get { return _itemType; }
            set
            {
                if (value != _itemType)
                {
                    _itemType = value;
                    OnPropertyChanged("EquipmentType");
                }
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
    }
}