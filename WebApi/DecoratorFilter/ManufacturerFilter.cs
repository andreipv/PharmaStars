using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public class ManufacturerFilter : Decorator
    {
        public ManufacturerFilter(IProducts prod) : base(prod) { }


        public override ICollection<SimpleProductModel> Filter(ICollection<string> param)
        {
            List<SimpleProductModel> manProd = new List<SimpleProductModel>();
            if (param == null) return FilteredProducts;

            foreach (SimpleProductModel prod in FilteredProducts)
                foreach(String manufacturer in param)
                    if (prod.Manufacturer.CompareTo(manufacturer) == 0)
                        manProd.Add(prod);

            FilteredProducts = manProd;
            return manProd;
        }

    }
}