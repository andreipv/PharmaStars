using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Mapping;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        return this.Ok(ProductMapper.EntityToModel(uow.ProductsRepo.Get(id)));
                    }catch(Exception e)
                    {
                        return this.BadRequest(e.ToString());
                    }
                   
                }
            }catch(Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        [HttpGet]
        public IQueryable<ProductsModel> Get()
        {
            var list = new List<ProductsModel>();

            using(var uow = new UnitOfWork())
            {
                foreach (var m in uow.ProductsRepo.GetAll())
                    list.Add(ProductMapper.EntityToModel(m));

                return list.AsQueryable();
            }
        }


        [HttpGet]
        public IQueryable<ProductsModel> GetAll(double minPrice, double maxPrice)
        {
            var list = new List<ProductsModel>();
            using(var uow = new UnitOfWork())
            {
                foreach (var m in uow.ProductsRepo.GetAll(minPrice, maxPrice))
                    list.Add(ProductMapper.EntityToModel(m));

                return list.AsQueryable();
            }
        }

        [HttpGet]
        public IQueryable<ProductsModel> GetAll(CategoriesModel category)
        {
            var list = new List<ProductsModel>();
            using (var uow = new UnitOfWork())
            {
                foreach (var m in uow.ProductsRepo.GetAll(CategoriesMapper.ModelToEntity(category)))
                    list.Add(ProductMapper.EntityToModel(m));

                return list.AsQueryable();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(ProductsModel model)
        {
            try
            {
                using(var uow = new UnitOfWork())
                {
                    if (ModelState.IsValid)
                        return this.Ok(ProductMapper.EntityToModel
                            (uow.ProductsRepo.Add(ProductMapper.ModelToEntity(model))));
                    else
                        return this.BadRequest();
                }
            }catch(Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, ProductsModel model)
        {
            try
            {
                using(var uow = new UnitOfWork())
                {
                    try
                    {
                        if (ModelState.IsValid)
                            return this.Ok(ProductMapper.EntityToModel
                                (uow.ProductsRepo.Update(id, ProductMapper.ModelToEntity(model))));
                        return this.BadRequest();

                    }catch(KeyNotFoundException e)
                    {
                        return this.BadRequest(e.ToString());
                    }
                    catch(Exception e)
                    {
                        return this.BadRequest(e.ToString());
                    }
                }

            }catch(Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(ProductsModel model)
        {
            try
            {
                using(var uow = new UnitOfWork())
                {
                    try
                    {
                        if (ModelState.IsValid)
                            return this.StatusCode(HttpStatusCode.NoContent);
                        return this.BadRequest();

                    }catch(KeyNotFoundException e)
                    {
                        return this.BadRequest(e.ToString());
                    }catch(Exception e)
                    {
                        return this.BadRequest(e.ToString());
                    }
                }
            }catch(Exception e)
            {
                return this.InternalServerError(e);
            }
        }

    }
}
