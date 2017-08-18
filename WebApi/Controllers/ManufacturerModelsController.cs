using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using WebApi.Models;
using DAL.Repositories;
using WebApi.Mapping;
using System.Web.Http.Description;

namespace WebApi.Controllers
{
    public class ManufacturerModelsController : ApiController
    {
        
        [HttpGet]
        public IQueryable<ManufacturerModel> GetManufacturers()
        {
            var list = new List<ManufacturerModel>();

            using (var repo = new UnitOfWork())
            {

               foreach(var m in repo.ManufacturerRepo.GetAll())
                {
                    list.Add(ManufacturerMapper.EntityToModel(m));
                }

                return list.AsQueryable();
            }


        }

        [HttpGet]
        
        public IHttpActionResult GetManufacturerModel(int id)
        {
            using(var repo=new UnitOfWork())
            {
                var man = repo.ManufacturerRepo.Get(id);
                if (man == null)
                {
                    return NotFound();

                }

                else
                {
                    return Ok(ManufacturerMapper.EntityToModel(man));
                }
            }
        }

        [HttpPut]
        public IHttpActionResult PutManufacturerModel(int id, [FromBody] ManufacturerModel model)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    var u = uow.ManufacturerRepo.Update(id, ManufacturerMapper.ModelToEntity(model));
                    return Ok(ManufacturerMapper.EntityToModel(u));
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpPost]
        public IHttpActionResult PostManufacturerModel(ManufacturerModel manufacturerModel)
        {
            try
            {
                using(var repo = new UnitOfWork())
                {
                    try
                    {
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                        var m=repo.ManufacturerRepo.Add(ManufacturerMapper.ModelToEntity(manufacturerModel));
                        return CreatedAtRoute("DefaultApi", new { id = m.ID }, ManufacturerMapper.EntityToModel(m));
                    }
                    catch
                    {
                        return BadRequest();
                    }

                }
            }
            catch(Exception e)
            {
                return this.InternalServerError(e);
            }

        }

        [HttpDelete]
        public IHttpActionResult DeleteHomeworkClass(int id)
        {
            try
            {
                using (var repo = new UnitOfWork())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        { 
                        var m = repo.ManufacturerRepo.Get(id);
                        repo.ManufacturerRepo.Delete(m);
                       return this.StatusCode(HttpStatusCode.NoContent);
                        }
                        else return this.BadRequest();

                    }
                    catch (KeyNotFoundException e)
                    {
                        return this.BadRequest(e.ToString());
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
