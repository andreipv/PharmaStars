using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public interface IProductService
    {
        Task<IList<SimpleProductModel>> GetAll();

        Task<IList<SimpleProductModel>> GetAll(String search);

        Task<SimpleProductModel> Get(int id);

        Task Post(FullProductModel model);

    }
}
