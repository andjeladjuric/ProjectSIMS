using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class RoomFileStorage
    {
        private String FileLocation = @"..\..\..\Data\rooms.json";
        private List<Room> rooms;

        public RoomFileStorage()
        {
            rooms = new List<Room>();
            rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(FileLocation));
        }
        public List<Room> GetAll()
        {
            return rooms;
        }

        public void Save(Room newRoom)
        {
            rooms.Add(newRoom);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
        }

        public void Delete(string roomId)
        {
            Room r;
            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Id.Equals(roomId))
                {
                    rooms.RemoveAt(i);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
                    break;
                }
            }
        }

        public void Edit(String id, String name, RoomType type)
        {
            Room r;
            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Id.Equals(id))
                {
                    r.Name = name;
                    r.Type = type;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
                    break;
                }
            }
        }


    }
}