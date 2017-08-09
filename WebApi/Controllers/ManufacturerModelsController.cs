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

        [ResponseType(typeof(void))]

        [System.Web.Http.HttpPut]
        public IHttpActionResult PutManufacturerModel(int id, [FromBody] ManufacturerModel manufacturerModel)
        {
            try {

                using (var repo = new UnitOfWork())
                {
                    try { 

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    repo.ManufacturerRepo.Update(id, ManufacturerMapper.ModelToEntity(manufacturerModel));
                    return StatusCode(HttpStatusCode.NoContent);
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
                        var m = repo.ManufacturerRepo.Get(id);
                        repo.ManufacturerRepo.Delete(m);
                        return Ok();
                    }
                    catch
                    {
                        return BadRequest();
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
