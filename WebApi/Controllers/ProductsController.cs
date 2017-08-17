using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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
        public IHttpActionResult Get()
        {
            try
            {
                using(var uow = new UnitOfWork())
                {
                    try
                    {
                        var entities = uow.ProductsRepo.GetAll();
                        List<SimpleProductModel> models = new List<SimpleProductModel>();
                        foreach(var entity in entities)
                        {
                            models.Add(ProductMapper.EntityToSimpleModel(entity));
                        }
                        return this.Ok(models);
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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Delete(int Id)
        {
            try
            {
                using(var uow = new UnitOfWork())
                {
                    try
                    {
                        if (ModelState.IsValid) {
                            var p=uow.ProductsRepo.Get(Id);
                            uow.ProductsRepo.Delete(p);
                            return this.StatusCode(HttpStatusCode.NoContent);

                        }
                            
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

        [HttpGet]
        [Route ("api/products/{search}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetAll(String search)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        var entities = uow.ProductsRepo.GetAll(search);
                        List<SimpleProductModel> models = new List<SimpleProductModel>();
                        foreach (var entity in entities)
                        {
                            models.Add(ProductMapper.EntityToSimpleModel(entity));
                        }
                        return this.Ok(models);
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
