using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.DecoratorFilter;
using WebApi.Mapping;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FilteredProductsController : ApiController
    {
        [AllowAnonymous]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody] FilterModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        IProducts filteredProducts = new Products();

                        if (model == null)
                        {
                            return Ok(filteredProducts.Filter(null));
                        }

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
