using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class TransferRequestsService
    {
        RoomInventoryService roomInventoryService;
        TransferRequestsRepository requestsRepository;
        InventoryQuantityService quantityService;

        public TransferRequestsService()
        {
            roomInventoryService = new RoomInventoryService();
            requestsRepository = new TransferRequestsRepository();
            quantityService = new InventoryQuantityService();
        }

        public void ExecuteRequest(MovingRequests request)
        {
            RoomService roomService = new RoomService();
            InventoryService inventoryService = new InventoryService();

            if (roomService.GetOne(request.moveFromThisRoom) != null && roomService.GetOne(request.sendToThisRoom) != null
                && inventoryService.GetOne(request.inventoryId) != null)

            {
                RoomInventory moveFromHere = roomInventoryService.GetRoomInventoryByIds(request.moveFromThisRoom, request.inventoryId);
                RoomInventory sendHere = roomInventoryService.GetRoomInventoryByIds(request.sendToThisRoom, request.inventoryId);

                if (moveFromHere.Quantity == request.quantity)
                {
                    RemoveItem(moveFromHere);
                }
                else
                {
                    moveFromHere.Quantity -= request.quantity;
                }

                quantityService.EnlargeQuantityInSelectedRoom(sendHere, request);

                roomInventoryService.SerializeRoomInventory();
                requestsRepository.Delete(request);
            }
        }

        private void RemoveItem(RoomInventory moveFromHere)
        {
            RoomInventory item;
            for (int i = 0; i < roomInventoryService.GetAll().Count; i++)
            {
                item = roomInventoryService.GetAll()[i];
                if (item.Equals(moveFromHere))
                {
                    roomInventoryService.GetAll().RemoveAt(i);
                    break;
                }
            }
        }

        public void CheckRequests()
        {
            if (requestsRepository.GetAll().Count != 0)
            {
                MovingRequests request;
                for (int i = 0; i < requestsRepository.GetAll().Count; i++)
                {
                    request = requestsRepository.GetAll()[i];
                    if (DateTime.Compare(request.movingTime, DateTime.Now) <= 0)
                    {
                        ExecuteRequest(request);
                    }
                }
            }
        }

        public void AddNewRequest(MovingRequests movingRequest)
        {
            requestsRepository.GetAll().Add(movingRequest);
            requestsRepository.SerializeTransferRequests();
            CheckRequests();
        }

        public List<MovingRequests> GetAll() => requestsRepository.GetAll();
    }
}
