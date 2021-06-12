using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Repositories
{
   public interface IRepository <T>
    {

        List<T> getAll(String path);
        void saveAll(List<T> items, String path);

    }
}
