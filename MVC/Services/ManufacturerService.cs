using MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Services
{
    public class ManufacturerService : IService<ManufacturerModel>
    {
        readonly string manufacturerUri = "http://localhost:51306/api/manufacturermodels/";

        public async Task<IEnumerable<ManufacturerModel>> GetAll()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(manufacturerUri)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    JArray array = JArray.Parse(resultContent);
                    List<ManufacturerModel> models = new List<ManufacturerModel>();

                    foreach (JObject o in array.Children<JObject>())
                    {
                        CategoryModel model = new CategoryModel();
                        models.Add(JsonConvert.DeserializeObject<ManufacturerModel>(o.ToString()));
                    }

                    return models;
                }
                else
                {
                    throw new Exception("Could not load manufacturers!");
                }
            }
        }

        public async Task<ManufacturerModel> Get(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(manufacturerUri + id)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(resultContent);

                    ManufacturerModel model = new ManufacturerModel();
                    model = JsonConvert.DeserializeObject<ManufacturerModel>(obj.ToString());

                    return model;
                }
                else
                {
                    throw new Exception("Could not load categories!");
                }
            }
        }

        public async Task Put(int id, ManufacturerModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(manufacturerUri + id),
                    Content = new StringContent(string.Format("ID={0}&Name={1}&Adress={2}", model.ID, model.Name, model.Adress))
                };

                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };

                var result = await httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong");
                }
            }
        }
    }
}