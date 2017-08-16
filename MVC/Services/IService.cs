using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public interface IService<T>
    {
        Task<IList<T>> GetAll();
    }
}
