using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);
        Task Put(int id, T model);
    }
}
