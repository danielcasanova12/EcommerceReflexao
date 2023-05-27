using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercenew.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }

}
