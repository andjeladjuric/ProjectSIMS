using HospitalService.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class RoomFileStorage
    {
        private string FileLocation = @"..\..\..\Data\rooms.json";
        private List<Room> rooms;

        public RoomFileStorage()
        {
            rooms = new List<Room>();
            rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(FileLocation));
        }

        public void SerializeRooms()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
        }

        public List<Room> DeserializeRooms()
        {
            return JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(FileLocation));
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
            for (int i = 0; i < rooms.Count; i++)
            {
                r = rooms[i];
                if (r.Id.Equals(roomId))
                {
                    MoveItemsToStorage(roomId);
                    DeleteRequests(roomId);
                    rooms.RemoveAt(i);
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(rooms));
                    break;
                }
            }
        }

        private void DeleteRequests(string roomId)
        {
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            MovingRequests mr;
            for (int i = 0; i < requests.Count; i++)
            {
                mr = requests[i];
                if (roomId.Equals(mr.moveFromThisRoom) || roomId.Equals(mr.sendToThisRoom))
                {
                    requests.RemoveAt(i);
                    File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                    continue;
                }
            }
        }

        private void MoveItemsToStorage(string roomId)
        {
            RoomInventoryStorage roomInv = new RoomInventoryStorage();
            for (int j = 0; j < roomInv.GetAll().Count; j++)
            {
                RoomInventory ri = roomInv.GetAll()[j];
                if (ri.RoomId.Equals(roomId))
                {
                    foreach (Room soba in getByType(RoomType.StorageRoom))
                    {
                        RoomInventory nova = roomInv.GetRoomInventoryByIds(soba.Id, ri.ItemId);
                        if (nova == null)
                        {
                            roomInv.GetAll().Add(new RoomInventory(soba.Id, ri.ItemId, ri.Quantity));
                            break;
                        }
                        else
                        {
                            nova.Quantity += ri.Quantity;
                            break;
                        }
                    }

                    roomInv.GetAll().RemoveAt(j);
                    File.WriteAllText(@"..\..\..\Data\roomInventory.json", JsonConvert.SerializeObject(roomInv.GetAll()));
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

        public static Room getOne(string id)
        {
            List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(@"..\..\..\Data\rooms.json"));
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