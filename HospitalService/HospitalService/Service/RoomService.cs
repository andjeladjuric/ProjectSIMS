using HospitalService.Repositories;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace HospitalService.Service
{
    public class RoomService
    {
        #region Fields

        RoomInventoryRepository roomInventoryRepository;
        RoomsRepository roomsRepository;
        TransferRequestsService requestsService;

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
            requestsService = new TransferRequestsService();
            RoomRenovationService renovationService = new RoomRenovationService();
            if (CheckTakenBeds(roomId))
            {
                requestsService.DeleteByRoomId(roomId);
                renovationService.Delete(roomId);
                MoveItemsToStorage(roomId);
                roomsRepository.Delete(roomId);
            }
            else
            {
                MessageBox.Show("Soba je zauzeta!");
            }
        }

        private bool CheckTakenBeds(string roomId)
        {
            RoomInventoryService service = new RoomInventoryService();
            int takenBeds = new MedicalRecordService().TakenBeds(roomId);
            if (takenBeds > 0)
            {
                return false;
            }

            return true;
        }

        public void ChangeRoomAvailability(string roomId, bool IsAvailable)
        {
            RoomService roomService = new RoomService();
            Room room = roomService.GetOne(roomId);
            room.IsFree = IsAvailable;
            roomService.UpdateRoom(room);
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
                if (!new ScheduleService().IsRoomTaken(startTime, endTime, rooms[i]))
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
        public void Edit(String id, String name, RoomType type) => roomsRepository.Edit(id, name, type);
        public void UpdateRoom(Room room) => roomsRepository.UpdateRoom(room);
        public void SerializeRooms() => roomsRepository.SerializeRooms();
        public Room FindRoomByPriority() => roomsRepository.FindRoomByPriority();

        #endregion
    }
}
