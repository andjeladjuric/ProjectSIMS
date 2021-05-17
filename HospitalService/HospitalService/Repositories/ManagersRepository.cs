using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class ManagersRepository
    {
        private String FileLocation = @"..\..\..\Data\managers.json";
        private List<Manager> managers;

        public ManagersRepository()
        {
            managers = new List<Manager>();
            managers = JsonConvert.DeserializeObject<List<Manager>>(File.ReadAllText(FileLocation));
        }
        public List<Manager> GetAll()
        {
            return managers;
        }

        public Manager GetOne(String username)
        {
            return managers.Find(x => x.Username == username);
        }
    }
}

