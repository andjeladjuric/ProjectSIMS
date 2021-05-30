using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class RoomsRepository
    {
        private string FileLocation = @"..\..\..\Data\rooms.json";
        private List<Room> rooms;

        public RoomsRepository()
        {
            rooms = new List<Room>();
            rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(FileLocation));
        }

        public void SerializeRooms()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
        }

        public List<Room> GetAll()
        {
            return rooms;
        }

        public void Save(Room newRoom)
        {
            rooms.Add(newRoom);
            SerializeRooms();
        }

        public void Delete(string roomId)
        {
            Room r;
            InventoryFileStorage invStorage = new InventoryFileStorage();
            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Id.Equals(roomId))
                {
                    rooms.RemoveAt(i);
                    SerializeRooms();
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
                    SerializeRooms();
                    break;
                }
            }
        }

        public Room GetOne(string id)
        {
            List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(@"..\..\..\Data\rooms.json"));
            return rooms.Find(x => x.Id == id);
        }

        public List<Room> GetByType(RoomType type)
        {
            List<Room> roomsByType = new List<Room>();
            Room r;

            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Type == type)
                    roomsByType.Add(r);
            }

            return roomsByType;
        }

        public Room FindRoomByPriority()
        {
            if (rooms.Count != 0)
            {
                foreach (Room room in rooms)
                {
                    if (room.Type == RoomType.StorageRoom)
                    {
                        return room;
                    }
                }

                foreach (Room room in rooms)
                {
                    if (room.Type == RoomType.ExaminationRoom || room.Type == RoomType.OperatingRoom)
                    {
                        return room;
                    }
                }

                foreach (Room room in rooms)
                {
                    if (room.Type == RoomType.PatientRoom)
                    {
                        return room;
                    }
                }
            }

            return null;
        }

        public void UpdateRoom(Room room)
        {
            for (int i = 0; i < GetAll().Count; i++)
            {
                if (room.Id == rooms[i].Id)
                {
                    rooms[i] = room;
                    break;
                }
            }
            SerializeRooms();
        }
    }
}
