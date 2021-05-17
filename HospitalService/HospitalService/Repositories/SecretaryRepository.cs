using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class SecretaryRepository
    {
        private String FileLocation = @"..\..\..\Data\secretaries.json";
        private List<Secretary> secretaries;

        public SecretaryRepository()
        {
            secretaries = new List<Secretary>();
            secretaries = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(FileLocation));
        }
        public List<Secretary> GetAll()
        {
            return secretaries;
        }

        public Secretary GetOne(String username)
        {
            return secretaries.Find(x => x.Username == username);
        }
    }
}
