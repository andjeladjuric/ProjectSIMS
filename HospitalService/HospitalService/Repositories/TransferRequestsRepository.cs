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
    }
}
