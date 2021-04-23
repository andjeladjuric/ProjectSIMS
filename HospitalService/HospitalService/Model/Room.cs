using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Room : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private RoomType _type;
        private string _id;
        private string _name;
        private Boolean _isFree;

        public RoomType Type 
        { 
            get { return _type; }
            set
            {
                if(value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public Boolean IsFree
        {
            get { return _isFree; }
            set
            {
                if (value != _isFree)
                {
                    _isFree = value;
                    OnPropertyChanged("IsFree");
                }
            }
        }

        public Dictionary<int, int> inventory { get; set; }
    }
}