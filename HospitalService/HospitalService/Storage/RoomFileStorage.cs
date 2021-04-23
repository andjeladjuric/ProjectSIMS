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
            InventoryFileStorage invStorage = new InventoryFileStorage();
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Id.Equals(roomId))
                {
                    foreach(int k in r.inventory.Keys)
                    {
                        foreach (Room soba in getByType(RoomType.StorageRoom))
                        {
                            if (soba.inventory.ContainsKey(k))
                            {
                                soba.inventory[k] += r.inventory[k];
                                break;
                            }
                            else
                            {
                                soba.inventory.Add(k, r.inventory[k]);
                                break;
                            }
                        }
                    }

                    MovingRequests mr;
                    for (int m = 0; m < requests.Count; m++)
                    {
                        mr = requests[m];
                        if (r.Id.Equals(mr.moveFromThisRoom))
                        {
                            requests.RemoveAt(m);
                            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                            continue;
                        }
                        else if (r.Id.Equals(mr.sendToThisRoom))
                        {
                            requests.RemoveAt(m);
                            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                            continue;
                        }
                    }

                    rooms.RemoveAt(i);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
                    break;
                }
            }
        }

        public void Edit(String id, String name, RoomType type, Boolean free)
        {
            Room r;
            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Id.Equals(id))
                {
                    r.Name = name;
                    r.Type = type;
                    r.IsFree = free;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
                    break;
                }
            }
        }

        public Room getOne(string id)
        {
            return rooms.Find(x => x.Id == id);
        }

        public List<Room> getByType(RoomType type)
        {
            List<Room> listByType = new List<Room>();
            Room r;

            for( int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Type == type)
                    listByType.Add(r);
            }

            return listByType;

        }

        public void editRoom(Room r)
        {
            for(int i = 0; i < GetAll().Count; i++)
            {
                if(r.Id == rooms[i].Id)
                {
                    rooms[i] = r;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
        }

    }
}