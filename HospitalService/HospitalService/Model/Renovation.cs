using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Renovation
    {
        public string RoomId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Renovation()
        {
        }

        public Renovation(string roomId, DateTime start, DateTime end)
        {
            RoomId = roomId;
            Start = start;
            End = end;
        }
    }
}
