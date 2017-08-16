using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public abstract class IProducts 
    {
        public ICollection<SimpleProductModel> FilteredProducts { get; set; }

        public abstract ICollection<SimpleProductModel> Filter(ICollection<string> param);
    }
}