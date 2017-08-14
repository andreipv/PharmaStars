using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi.Mapping;
using WebApi.Models;

namespace WebApi.DecoratorFilter
{
    public class Products : IProducts
    {

        public Products()
        {
            if (FilteredProducts == null)
            {
                FilteredProducts = new List<SimpleProductModel>();

                using (var uow = new UnitOfWork())
                {

                    foreach (Product prod in uow.ProductsRepo.GetAll())
                        FilteredProducts.Add(ProductMapper.EntityToSimpleModel(prod));
                }
            }
        }

        public override ICollection<SimpleProductModel> Filter(ICollection<string> param)
        {
            return FilteredProducts;
        }
    }
}