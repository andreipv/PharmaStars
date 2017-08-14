using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public interface IProducts 
    {
        ICollection<SimpleProductModel> FilteredProducts { get; set; }
        ICollection<SimpleProductModel> Filter(ICollection<string> param);
    }
}