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
    public class ProductsService
    {
        readonly string productsUri = "http://localhost:51306/api/products";

        public async Task<IList<SimpleProductModel>> GetAll()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(productsUri)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    JArray array = JArray.Parse(resultContent);
                    List<SimpleProductModel> models = new List<SimpleProductModel>();

                    foreach (JObject o in array.Children<JObject>())
                    {
                        models.Add(JsonConvert.DeserializeObject<SimpleProductModel>(o.ToString()));
                    }

                    return models;
                }
                else
                {
                    throw new Exception("Could not load products!");
                }
            }
        }
    }
}