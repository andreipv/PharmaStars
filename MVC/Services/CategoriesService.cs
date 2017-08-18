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
    public class CategoriesService : IService<CategoryModel>
    {
        readonly string categoriesUri = "http://localhost:51306/api/categories/";

        public async Task<IEnumerable<CategoryModel>> GetAll()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(categoriesUri)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    JArray array = JArray.Parse(resultContent);
                    List<CategoryModel> models = new List<CategoryModel>();

                    foreach (JObject o in array.Children<JObject>())
                    {
                        CategoryModel model = new CategoryModel();
                        models.Add(JsonConvert.DeserializeObject<CategoryModel>(o.ToString()));
                    }

                    return models;
                }
                else
                {
                    throw new Exception("Could not load categories!");
                }
            }
        }

        public async Task<CategoryModel> Get(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(categoriesUri + id)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(resultContent);

                    CategoryModel model = new CategoryModel();
                    model = JsonConvert.DeserializeObject<CategoryModel>(obj.ToString());

                    return model;
                }
                else
                {
                    throw new Exception("Could not load categories!");
                }
            }
        }

        public async Task Put(int id, CategoryModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(categoriesUri + id),
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