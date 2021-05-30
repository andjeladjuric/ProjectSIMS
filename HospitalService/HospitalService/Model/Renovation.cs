﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum RenovationType { Split, Merge }
    public class Renovation
    {
        public string RoomId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RenovationType Type { get; set; }
        public string SecondRoomId { get; set; }
        public string NewRoomId { get; set; }
        public RoomType NewRoomType { get; set; }
        public string NewRoomName { get; set; }
        public double NewSize { get; set; }

        /*merge*/
        public Renovation(string roomId, DateTime start, DateTime end, RenovationType type, string secondRoomId, string newRoomId,
            RoomType newType, string newName)
        {
            RoomId = roomId;
            Start = start;
            End = end;
            Type = type;
            SecondRoomId = secondRoomId;
            NewRoomId = newRoomId;
            NewRoomType = newType;
            NewRoomName = newName;
        }

        /*split*/
        public Renovation(string roomId, DateTime start, DateTime end, RenovationType type, 
            string newRoomId, RoomType newRoomType, string newRoomName, double newSize)
        {
            RoomId = roomId;
            Start = start;
            End = end;
            Type = type;
            NewRoomId = newRoomId;
            NewRoomType = newRoomType;
            NewRoomName = newRoomName;
            NewSize = newSize;
        }

        public Renovation() {}

        public Renovation(string roomId, DateTime start, DateTime end)
        {
            RoomId = roomId;
            Start = start;
            End = end;
        }
    }
}
