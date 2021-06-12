using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace HospitalService.Repositories
{
    public class RepositoryImplementationJson<T> : IRepository<T>
    {
        public List<T> getAll(string path)
        {
            List<T> items = new List<T>();
            items = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
            return items;
        }

        public void saveAll(List<T> items, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(items,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
        }
    }
}
