using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    interface IRepository<T>
    {
        T Get(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}
