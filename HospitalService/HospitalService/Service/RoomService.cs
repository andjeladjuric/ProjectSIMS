using HospitalService.Repositories;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Service
{
    public class RoomService
    {
        #region Fields

        RoomInventoryRepository roomInventoryRepository;
        RoomsRepository roomsRepository;

        #endregion

        #region Constructors
        public RoomService()
        {
            roomInventoryRepository = new RoomInventoryRepository();
            roomsRepository = new RoomsRepository();
        }

        #endregion

        #region Functions

        public void DeleteRoom(string roomId)
        {
            DeleteMovingRequests(roomId);
            DeleteRenovationRequests(roomId);
            MoveItemsToStorage(roomId);
            roomsRepository.Delete(roomId);
        }

        private void DeleteMovingRequests(string roomId)
        {
            List<MovingRequests> requests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
            MovingRequests movingRequest;
            for (int i = 0; i < requests.Count; i++)
            {
                movingRequest = requests[i];
                if (roomId.Equals(movingRequest.moveFromThisRoom) || roomId.Equals(movingRequest.sendToThisRoom))
                {
                    requests.RemoveAt(i);
                    File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(requests));
                    continue;
                }
            }
        }

        private void DeleteRenovationRequests(string roomId)
        {
            RenovationsRepository renovationRepository = new RenovationsRepository();
            Renovation renovationRequest;
            for (int i = 0; i < renovationRepository.GetAll().Count; i++)
            {
                renovationRequest = renovationRepository.GetAll()[i];
                if (roomId.Equals(renovationRequest.RoomId))
                {
                    renovationRepository.GetAll().RemoveAt(i);
                    renovationRepository.SerializeRenovations();
                    continue;
                }
            }

        }

        private void MoveItemsToStorage(string roomId)
        {
            for (int j = 0; j < roomInventoryRepository.GetAll().Count; j++)
            {
                RoomInventory ri = roomInventoryRepository.GetAll()[j];
                if (ri.RoomId.Equals(roomId))
                {
                    foreach (Room soba in roomsRepository.GetByType(RoomType.StorageRoom))
                    {
                        RoomInventory inventoryById = roomInventoryRepository.GetRoomInventoryByIds(soba.Id, ri.ItemId);
                        if (inventoryById == null)
                        {
                            roomInventoryRepository.GetAll().Add(new RoomInventory(soba.Id, ri.ItemId, ri.Quantity));
                            break;
                        }
                        else
                        {
                            inventoryById.Quantity += ri.Quantity;
                            break;
                        }
                    }

                    roomInventoryRepository.GetAll().RemoveAt(j);
                    roomInventoryRepository.SerializeRoomInventory();
                }
            }
        }


        public Room getFirstAvailableRoom(DateTime startTime, DateTime endTime)
        {
            roomsRepository = new RoomsRepository();
            List<Room> rooms = roomsRepository.GetAll();
            bool isFindAvailableRoom = false;
            Room availableRoom = new Room();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (isCurrentRoomAvailable(startTime, endTime, rooms[i]))
                {

                    availableRoom = rooms[i];
                    isFindAvailableRoom = true;
                    break;
                }
            }
            if (isFindAvailableRoom == false)
            {
                return null;
            }
            return availableRoom;
        }
        public bool isCurrentRoomAvailable(DateTime startTime, DateTime endTime, Room examinedRoom)
        {
            AppointmentsService appointmentsService = new AppointmentsService();
            List<Appointment> appointments = appointmentsService.GetAll();
            for (int j = 0; j < appointments.Count; j++)
            {
                if ((DateTime.Compare(appointments[j].StartTime, startTime) == 0 || DateTime.Compare(appointments[j].EndTime, endTime) == 0) && appointments[j].room.Id.Equals(examinedRoom.Id) && appointments[j].Status != Status.Canceled)
                {
                    return false;
                }
                else if (startTime >= appointments[j].StartTime && startTime < appointments[j].EndTime && appointments[j].room.Id.Equals(examinedRoom.Id) && appointments[j].Status != Status.Canceled)
                {
                    return false;
                }
                else if (endTime >= appointments[j].StartTime && endTime < appointments[j].EndTime && appointments[j].room.Id.Equals(examinedRoom.Id) && appointments[j].Status != Status.Canceled)
                {
                    return false;
                }
            }
            return true;
        }


        public RoomType GetRoomType(AppointmentType appointmentType)
        {
            RoomType roomType;
            if (appointmentType == AppointmentType.Pregled)
                roomType = RoomType.ExaminationRoom;
            else
                roomType = RoomType.OperatingRoom;
            return roomType;
        }

        public List<Room> GetAll() => roomsRepository.GetAll();
        public Room GetOne(string Id) => roomsRepository.GetOne(Id);
        public List<Room> GetByType(RoomType Type) => roomsRepository.GetByType(Type);
        public void AddRoom(Room newRoom) => roomsRepository.Save(newRoom);
        public void Edit(String id, String name, RoomType type, Boolean free) => roomsRepository.Edit(id, name, type, free);
        public void UpdateRoom(Room room) => roomsRepository.UpdateRoom(room);

        #endregion
    }
}
