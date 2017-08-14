using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public class CategoryFilter : Decorator
    {
        public CategoryFilter(IProducts prod) : base(prod) { }

        public override ICollection<SimpleProductModel> Filter(ICollection<string> param)
        {
            List<SimpleProductModel> categProd = new List<SimpleProductModel>();
            if (param == null) return FilteredProducts;

            foreach (SimpleProductModel prod in FilteredProducts)
                foreach (String category in prod.Categories)
                    foreach(String par in param)
                        if (category.CompareTo(par) == 0)
                            categProd.Add(prod);

            FilteredProducts = categProd;
            return categProd;
        }
    }
}