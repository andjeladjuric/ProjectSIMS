using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    public class TransferRequestsRepository
    {
        public List<MovingRequests> movingRequests { get; set; }
        public TransferRequestsRepository()
        {
            movingRequests = new List<MovingRequests>();
            movingRequests = JsonConvert.DeserializeObject<List<MovingRequests>>(File.ReadAllText(@"..\..\..\Data\requests.json"));
        }

        public List<MovingRequests> GetAll()
        {
            return movingRequests;
        }
        public void SerializeTransferRequests()
        {
            File.WriteAllText(@"..\..\..\Data\requests.json", JsonConvert.SerializeObject(movingRequests));
        }

        public MovingRequests GetOne(int itemId)
        {
            return movingRequests.Find(x => x.inventoryId == itemId);
        }

        public void Delete(MovingRequests mr)
        {
            for (int i = 0; i < movingRequests.Count; i++)
            {
                if (movingRequests[i].movingTime == mr.movingTime)
                {
                    movingRequests.RemoveAt(i);
                }
            }

            SerializeTransferRequests();
        }

        public void DeleteByRoomId(string roomId)
        {
            MovingRequests movingRequest;
            for (int i = 0; i < movingRequests.Count; i++)
            {
                movingRequest = movingRequests[i];
                if (roomId.Equals(movingRequest.moveFromThisRoom) || roomId.Equals(movingRequest.sendToThisRoom))
                {
                    movingRequests.RemoveAt(i);
                    SerializeTransferRequests();
                    continue;
                }
            }
        }
    }
}
