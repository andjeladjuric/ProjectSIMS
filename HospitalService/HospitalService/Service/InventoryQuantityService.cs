using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalService.Service
{
    public class InventoryQuantityService
    {
        #region Fields
        private RoomService roomService;
        private InventoryService inventoryService;
        private RoomInventoryService roomInventoryService;
        #endregion

        #region Constructors
        public InventoryQuantityService()
        {
            roomService = new RoomService();
        }
        #endregion

        public void RemoveUsedItems(int enteredQuantity, Inventory SelectedItem, Room SelectedRoom)
        {
            inventoryService = new InventoryService();
            Inventory item = inventoryService.GetOne(SelectedItem.Id);
            RoomInventory inventoryInRoom = roomInventoryService.GetRoomInventoryByIds(SelectedRoom.Id, SelectedItem.Id);
            ReduceItemQuantity(enteredQuantity, item);
            ReduceQuantityInSelectedRoom(enteredQuantity, inventoryInRoom);
        }

        private void ReduceItemQuantity(int enteredQuantity, Inventory item)
        {
            if (item.Quantity == enteredQuantity)
            {
                inventoryService.GetAll().Remove(item);
            }
            else
            {
                item.Quantity -= enteredQuantity;
            }

            inventoryService.EditItem(item);
        }

        public void ReduceQuantityInSelectedRoom(int enteredQuantity, RoomInventory inventoryInRoom)
        {
            roomInventoryService = new RoomInventoryService();
            if (inventoryInRoom.Quantity == enteredQuantity)
            {
                RemoveItem(inventoryInRoom);
            }
            else
            {
                inventoryInRoom.Quantity -= enteredQuantity;
            }

            roomInventoryService.EditItem(inventoryInRoom);
        }

        private void RemoveItem(RoomInventory itemInRoom)
        {
            RoomInventory item;
            roomInventoryService = new RoomInventoryService();
            for (int i = 0; i < roomInventoryService.GetAll().Count; i++)
            {
                item = roomInventoryService.GetAll()[i];
                if (item.Equals(itemInRoom))
                {
                    roomInventoryService.GetAll().RemoveAt(i);
                    break;
                }
            }
        }

        public void EnlargeQuantityInSelectedRoom(RoomInventory inventoryInRoom, MovingRequests request)
        {
            roomInventoryService = new RoomInventoryService();
            if (inventoryInRoom == null)
            {
                roomInventoryService.GetAll().Add(new RoomInventory(request.sendToThisRoom, request.inventoryId, request.quantity));
            }
            else
            {
                inventoryInRoom.Quantity += request.quantity;
            }

            roomInventoryService.SerializeRoomInventory();
        }

        public void EnlargeExistingQuantity(Inventory item, int quantity)
        {
            int temp;
            roomInventoryService = new RoomInventoryService();

            if (roomService.GetByType(RoomType.StorageRoom).Count == 0)
            {
                MessageBox.Show("Količina opreme se može povećati samo unutar skladišta!");
            }
            else
            {
                if (item.Quantity < quantity)
                {
                    temp = quantity - item.Quantity;

                    foreach (Room room in roomService.GetByType(RoomType.StorageRoom))
                    {
                        RoomInventory r = roomInventoryService.GetRoomInventoryByIds(room.Id, item.Id);
                        r.Quantity += temp;
                        item.Quantity += quantity;
                        roomInventoryService.SerializeRoomInventory();
                        return;
                    }
                }
            }
        }
    }
}
