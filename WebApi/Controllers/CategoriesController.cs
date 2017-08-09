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
    
    public class CategoriesController : ApiController
    { 
    public IHttpActionResult Get()
    {
        try
        {
            using(var uow = new UnitOfWork())
            {
                List<CategoriesModel> models = new List<CategoriesModel>();
                var entities = uow.CategoriesRepo.GetAll();
                foreach(var entity in entities)
                {
                    var model = CategoriesMapper.EntityToModel(entity);
                    models.Add(model);
                }
                return this.Ok(models);
            }
           
        }
        catch (Exception ex)
        {
            //to do log this
            return this.InternalServerError();
        }
    }

    [HttpGet]
    public IHttpActionResult Get(int id)
    {
        try
        {
                using (var uow = new UnitOfWork())
                {
                    var cat = uow.CategoriesRepo.Get(id);
                    
                    if (cat == null)
                    {
                        return this.NotFound();
                    }

                    return this.Ok(CategoriesMapper.EntityToModel(cat));
                }
        }
        catch (Exception ex)
        {
            //to do log this
            return this.InternalServerError();
        }
    }

    [HttpPost]
    public IHttpActionResult Post([FromBody] CategoriesModel category)
    {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        uow.CategoriesRepo.Add(CategoriesMapper.ModelToEntity(category));
                        return this.Ok(CategoriesMapper.ModelToEntity(category));
                    }
                    catch (Exception e)
                    {
                        return this.BadRequest(e.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //to do log this
                return this.InternalServerError();
            }
    }

    [HttpPut]
    public IHttpActionResult Put(int id, CategoriesModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    return this.Ok(uow.CategoriesRepo.Update(id, CategoriesMapper.ModelToEntity(model)));
                  
                }
            }
            catch (Exception ex)
            {
                //to do log this
                return this.InternalServerError();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(CategoriesModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.CategoriesRepo.Delete(CategoriesMapper.ModelToEntity(model));
                    return this.Ok();
                }
            }
            catch (Exception ex)
            {
                //to do log this
                return this.InternalServerError(ex);
            }
        }
    }
}

