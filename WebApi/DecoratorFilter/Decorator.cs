using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public abstract class Decorator : IProducts
    {
        protected IProducts products;

        public Decorator(IProducts prod)
        {
            products = prod;
            this.FilteredProducts = prod.FilteredProducts;
        }

        public override ICollection<SimpleProductModel> Filter(ICollection<string> param)
        {
            return products.Filter(param);
        }
    }
}