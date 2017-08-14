using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DecoratorFilter;
using WebApi.Mapping;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FilteredProductsController : ApiController
    {
        [AllowAnonymous]
        public IHttpActionResult Post([FromBody] FilterModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        var entities = uow.ProductsRepo.GetAll();
                        List<SimpleProductModel> models = new List<SimpleProductModel>();
                        foreach (var entity in entities)
                        {
                            models.Add(ProductMapper.EntityToSimpleModel(entity));
                        }

                        Products filteredProducts = new Products();
                        if (model.CategoryFilters != null)
                        {
                            filteredProducts = new CategoryFilter(filteredProducts);
                            filteredProducts.Filter(model.CategoryFilters);
                        }

                        if (model.ManufacturerFilters != null)
                        {
                            filteredProducts = new ManufacturerFilter(filteredProducts);
                            filteredProducts.Filter(model.ManufacturerFilters);
                        }
                        
                        return this.Ok(filteredProducts.Filter(null));
                    }
                    catch (Exception e)
                    {
                        return this.BadRequest(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }
    }
}
