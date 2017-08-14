using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public class Decorator : Products
    {
        public IProducts products;

        public Decorator(IProducts prod)
        {
            this.products = prod;
        }

        public override ICollection<SimpleProductModel> Filter(ICollection<string> param)
        {
            return base.Filter(param);
        }
    }
}