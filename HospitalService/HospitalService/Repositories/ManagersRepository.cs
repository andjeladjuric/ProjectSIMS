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

        public void SerializeManagers()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(managers));
        }

        public List<Manager> GetAll()
        {
            return managers;
        }

        public Manager GetOne(String username)
        {
            return managers.Find(x => x.Username == username);
        }

        public void EditManager(Manager editedManager)
        {
            Manager manager;
            for (int i = 0; i < managers.Count; i++)
            {
               manager = managers[i];
                if (manager.Jmbg.Equals(editedManager.Jmbg))
                {
                    managers[i] = editedManager;
                    SerializeManagers();
                    break;
                }
            }
        }
    }
}

