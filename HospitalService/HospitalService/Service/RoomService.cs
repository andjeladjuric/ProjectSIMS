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
            DeleteMovingRequests(roomId);
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

        #endregion
    }
}
