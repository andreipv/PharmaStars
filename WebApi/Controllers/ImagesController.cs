using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        public async Task<IHttpActionResult> Post()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return this.StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                var filesProvider = await Request.Content.ReadAsMultipartAsync();
                var fileContents = filesProvider.Contents.FirstOrDefault();
                if (fileContents == null)
                {
                    return this.BadRequest("Missing file");
                }

                byte[] image = await fileContents.ReadAsByteArrayAsync();

                MemoryStream ms = new MemoryStream(image);
                Image file = Image.FromStream(ms);
                string path = Path.Combine(
                            HttpContext.Current.Server.MapPath("~/Images/"));

                string imageName = fileContents.Headers.ContentDisposition.FileName + DateTime.Now.Millisecond;
                imageName = imageName.Replace('.', 'a');
                imageName = imageName.Replace('\\', 'a');
                imageName = imageName.Replace('\"', 'a');
                imageName = imageName + ".jpg";
                string imgPath = Path.Combine(path, imageName);

                File.WriteAllBytes(imgPath, image);
                //file.SaveAs(path);

                return this.Ok(new
                {
                    Result = "~/Images/" + imageName,
                });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
