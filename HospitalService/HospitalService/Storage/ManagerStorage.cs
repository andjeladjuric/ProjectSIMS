using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Storage
{
    class ManagerStorage
    {
        private String FileLocation = @"..\..\..\Data\managers.json";
        private List<Manager> managers;

        public ManagerStorage()
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
