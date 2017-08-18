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
        [Route ("api/Products/Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        return this.Ok(ProductMapper.EntityToSimpleModel(uow.ProductsRepo.Get(id)));
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
        public IHttpActionResult Post(FullProductModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    if (ModelState.IsValid)
                    {
                        var entity = ProductMapper.FullModelToEntity(model);
                        entity.ID_MRF = model.IDManufacturer;

                        if (model.Categories != null)
                        {
                            entity.Categories = new List<Category>();
                            foreach (var id in model.Categories)
                            {
                                entity.Categories.Add(uow.CategoriesRepo.Get(id));
                            }
                        }

                        return this.Ok(ProductMapper.EntityToModel
                            (uow.ProductsRepo.Add(entity)));
                    }
                    else
                        return this.BadRequest();
                }
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(FullProductModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    if (ModelState.IsValid)
                    {
                        var entity = ProductMapper.FullModelToEntity(model);
                        entity.Manufacturer = uow.ManufacturerRepo.Get(model.IDManufacturer);

                        entity.Categories = new List<Category>();
                        foreach (var id in model.Categories)
                        {
                            entity.Categories.Add(uow.CategoriesRepo.Get(id));
                        }
                        return this.Ok(ProductMapper.EntityToModel
                            (uow.ProductsRepo.Update(entity.ID, entity)));
                    }
                    else
                        return this.BadRequest();
                }
            }
            catch (Exception e)
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

        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
