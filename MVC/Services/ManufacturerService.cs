using MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Services
{
    public class ManufacturerService
    {
        readonly string manufacturerUri = "http://localhost:51306/api/manufacturermodels";

        public async Task<IList<ManufacturerModel>> GetAll()
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
    }
}