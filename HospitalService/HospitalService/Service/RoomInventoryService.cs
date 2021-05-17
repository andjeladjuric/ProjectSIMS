using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class RoomInventoryService
    {
        RoomInventoryRepository roomInventoryRepository;

        public RoomInventoryService()
        {
            roomInventoryRepository = new RoomInventoryRepository();
        }
    }
}
