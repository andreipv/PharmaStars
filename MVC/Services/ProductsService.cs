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
    public class ProductsService : IProductService
    {
        readonly string productsUri = "http://localhost:51306/api/products/";

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

        public async Task<IList<SimpleProductModel>> GetAll(String search)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(productsUri + search),
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

        public async Task<SimpleProductModel> Get(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(productsUri + id)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(resultContent);

                    SimpleProductModel model = new SimpleProductModel();
                    model = JsonConvert.DeserializeObject<SimpleProductModel>(obj.ToString());

                    return model;
                }
                else
                {
                    throw new Exception("Could not load categories!");
                }
            }
        }

        public async Task Post(FullProductModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(productsUri),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json") { CharSet = "UTF-8" };

                var result = await httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong");
                }
            }
        }
    }
}