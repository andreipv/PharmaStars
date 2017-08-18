using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebApi.Mapping;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ImagesController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    SimpleProductModel model = ProductMapper.EntityToSimpleModel(uow.ProductsRepo.Get(id));
                    byte[] bytes = System.IO.File
                        .ReadAllBytes(
                          HttpContext.Current.Server.MapPath(model.ImgPath));
                    var result_ = new HttpResponseMessage(HttpStatusCode.OK);
                    result_.Content = new ByteArrayContent(bytes);
                    result_.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                    return result_;
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
