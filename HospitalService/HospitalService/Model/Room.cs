using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Room
    {
        public RoomType Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public Boolean IsFree { get; set; }

        public Room(RoomType type, string id, string name, bool isFree)
        {
            Type = type;
            Id = id;
            Name = name;
            IsFree = isFree;
        }

        public Room() { }
    }
}