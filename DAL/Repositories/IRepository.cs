using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepository<T> where T: class
    {
        T Get(int key);
        T Add(T entity);
        T Delete(T entity);
        T Update(int key, T updated);
        IQueryable<T> GetAll();
    }
}
